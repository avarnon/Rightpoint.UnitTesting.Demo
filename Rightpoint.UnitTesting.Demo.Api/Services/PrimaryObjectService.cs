using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
using Rightpoint.UnitTesting.Demo.Domain.Repositories;
using ApiModels = Rightpoint.UnitTesting.Demo.Api.Models;
using DomainModels = Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Services
{
    public class PrimaryObjectService : IPrimaryObjectService
    {
        private readonly IPrimaryObjectRepository _primaryObjectRepoistory;
        private readonly ISecondaryObjectRepository _secondaryObjectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PrimaryObjectService(
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

        public async Task<DomainModels.PrimaryObject> CreateAsync(ApiModels.PrimaryObject inputModel)
        {
            Ensure.That(inputModel, nameof(inputModel)).IsNotNull();

            var domainPrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid());
            this.Map(inputModel, domainPrimaryObject);
            _primaryObjectRepoistory.Add(domainPrimaryObject);

            await _unitOfWork.SaveChangesAsync();

            return domainPrimaryObject;
        }

        public async Task DeleteAsync(Guid id)
        {
            var domainPrimaryObject = await _primaryObjectRepoistory.GetByIdAsync(id);

            Ensure.That(domainPrimaryObject, nameof(domainPrimaryObject)).IsNotNull();

            foreach (var domainSecondaryObject in domainPrimaryObject.SecondaryObjects.ToArray())
            {
                domainPrimaryObject.SecondaryObjects.Remove(domainSecondaryObject);
                _secondaryObjectRepository.Remove(domainSecondaryObject);
            }

            _primaryObjectRepoistory.Remove(domainPrimaryObject);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<DomainModels.PrimaryObject>> GetAllAsync()
        {
            return await _primaryObjectRepoistory.GetAllAsync();
        }

        public async Task<DomainModels.PrimaryObject> GetAsync(Guid id)
        {
            return await _primaryObjectRepoistory.GetByIdAsync(id);
        }

        public async Task<DomainModels.PrimaryObject> UpdateAsync(Guid id, ApiModels.PrimaryObject inputModel)
        {
            Ensure.That(inputModel, nameof(inputModel)).IsNotNull();

            var domainPrimaryObject = await _primaryObjectRepoistory.GetByIdAsync(id);

            Ensure.That(domainPrimaryObject, nameof(domainPrimaryObject)).IsNotNull();

            this.Map(inputModel, domainPrimaryObject);

            await _unitOfWork.SaveChangesAsync();

            return domainPrimaryObject;
        }

        private void Map(ApiModels.PrimaryObject source, DomainModels.PrimaryObject target)
        {
            Ensure.That(source, nameof(source)).IsNotNull();
            Ensure.That(target, nameof(target)).IsNotNull();
            Ensure.That(source.Name, $"{nameof(target)}.{nameof(ApiModels.PrimaryObject.Name)}").IsNotNullOrWhiteSpace();
            Ensure.That(source.Description, $"{nameof(target)}.{nameof(ApiModels.PrimaryObject.Description)}").IsNotNullOrWhiteSpace();

            target.Description = source.Description;
            target.Name = source.Name;
        }
    }
}