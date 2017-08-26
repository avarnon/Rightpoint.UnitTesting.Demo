using System;
using System.Threading.Tasks;
using Rightpoint.UnitTesting.Demo.Domain.Models;
using Rightpoint.UnitTesting.Demo.Domain.Repositories;
using Rightpoint.UnitTesting.Demo.Infrastructure.Data;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Repositories
{
    public class PrimaryObjectRepository : BaseRepository<PrimaryObject>, IPrimaryObjectRepository
    {
        public PrimaryObjectRepository(DemoContext context)
            : base(context)
        {
        }
    }
}
