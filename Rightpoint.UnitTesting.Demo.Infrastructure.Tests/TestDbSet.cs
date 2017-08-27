using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Tests
{
    /// <summary>
    /// Test DB Set.
    /// </summary>
    /// <typeparam name="TEntity">The type of the inner objects</typeparam>
    /// <remarks>From https://msdn.microsoft.com/en-us/data/dn314431</remarks>
    public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
            where TEntity : class
    {
        private ObservableCollection<TEntity> _data;
        private IQueryable _query;

        public TestDbSet()
        {
            _data = new ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public override ObservableCollection<TEntity> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<TEntity>(_query.Provider); }
        }

        public override TEntity Add(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _data.Add(entity);
            }

            return entities;
        }

        public override TEntity Remove(TEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public void AddOrUpdate(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                if (_data.Contains(entity) == false)
                {
                    _data.Add(entity);
                }
            }
        }

        public void AddOrUpdate(Expression<Func<TEntity, object>> identifierExpression, params TEntity[] entities)
        {
            var getIdentifier = identifierExpression.Compile();
            foreach (var entity in entities)
            {
                if (_data.Any(item => getIdentifier(item) == getIdentifier(entity)) == false)
                {
                    _data.Add(entity);
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<TEntity>(_data.GetEnumerator());
        }
    }
}
