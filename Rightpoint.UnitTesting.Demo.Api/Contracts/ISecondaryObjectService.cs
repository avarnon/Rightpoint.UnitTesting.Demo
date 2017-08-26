using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Contracts
{
    public interface ISecondaryObjectService
    {
        Task<IEnumerable<SecondaryObject>> GetAllAsync();

        Task<SecondaryObject> GetAsync(Guid id);
    }
}
