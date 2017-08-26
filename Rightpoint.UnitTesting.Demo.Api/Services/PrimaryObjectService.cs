using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
using Rightpoint.UnitTesting.Demo.Domain.Models;
using Rightpoint.UnitTesting.Demo.Domain.Repositories;

namespace Rightpoint.UnitTesting.Demo.Api.Services
{
    public class PrimaryObjectService : IPrimaryObjectService
    {
        private readonly IPrimaryObjectRepository _primaryObjectRepoistory;

        public PrimaryObjectService(IPrimaryObjectRepository primaryObjectRepoistory)
        {
            Ensure.That(primaryObjectRepoistory, nameof(primaryObjectRepoistory)).IsNotNull();

            this._primaryObjectRepoistory = primaryObjectRepoistory;
        }

        public async Task<IEnumerable<PrimaryObject>> GetAllAsync()
        {
            return await _primaryObjectRepoistory.GetAllAsync();
        }

        public async Task<PrimaryObject> GetAsync(Guid id)
        {
            return await _primaryObjectRepoistory.GetByIdAsync(id);
        }
    }
}