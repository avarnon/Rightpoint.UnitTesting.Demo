using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Domain.Repositories
{
    public interface IPrimaryObjectRepository
    {
        Task<PrimaryObject> GetByIdAsync(Guid id);

        Task<ICollection<PrimaryObject>> GetAllAsync();
    }
}
