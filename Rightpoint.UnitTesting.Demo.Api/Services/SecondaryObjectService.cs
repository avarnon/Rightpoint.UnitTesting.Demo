using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
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

        public async Task<DomainModels.SecondaryObject> CreateAsync(Guid primaryObjectd, ApiModels.SecondaryObject inputModel)
        {
            Ensure.That(inputModel, nameof(inputModel)).IsNotNull();

            var domainPrimaryObject = await _primaryObjectRepoistory.GetByIdAsync(primaryObjectd);

            Ensure.That(domainPrimaryObject, nameof(domainPrimaryObject)).IsNotNull();

            var domainSecondaryObject = new DomainModels.SecondaryObject(Guid.NewGuid());
            this.Map(inputModel, domainSecondaryObject);
            domainSecondaryObject.PrimaryObject = domainPrimaryObject;
            domainSecondaryObject.PrimaryObject_Id = domainPrimaryObject.Id;
            domainPrimaryObject.SecondaryObjects.Add(domainSecondaryObject);

            await _unitOfWork.SaveChangesAsync();

            return domainSecondaryObject;
        }

        public async Task DeleteAsync(Guid id)
        {
            var domainSecondaryObject = await _secondaryObjectRepository.GetByIdAsync(id);

            Ensure.That(domainSecondaryObject, nameof(domainSecondaryObject)).IsNotNull();

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
            Ensure.That(inputModel, nameof(inputModel)).IsNotNull();

            var domainSecondaryObject = await _secondaryObjectRepository.GetByIdAsync(id);

            Ensure.That(domainSecondaryObject, nameof(domainSecondaryObject)).IsNotNull();

            this.Map(inputModel, domainSecondaryObject);

            await _unitOfWork.SaveChangesAsync();

            return domainSecondaryObject;
        }

        private void Map(ApiModels.SecondaryObject source, DomainModels.SecondaryObject target)
        {
            Ensure.That(source, nameof(source)).IsNotNull();
            Ensure.That(target, nameof(target)).IsNotNull();
            Ensure.That(source.Name, $"{nameof(target)}.{nameof(ApiModels.SecondaryObject.Name)}").IsNotNullOrWhiteSpace();
            Ensure.That(source.Description, $"{nameof(target)}.{nameof(ApiModels.SecondaryObject.Description)}").IsNotNullOrWhiteSpace();

            target.Description = source.Description;
            target.Name = source.Name;
        }
    }
}