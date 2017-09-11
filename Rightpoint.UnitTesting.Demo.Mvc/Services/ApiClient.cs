using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using EnsureThat;
using Newtonsoft.Json;
using Rightpoint.UnitTesting.Demo.Mvc.Contracts;
using Rightpoint.UnitTesting.Demo.Mvc.Exceptions;

namespace Rightpoint.UnitTesting.Demo.Mvc.Services
{
    [ExcludeFromCodeCoverage]
    public class ApiClient : IApiClient, IDisposable
    {
        private const string ContentTypeApplicationJson = "application/json";
        private readonly WebClient _webClient;
        private bool _disposed = false;

        public ApiClient(string baseAddress)
        {
            Ensure.That(baseAddress, nameof(baseAddress)).IsNotNullOrWhiteSpace();

            this._webClient = new WebClient()
            {
                BaseAddress = baseAddress,
            };
        }

        public async Task<TEntity> CreateAsync<TEntity>(string address, TEntity entity)
            where TEntity : class
        {
            Ensure.That(this._disposed).WithException(_ => new ObjectDisposedException(nameof(ApiClient))).IsFalse();
            Ensure.That(address, nameof(address)).IsNotNullOrWhiteSpace();
            Ensure.That(entity, nameof(entity)).IsNotNull();

            var inputJson = JsonConvert.SerializeObject(entity);
            _webClient.Headers[HttpRequestHeader.ContentType] = ContentTypeApplicationJson;
            _webClient.Headers[HttpRequestHeader.Accept] = ContentTypeApplicationJson;
            string outputJson = null;

            try
            {
                outputJson = await _webClient.UploadStringTaskAsync(address, "POST", inputJson);
            }
            catch (WebException ex)
            {
                HandleWebExcpetion(ex);
                throw;
            }

            return JsonConvert.DeserializeObject<TEntity>(outputJson);
        }

        public async Task DeleteAsync(string address)
        {
            Ensure.That(this._disposed).WithException(_ => new ObjectDisposedException(nameof(ApiClient))).IsFalse();
            Ensure.That(address, nameof(address)).IsNotNullOrWhiteSpace();

            try
            {
                await _webClient.UploadStringTaskAsync(address, "DELETE", string.Empty);
            }
            catch (WebException ex)
            {
                HandleWebExcpetion(ex);
                throw;
            }
        }

        public async Task<TEntity> GetAsync<TEntity>(string address)
            where TEntity : class
        {
            Ensure.That(this._disposed).WithException(_ => new ObjectDisposedException(nameof(ApiClient))).IsFalse();
            Ensure.That(address, nameof(address)).IsNotNullOrWhiteSpace();

            _webClient.Headers[HttpRequestHeader.Accept] = ContentTypeApplicationJson;
            string outputJson = null;

            try
            {
                outputJson = await _webClient.DownloadStringTaskAsync(address);
            }
            catch (WebException ex)
            {
                HandleWebExcpetion(ex);
                throw;
            }

            return JsonConvert.DeserializeObject<TEntity>(outputJson);
        }

        public async Task<TEntity> UpdateAsync<TEntity>(string address, TEntity entity)
            where TEntity : class
        {
            Ensure.That(this._disposed).WithException(_ => new ObjectDisposedException(nameof(ApiClient))).IsFalse();
            Ensure.That(address, nameof(address)).IsNotNullOrWhiteSpace();
            Ensure.That(entity, nameof(entity)).IsNotNull();

            var inputJson = JsonConvert.SerializeObject(entity);
            _webClient.Headers[HttpRequestHeader.ContentType] = ContentTypeApplicationJson;
            _webClient.Headers[HttpRequestHeader.Accept] = ContentTypeApplicationJson;
            string outputJson = null;

            try
            {
                outputJson = await _webClient.UploadStringTaskAsync(address, "PUT", inputJson);
            }
            catch (WebException ex)
            {
                HandleWebExcpetion(ex);
                throw;
            }

            return JsonConvert.DeserializeObject<TEntity>(outputJson);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                this._webClient.Dispose();
                _disposed = true;
            }
        }

        private static async Task HandleWebExcpetion(WebException ex)
        {
            var response = ex.Response as HttpWebResponse;
            if (response != null)
            {
                string responsePayload = null;
                var strm = response.GetResponseStream();
                if (strm != null)
                {
                    try
                    {
                        using (var rdr = new StreamReader(strm))
                        {
                            responsePayload = await rdr.ReadToEndAsync();
                        }
                    }
                    finally
                    {
                        strm.Dispose();
                        strm = null;
                    }
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        throw new RemoteEntityNotFoundException(responsePayload, ex);
                    case HttpStatusCode.BadRequest:
                        throw new RemoteInvalidOperationException(responsePayload, ex);
                    default:
                        throw new RemoteException(responsePayload, ex);
                }
            }
        }
    }
}