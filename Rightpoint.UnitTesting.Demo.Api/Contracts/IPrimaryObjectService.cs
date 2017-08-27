using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiModels = Rightpoint.UnitTesting.Demo.Api.Models;
using DomainModels = Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Contracts
{
    public interface IPrimaryObjectService
    {
        Task<DomainModels.PrimaryObject> CreateAsync(ApiModels.PrimaryObject inputModel);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<DomainModels.PrimaryObject>> GetAllAsync();

        Task<DomainModels.PrimaryObject> GetAsync(Guid id);

        Task<DomainModels.PrimaryObject> UpdateAsync(Guid id, ApiModels.PrimaryObject inputModel);
    }
}
