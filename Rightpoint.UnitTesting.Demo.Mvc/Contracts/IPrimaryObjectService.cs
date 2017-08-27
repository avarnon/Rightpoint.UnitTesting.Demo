using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContractModels = Rightpoint.UnitTesting.Demo.Mvc.Contracts.Models;
using ViewModels = Rightpoint.UnitTesting.Demo.Mvc.Models;

namespace Rightpoint.UnitTesting.Demo.Mvc.Contracts
{
    public interface IPrimaryObjectService
    {
        Task<ContractModels.PrimaryObject> CreateAsync(ViewModels.PrimaryObject inputModel);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<ContractModels.PrimaryObject>> GetAllAsync();

        Task<ContractModels.PrimaryObject> GetAsync(Guid id);

        Task<ContractModels.PrimaryObject> UpdateAsync(Guid id, ViewModels.PrimaryObject inputModel);
    }
}
