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
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the default constructor with no custom logic.
            var apiExceptionMapper = new ApiExceptionMapper();
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_AggregateException()
        {
            // This method tests what happens when an AggregateException was thrown
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
            // This method tests what happens when a DemoEntityNotFoundException was thrown
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
            // This method tests what happens when a DemoException was thrown
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
            // This method tests what happens when a DemoInputValidationException was thrown
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
            // This method tests what happens when a DemoInvalidOperationException was thrown
            var apiExceptionMapper = new ApiExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new DemoInvalidOperationException("Testing"));

            apiExceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_HttpResponseException()
        {
            // This method tests what happens when a HttpResponseException was thrown
            var apiExceptionMapper = new ApiExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new HttpResponseException(HttpStatusCode.InternalServerError));

            apiExceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            Assert.IsNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ApiExceptionMapper_MapException_NullException()
        {
            // This method tests what happens when a NULL exception was thrown
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
            // This method tests what happens when an Exception was thrown
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
            // This method tests what happens when an Exception with an inner exception was thrown
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