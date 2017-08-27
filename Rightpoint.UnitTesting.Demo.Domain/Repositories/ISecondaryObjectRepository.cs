using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Domain.Repositories
{
    public interface ISecondaryObjectRepository
    {
        SecondaryObject Add(SecondaryObject entity);

        Task<ICollection<SecondaryObject>> GetAllAsync();

        Task<SecondaryObject> GetByIdAsync(Guid id);

        SecondaryObject Remove(SecondaryObject entity);
    }
}
