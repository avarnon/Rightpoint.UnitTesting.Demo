using System.Threading.Tasks;

namespace Rightpoint.UnitTesting.Demo.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
