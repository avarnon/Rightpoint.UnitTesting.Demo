using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Web.Mvc;
using Rightpoint.UnitTesting.Demo.Common.Exceptions;
using Rightpoint.UnitTesting.Demo.Mvc.Contracts;
using Rightpoint.UnitTesting.Demo.Mvc.Controllers;
using Rightpoint.UnitTesting.Demo.Mvc.Exceptions;

namespace Rightpoint.UnitTesting.Demo.Mvc.Services
{
    public class MvcExceptionMapper : IMvcExceptionMapper
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
                    { typeof(RemoteEntityNotFoundException), "Not Found" },
                    { typeof(RemoteException), "Internal Server Error" },
                    { typeof(RemoteInvalidOperationException), "Bad Request" },
                });

        private static readonly IDictionary<Type, HttpStatusCode> __exceptionTypeMap = new ReadOnlyDictionary<Type, HttpStatusCode>(
                new Dictionary<Type, HttpStatusCode>
                {
                    { typeof(DemoEntityNotFoundException), HttpStatusCode.NotFound },
                    { typeof(DemoException), HttpStatusCode.InternalServerError },
                    { typeof(DemoInputValidationException), HttpStatusCode.BadRequest },
                    { typeof(DemoInvalidOperationException), HttpStatusCode.BadRequest },
                    { typeof(RemoteEntityNotFoundException), HttpStatusCode.NotFound },
                    { typeof(RemoteException), HttpStatusCode.InternalServerError },
                    { typeof(RemoteInvalidOperationException), HttpStatusCode.BadRequest },
                });

        public void SetResponse(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;

            if (exception == null)
            {
                exception = new Exception("Server Error");
            }

            var controller = filterContext.Controller as BaseController;
            var urlHelper = controller.Url;
            var errorUrl = urlHelper.Action("Error");
            var exceptionType = exception.GetType();
            var statusCode = GetExactMatchStatusCode(exceptionType) ?? DefaultStatusCode;
            var responseMessage = GetExactMatchResponseMessage(exceptionType) ?? DefaultResponseMessage;

            filterContext.Result = new RedirectResult(errorUrl);
            filterContext.HttpContext.Response.StatusCode = (int)statusCode;
            filterContext.HttpContext.Response.StatusDescription = responseMessage;
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
    }
}