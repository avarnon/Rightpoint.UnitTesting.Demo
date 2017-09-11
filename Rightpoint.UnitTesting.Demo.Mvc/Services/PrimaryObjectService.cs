using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Mvc.Contracts;
using ContractModels = Rightpoint.UnitTesting.Demo.Mvc.Contracts.Models;
using ViewModels = Rightpoint.UnitTesting.Demo.Mvc.Models;

namespace Rightpoint.UnitTesting.Demo.Mvc.Services
{
    public class PrimaryObjectService : IPrimaryObjectService
    {
        private const string __addressRoot = "api/primaryobjects";
        private readonly IApiClient _apiClient;

        public PrimaryObjectService(IApiClient apiClient)
        {
            Ensure.That(apiClient, nameof(apiClient)).IsNotNull();

            this._apiClient = apiClient;
        }

        public async Task<ContractModels.PrimaryObject> CreateAsync(ViewModels.PrimaryObject inputModel)
        {
            Ensure.That(inputModel, nameof(inputModel)).IsNotNull();

            var contractModel = new ContractModels.PrimaryObject()
            {
                Description = inputModel.Description,
                Name = inputModel.Name,
            };

            return await _apiClient.CreateAsync(__addressRoot, contractModel);
        }

        public async Task DeleteAsync(Guid id)
        {
            Ensure.That(id, nameof(id)).IsNotEmpty();

            await _apiClient.DeleteAsync($"{__addressRoot}/{id}");
        }

        public async Task<IEnumerable<ContractModels.PrimaryObject>> GetAllAsync()
        {
            return await _apiClient.GetAsync<IEnumerable<ContractModels.PrimaryObject>>(__addressRoot);
        }

        public async Task<ContractModels.PrimaryObject> GetAsync(Guid id)
        {
            Ensure.That(id, nameof(id)).IsNotEmpty();

            return await _apiClient.GetAsync<ContractModels.PrimaryObject>($"{__addressRoot}/{id}");
        }

        public async Task<ContractModels.PrimaryObject> UpdateAsync(Guid id, ViewModels.PrimaryObject inputModel)
        {
            Ensure.That(id, nameof(id)).IsNotEmpty();
            Ensure.That(inputModel, nameof(inputModel)).IsNotNull();

            var contractModel = new ContractModels.PrimaryObject()
            {
                Description = inputModel.Description,
                Name = inputModel.Name,
            };

            return await _apiClient.UpdateAsync($"{__addressRoot}/{id}", contractModel);
        }
    }
}