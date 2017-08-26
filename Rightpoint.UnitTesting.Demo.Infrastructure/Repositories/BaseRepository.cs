using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity>
        where TEntity : class, IIdentifiable<Guid>
    {
        private readonly DbContext _dbContext;

        protected BaseRepository(DbContext context)
        {
            Ensure.That(context, nameof(context)).IsNotNull();

            this._dbContext = context;
        }

        protected virtual IQueryable<TEntity> Set => this._dbContext.Set<TEntity>();

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await this.Set.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<TEntity>> GetByIdsAsync(ICollection<Guid> ids)
        {
            return await this.Set.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await this.Set.ToListAsync();
        }

        public TEntity Add(TEntity entity)
        {
            return this._dbContext.Set<TEntity>().Add(entity);
        }

        public void AddMany(IEnumerable<TEntity> entities)
        {
            this._dbContext.Set<TEntity>().AddRange(entities);
        }

        public TEntity Remove(TEntity entity)
        {
            return this._dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
