using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rightpoint.UnitTesting.Demo.Mvc.Contracts
{
    public interface IApiClient
    {
        Task<TEntity> CreateAsync<TEntity>(string address, TEntity entity);

        Task DeleteAsync(string address);

        Task<TEntity> GetAsync<TEntity>(string address);

        Task<TEntity> UpdateAsync<TEntity>(string address, TEntity entity);
    }
}
