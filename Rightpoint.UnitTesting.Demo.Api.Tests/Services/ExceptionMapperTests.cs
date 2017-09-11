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
    public class ExceptionMapperTests
    {
        [TestMethod]
        public void ExceptionMapper_Constructor()
        {
            var exceptionMapper = new ExceptionMapper();
        }

        [TestMethod]
        public void ExceptionMapper_MapException_Existing_Exception()
        {
            var exceptionMapper = new ExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(null);
            actionExecutedContext.Exception = new HttpResponseException(HttpStatusCode.InternalServerError);

            exceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
        }

        [TestMethod]
        public void ExceptionMapper_MapException_AggregateException()
        {
            var exceptionMapper = new ExceptionMapper();
            var exception = new AggregateException("Test 1", new Exception("Test 2", new Exception("Test 2.1")), new Exception("Test 3", new Exception("Test 3.1")));
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(exception);

            exceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ExceptionMapper_MapException_DemoEntityNotFoundException()
        {
            var exceptionMapper = new ExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new DemoEntityNotFoundException("Testing"));

            exceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.NotFound, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ExceptionMapper_MapException_DemoException()
        {
            var exceptionMapper = new ExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new DemoException("Testing"));

            exceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ExceptionMapper_MapException_DemoInputValidationException()
        {
            var exceptionMapper = new ExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new DemoInputValidationException("Testing"));

            exceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ExceptionMapper_MapException_DemoInvalidOperationException()
        {
            var exceptionMapper = new ExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new DemoInvalidOperationException("Testing"));

            exceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ExceptionMapper_MapException_NullException()
        {
            var exceptionMapper = new ExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(null);

            exceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ExceptionMapper_MapException_SystemException()
        {
            var exceptionMapper = new ExceptionMapper();
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(new Exception("Testing"));

            exceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        [TestMethod]
        public void ExceptionMapper_MapException_SystemException_InnerException()
        {
            var exceptionMapper = new ExceptionMapper();
            var exception = new Exception("Test 1", new Exception("Test 2"));
            var actionExecutedContext = this.CreateExecutedContextWithStatusCode(exception);

            exceptionMapper.MapException(actionExecutedContext);

            Assert.IsNotNull(actionExecutedContext.Response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionExecutedContext.Response.StatusCode);
            Assert.IsNotNull(actionExecutedContext.Response.Content);
        }

        private HttpActionExecutedContext CreateExecutedContextWithStatusCode(Exception exception)
        {
            var request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());

            var actionContext = new HttpActionContext
            {
                ControllerContext = new HttpControllerContext
                {
                    Request = request
                }
            };

            return new HttpActionExecutedContext(actionContext, exception)
            {
                Request =
                {
                    Content = new StringContent("Testing In"),
                    RequestUri = new Uri("http://localhost")
                },
                Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("Testing Out")
                }
            };
        }
    }
}