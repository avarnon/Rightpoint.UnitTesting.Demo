using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContractModels = Rightpoint.UnitTesting.Demo.Mvc.Contracts.Models;
using ViewModels = Rightpoint.UnitTesting.Demo.Mvc.Models;

namespace Rightpoint.UnitTesting.Demo.Mvc.Contracts
{
    public interface ISecondaryObjectService
    {
        Task<ContractModels.SecondaryObject> CreateAsync(Guid primaryObjectd, ViewModels.SecondaryObject inputModel);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<ContractModels.SecondaryObject>> GetAllAsync();

        Task<ContractModels.SecondaryObject> GetAsync(Guid id);

        Task<ContractModels.SecondaryObject> UpdateAsync(Guid id, ViewModels.SecondaryObject inputModel);
    }
}
