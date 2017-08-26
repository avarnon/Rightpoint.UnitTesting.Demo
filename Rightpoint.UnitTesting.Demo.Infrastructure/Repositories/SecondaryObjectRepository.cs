using System;
using System.Threading.Tasks;
using Rightpoint.UnitTesting.Demo.Domain.Models;
using Rightpoint.UnitTesting.Demo.Domain.Repositories;
using Rightpoint.UnitTesting.Demo.Infrastructure.Data;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Repositories
{
    public class SecondaryObjectRepository : BaseRepository<SecondaryObject>, ISecondaryObjectRepository
    {
        public SecondaryObjectRepository(DemoContext context)
            : base(context)
        {
        }
    }
}
