using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Contracts
{
    public interface IPrimaryObjectService
    {
        Task<IEnumerable<PrimaryObject>> GetAllAsync();

        Task<PrimaryObject> GetAsync(Guid id);
    }
}
