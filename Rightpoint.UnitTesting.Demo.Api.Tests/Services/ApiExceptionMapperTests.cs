using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Api.Services;
using Rightpoint.UnitTesting.Demo.Common.Exceptions;

namespace Rightpoint.UnitTesting.Demo.Api.Tests.Services
{
    [TestClass]
    public class ApiExceptionMapperTests
    {
        [TestMethod]
        public void ApiExceptionMapper_Constructor()
        {
            var apiExceptionMapper = new ApiExceptionMapper();
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_Existing_Exception()
        {
            var apiExceptionMapper = new ApiExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(null);
            actionExecutedContext.Exception = new HttpResponseException(HttpStatusCode.InternalServerError);

            apiExceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_AggregateException()
        {
            var apiExceptionMapper = new ApiExceptionMapper();
            var exception = new AggregateException("Test 1", new Exception("Test 2", new Exception("Test 2.1")), new Exception("Test 3", new Exception("Test 3.1")));
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(exception);

            apiExceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_DemoEntityNotFoundException()
        {
            var apiExceptionMapper = new ApiExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new DemoEntityNotFoundException("Testing"));

            apiExceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.NotFound, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_DemoException()
        {
            var apiExceptionMapper = new ApiExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new DemoException("Testing"));

            apiExceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_DemoInputValidationException()
        {
            var apiExceptionMapper = new ApiExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new DemoInputValidationException("Testing"));

            apiExceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_DemoInvalidOperationException()
        {
            var apiExceptionMapper = new ApiExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new DemoInvalidOperationException("Testing"));

            apiExceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_NullException()
        {
            var apiExceptionMapper = new ApiExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(null);

            apiExceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_SystemException()
        {
            var apiExceptionMapper = new ApiExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new Exception("Testing"));

            apiExceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_SystemException_InnerException()
        {
            var apiExceptionMapper = new ApiExceptionMapper();
            var exception = new Exception("Test 1", new Exception("Test 2"));
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(exception);

            apiExceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        private HttpActionExecutedContext CreateExecutedContextWithStatusCode(Exception exception)
        {
            var request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());

            var actionContext = new HttpActionContext()
            {
                ControllerContext = new HttpControllerContext()
                {
                    Request = request,
                },
            };

            return new HttpActionExecutedContext(actionContext, exception)
            {
                Request =
                {
                    Content = new StringContent("Testing In"),
                    RequestUri = new Uri("http://localhost"),
                },
                Response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("Testing Out"),
                },
            };
        }
    }
}