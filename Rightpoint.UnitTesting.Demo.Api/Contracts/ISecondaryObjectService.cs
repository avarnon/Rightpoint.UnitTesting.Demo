using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiModels = Rightpoint.UnitTesting.Demo.Api.Models;
using DomainModels = Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Contracts
{
    public interface ISecondaryObjectService
    {
        Task<DomainModels.SecondaryObject> CreateAsync(Guid primaryObjectId, ApiModels.SecondaryObject inputModel);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<DomainModels.SecondaryObject>> GetAllAsync();

        Task<DomainModels.SecondaryObject> GetAsync(Guid id);

        Task<DomainModels.SecondaryObject> UpdateAsync(Guid id, ApiModels.SecondaryObject inputModel);
    }
}
