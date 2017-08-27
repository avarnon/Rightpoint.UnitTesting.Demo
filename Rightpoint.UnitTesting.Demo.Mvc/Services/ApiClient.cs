using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using EnsureThat;
using Newtonsoft.Json;
using Rightpoint.UnitTesting.Demo.Mvc.Contracts;

namespace Rightpoint.UnitTesting.Demo.Mvc.Services
{
    [ExcludeFromCodeCoverage]
    public class ApiClient : IApiClient
    {
        private readonly WebClient _webClient;

        public ApiClient(WebClient webClient)
        {
            Ensure.That(webClient, nameof(webClient)).IsNotNull();

            this._webClient = webClient;
        }

        public async Task<TEntity> CreateAsync<TEntity>(string address, TEntity entity)
        {
            var inputJson = JsonConvert.SerializeObject(entity);
            var outputJson = await _webClient.UploadStringTaskAsync(address, "POST", inputJson);
            return JsonConvert.DeserializeObject<TEntity>(outputJson);
        }

        public async Task DeleteAsync(string address)
        {
            await _webClient.UploadStringTaskAsync(address, "DELETE", string.Empty);
        }

        public async Task<TEntity> GetAsync<TEntity>(string address)
        {
            var outputJson = await _webClient.DownloadStringTaskAsync(address);
            return JsonConvert.DeserializeObject<TEntity>(outputJson);
        }

        public async Task<TEntity> UpdateAsync<TEntity>(string address, TEntity entity)
        {
            var inputJson = JsonConvert.SerializeObject(entity);
            var outputJson = await _webClient.UploadStringTaskAsync(address, "PUT", inputJson);
            return JsonConvert.DeserializeObject<TEntity>(outputJson);
        }
    }
}