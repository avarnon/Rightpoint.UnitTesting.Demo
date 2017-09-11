using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
using Rightpoint.UnitTesting.Demo.Common.Exceptions;
using Rightpoint.UnitTesting.Demo.Domain.Repositories;
using ApiModels = Rightpoint.UnitTesting.Demo.Api.Models;
using DomainModels = Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Services
{
    public class SecondaryObjectService : ISecondaryObjectService
    {
        private readonly IPrimaryObjectRepository _primaryObjectRepoistory;
        private readonly ISecondaryObjectRepository _secondaryObjectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SecondaryObjectService(
            IPrimaryObjectRepository primaryObjectRepoistory,
            ISecondaryObjectRepository secondaryObjectRepository,
            IUnitOfWork unitOfWork)
        {
            Ensure.That(primaryObjectRepoistory, nameof(primaryObjectRepoistory)).IsNotNull();
            Ensure.That(secondaryObjectRepository, nameof(secondaryObjectRepository)).IsNotNull();
            Ensure.That(unitOfWork, nameof(unitOfWork)).IsNotNull();

            this._primaryObjectRepoistory = primaryObjectRepoistory;
            this._secondaryObjectRepository = secondaryObjectRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<DomainModels.SecondaryObject> CreateAsync(Guid primaryObjectId, ApiModels.SecondaryObject inputModel)
        {
            Ensure.That(primaryObjectId, nameof(primaryObjectId)).IsNotEmpty();
            Ensure.That(inputModel, nameof(inputModel)).IsNotNull();

            var domainPrimaryObject = await _primaryObjectRepoistory.GetByIdAsync(primaryObjectId);

            Ensure.That(domainPrimaryObject, nameof(domainPrimaryObject))
                .WithException(_ =>
                {
                    var ex = new DemoEntityNotFoundException();
                    ex.Data.Add($"{nameof(DomainModels.PrimaryObject)}.{nameof(DomainModels.PrimaryObject.Id)}", primaryObjectId.ToString());
                    return ex;
                })
                .IsNotNull();

            var domainSecondaryObject = new DomainModels.SecondaryObject(Guid.NewGuid());
            Map(inputModel, domainSecondaryObject);
            domainSecondaryObject.PrimaryObject = domainPrimaryObject;
            domainSecondaryObject.PrimaryObject_Id = domainPrimaryObject.Id;
            domainPrimaryObject.SecondaryObjects.Add(domainSecondaryObject);

            await _unitOfWork.SaveChangesAsync();

            return domainSecondaryObject;
        }

        public async Task DeleteAsync(Guid id)
        {
            var domainSecondaryObject = await _secondaryObjectRepository.GetByIdAsync(id);

            Ensure.That(domainSecondaryObject, nameof(domainSecondaryObject))
                .WithException(_ => GetDemoEntityNotFoundException(id))
                .IsNotNull();

            _secondaryObjectRepository.Remove(domainSecondaryObject);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<DomainModels.SecondaryObject>> GetAllAsync()
        {
            return await _secondaryObjectRepository.GetAllAsync();
        }

        public async Task<DomainModels.SecondaryObject> GetAsync(Guid id)
        {
            return await _secondaryObjectRepository.GetByIdAsync(id);
        }

        public async Task<DomainModels.SecondaryObject> UpdateAsync(Guid id, ApiModels.SecondaryObject inputModel)
        {
            Ensure.That(id, nameof(id)).IsNotEmpty();
            Ensure.That(inputModel, nameof(inputModel)).IsNotNull();

            var domainSecondaryObject = await _secondaryObjectRepository.GetByIdAsync(id);

            Ensure.That(domainSecondaryObject, nameof(domainSecondaryObject))
                .WithException(_ => GetDemoEntityNotFoundException(id))
                .IsNotNull();

            Map(inputModel, domainSecondaryObject);

            await _unitOfWork.SaveChangesAsync();

            return domainSecondaryObject;
        }

        private static DemoEntityNotFoundException GetDemoEntityNotFoundException(Guid secondaryObjectId)
        {
            var ex = new DemoEntityNotFoundException();
            ex.Data.Add($"{nameof(DomainModels.SecondaryObject)}.{nameof(DomainModels.SecondaryObject.Id)}", secondaryObjectId.ToString());
            return ex;
        }

        private static void Map(ApiModels.SecondaryObject source, DomainModels.SecondaryObject target)
        {
            Ensure.That(source, nameof(source)).IsNotNull();
            Ensure.That(target, nameof(target)).IsNotNull();
            Ensure.That(source.Name, $"{nameof(target)}.{nameof(ApiModels.SecondaryObject.Name)}").WithException(_ => new DemoInputValidationException()).IsNotNullOrWhiteSpace();
            Ensure.That(source.Description, $"{nameof(target)}.{nameof(ApiModels.SecondaryObject.Description)}").WithException(_ => new DemoInputValidationException()).IsNotNullOrWhiteSpace();

            target.Description = source.Description;
            target.Name = source.Name;
        }
    }
}