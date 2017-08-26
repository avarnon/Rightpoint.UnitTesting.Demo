using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Domain.Repositories
{
    public interface ISecondaryObjectRepository
    {
        Task<SecondaryObject> GetByIdAsync(Guid id);

        Task<ICollection<SecondaryObject>> GetAllAsync();
    }
}
