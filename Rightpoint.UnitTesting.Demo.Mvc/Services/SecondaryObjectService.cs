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
    public class SecondaryObjectService : ISecondaryObjectService
    {
        private const string __addressRoot = "api/secondaryobjects";
        private readonly IApiClient _apiClient;

        public SecondaryObjectService(IApiClient apiClient)
        {
            Ensure.That(apiClient, nameof(apiClient)).IsNotNull();

            this._apiClient = apiClient;
        }

        public async Task<ContractModels.SecondaryObject> CreateAsync(Guid primaryObjectd, ViewModels.SecondaryObject inputModel)
        {
            Ensure.That(inputModel, nameof(inputModel)).IsNotNull();

            var contractModel = new ContractModels.SecondaryObject()
            {
                Description = inputModel.Description,
                Name = inputModel.Name,
            };

            return await _apiClient.CreateAsync($"api/primaryobjects/{primaryObjectd}/secondaryobjects", contractModel);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _apiClient.DeleteAsync($"{__addressRoot}/{id}");
        }

        public async Task<IEnumerable<ContractModels.SecondaryObject>> GetAllAsync()
        {
            return await _apiClient.GetAsync<IEnumerable<ContractModels.SecondaryObject>>(__addressRoot);
        }

        public async Task<ContractModels.SecondaryObject> GetAsync(Guid id)
        {
            return await _apiClient.GetAsync<ContractModels.SecondaryObject>($"{__addressRoot}/{id}");
        }

        public async Task<ContractModels.SecondaryObject> UpdateAsync(Guid id, ViewModels.SecondaryObject inputModel)
        {
            var contractModel = new ContractModels.SecondaryObject()
            {
                Description = inputModel.Description,
                Name = inputModel.Name,
            };

            return await _apiClient.UpdateAsync($"{__addressRoot}/{id}", contractModel);
        }
    }
}