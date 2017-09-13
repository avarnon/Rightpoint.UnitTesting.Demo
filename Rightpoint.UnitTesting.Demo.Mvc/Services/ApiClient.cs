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
    /// <summary>
    /// Demo API Client.
    /// </summary>
    /// <remarks>
    /// We're using <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> on the class because we can't really unit test it.
    /// This class is a thin wrapper around <see cref="System.Net.WebClient"/> which can't really be mocked for unit testing.
    /// </remarks>
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
                throw await ProcessWebExcpetionAsync(ex);
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
                throw await ProcessWebExcpetionAsync(ex);
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
                throw await ProcessWebExcpetionAsync(ex);
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
                throw await ProcessWebExcpetionAsync(ex);
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

        private static async Task<Exception> ProcessWebExcpetionAsync(WebException ex)
        {
            string response = null;
            string responseContentType = null;
            Uri responseUri = null;
            var status = ex.Status;
            HttpStatusCode? statusCode = null;
            string statusDescription = null;

            var webResponse = ex.Response as HttpWebResponse;
            if (webResponse != null)
            {
                responseUri = webResponse.ResponseUri;
                responseContentType = webResponse.ContentType;
                statusCode = webResponse.StatusCode;
                statusDescription = webResponse.StatusDescription;

                var responseStream = webResponse.GetResponseStream();
                if (responseStream != null)
                {
                    try
                    {
                        if (responseStream.CanRead)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                response = await reader.ReadToEndAsync();
                            }
                        }
                    }
                    finally
                    {
                        responseStream.Dispose();
                        responseStream = null;
                    }
                }
            }

            Exception newEx = null;
            var message = string.IsNullOrWhiteSpace(response) ? "Unknown server error" : response;

            if (statusCode.HasValue)
            {
                switch (statusCode.Value)
                {
                    case HttpStatusCode.NotFound:
                        newEx = new RemoteEntityNotFoundException(message, ex);
                        break;
                    case HttpStatusCode.BadRequest:
                        newEx = new RemoteInvalidOperationException(message, ex);
                        break;
                    default:
                        newEx = new RemoteException(message, ex);
                        break;
                }
            }
            else
            {
                newEx = new RemoteException(message, ex);
            }

            if (string.IsNullOrWhiteSpace(response) == false) newEx.Data.Add("Response", response);
            if (string.IsNullOrWhiteSpace(responseContentType) == false) newEx.Data.Add("ContentType", responseContentType);
            if (responseUri != null) newEx.Data.Add("ResponseUri", responseUri.ToString());
            if (statusCode.HasValue) newEx.Data.Add("StatusCode", statusCode.ToString());
            if (string.IsNullOrWhiteSpace(statusDescription) == false) newEx.Data.Add("StatusDescription", statusDescription);
            newEx.Data.Add("Status", status.ToString());

            return newEx;
        }
    }
}