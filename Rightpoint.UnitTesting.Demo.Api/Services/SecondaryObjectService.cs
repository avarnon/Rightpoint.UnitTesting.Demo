using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
using Rightpoint.UnitTesting.Demo.Domain.Models;
using Rightpoint.UnitTesting.Demo.Domain.Repositories;

namespace Rightpoint.UnitTesting.Demo.Api.Services
{
    public class SecondaryObjectService : ISecondaryObjectService
    {
        private readonly ISecondaryObjectRepository _secondaryObjectRepository;

        public SecondaryObjectService(ISecondaryObjectRepository secondaryObjectRepository)
        {
            Ensure.That(secondaryObjectRepository, nameof(secondaryObjectRepository)).IsNotNull();

            this._secondaryObjectRepository = secondaryObjectRepository;
        }

        public async Task<IEnumerable<SecondaryObject>> GetAllAsync()
        {
            return await _secondaryObjectRepository.GetAllAsync();
        }

        public async Task<SecondaryObject> GetAsync(Guid id)
        {
            return await _secondaryObjectRepository.GetByIdAsync(id);
        }
    }
}