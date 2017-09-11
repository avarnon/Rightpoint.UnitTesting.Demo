using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
using Rightpoint.UnitTesting.Demo.Common.Exceptions;

namespace Rightpoint.UnitTesting.Demo.Api.Services
{
    public class ApiExceptionMapper : IApiExceptionMapper
    {
        private const string DefaultResponseMessage = "Internal Server Error";
        private const HttpStatusCode DefaultStatusCode = HttpStatusCode.InternalServerError;

        private static readonly IDictionary<Type, string> __exceptionResponseMessageMap = new ReadOnlyDictionary<Type, string>(
                new Dictionary<Type, string>
                {
                    { typeof(DemoEntityNotFoundException), "Not Found" },
                    { typeof(DemoException), "Internal Server Error" },
                    { typeof(DemoInvalidOperationException), "Bad Request" },
                    { typeof(DemoInputValidationException), "Bad Request" },
                });

        private static readonly IDictionary<Type, HttpStatusCode> __exceptionTypeMap = new ReadOnlyDictionary<Type, HttpStatusCode>(
                new Dictionary<Type, HttpStatusCode>
                {
                    { typeof(DemoEntityNotFoundException), HttpStatusCode.NotFound },
                    { typeof(DemoException), HttpStatusCode.InternalServerError },
                    { typeof(DemoInputValidationException), HttpStatusCode.BadRequest },
                    { typeof(DemoInvalidOperationException), HttpStatusCode.BadRequest },
                });

        public void MapException(HttpActionExecutedContext actionExecutedContext)
        {
            var httpResponseException = actionExecutedContext.Exception as HttpResponseException;

            if (httpResponseException != null)
            {
                actionExecutedContext.Response = httpResponseException.Response;
            }
            else
            {
                var exception = actionExecutedContext.Exception;
                var exceptionType = exception?.GetType();

                actionExecutedContext.Response = GetDefaultResponse(actionExecutedContext);
            }
        }

        private static HttpStatusCode? GetExactMatchStatusCode(Type exceptionType)
        {
            var statusCode = default(HttpStatusCode);
            if (__exceptionTypeMap.TryGetValue(exceptionType, out statusCode))
            {
                return statusCode;
            }
            else
            {
                return null;
            }
        }

        private static string GetExactMatchResponseMessage(Type exceptionType)
        {
            var responseMessage = default(string);
            if (__exceptionResponseMessageMap.TryGetValue(exceptionType, out responseMessage))
            {
                return responseMessage;
            }
            else
            {
                return null;
            }
        }

        private static HttpResponseMessage GetDefaultResponse(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            if (exception == null)
            {
                exception = new Exception("Server Error");
            }

            var exceptionType = exception.GetType();
            var statusCode = GetExactMatchStatusCode(exceptionType) ?? DefaultStatusCode;
            var responseMessage = GetExactMatchResponseMessage(exceptionType) ?? DefaultResponseMessage;

            return actionExecutedContext.Request.CreateResponse(statusCode, new
            {
                Code = (int)statusCode,
                Message = responseMessage,
            });
        }
    }
}