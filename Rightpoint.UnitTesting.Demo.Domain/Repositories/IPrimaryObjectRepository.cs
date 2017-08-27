using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Domain.Repositories
{
    public interface IPrimaryObjectRepository
    {
        PrimaryObject Add(PrimaryObject entity);

        Task<ICollection<PrimaryObject>> GetAllAsync();

        Task<PrimaryObject> GetByIdAsync(Guid id);

        PrimaryObject Remove(PrimaryObject entity);
    }
}
