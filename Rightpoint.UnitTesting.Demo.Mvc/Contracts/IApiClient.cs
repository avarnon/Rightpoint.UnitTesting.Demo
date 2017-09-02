using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rightpoint.UnitTesting.Demo.Mvc.Contracts
{
    public interface IApiClient
    {
        Task<TEntity> CreateAsync<TEntity>(string address, TEntity entity)
            where TEntity : class;

        Task DeleteAsync(string address);

        Task<TEntity> GetAsync<TEntity>(string address)
            where TEntity : class;

        Task<TEntity> UpdateAsync<TEntity>(string address, TEntity entity)
            where TEntity : class;
    }
}
