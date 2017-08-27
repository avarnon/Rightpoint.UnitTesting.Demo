using System.Threading.Tasks;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Domain.Repositories;
using Rightpoint.UnitTesting.Demo.Infrastructure.Data;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DemoContext _context;

        public UnitOfWork(DemoContext context)
        {
            Ensure.That(context, nameof(context)).IsNotNull();

            this._context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
