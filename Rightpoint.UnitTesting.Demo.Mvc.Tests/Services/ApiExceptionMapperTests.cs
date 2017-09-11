using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Common.Exceptions;
using Rightpoint.UnitTesting.Demo.Mvc.Controllers;
using Rightpoint.UnitTesting.Demo.Mvc.Exceptions;
using Rightpoint.UnitTesting.Demo.Mvc.Services;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.Services
{
    [TestClass]
    public class MvcExceptionMapperTests
    {
        [TestMethod]
        public void MvcExceptionMapper_Constructor()
        {
            var mvcExceptionMapper = new MvcExceptionMapper();
        }

        [TestMethod]
        public void MvcExceptionMapper_MapException_AggregateException()
        {
            var mvcExceptionMapper = new MvcExceptionMapper();
            var exception = new AggregateException("Test 1", new Exception("Test 2", new Exception("Test 2.1")), new Exception("Test 3", new Exception("Test 3.1")));
            var filterContext = this.CreateExceptionContext(exception);

            mvcExceptionMapper.SetResponse(filterContext);

            Assert.IsNotNull(filterContext.Result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, filterContext.HttpContext.Response.StatusCode);
        }

        [TestMethod]
        public void MvcExceptionMapper_MapException_DemoEntityNotFoundException()
        {
            var mvcExceptionMapper = new MvcExceptionMapper();
            var filterContext = this.CreateExceptionContext(new DemoEntityNotFoundException("Testing"));

            mvcExceptionMapper.SetResponse(filterContext);

            Assert.IsNotNull(filterContext.Result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, filterContext.HttpContext.Response.StatusCode);
        }

        [TestMethod]
        public void MvcExceptionMapper_MapException_DemoException()
        {
            var mvcExceptionMapper = new MvcExceptionMapper();
            var filterContext = this.CreateExceptionContext(new DemoException("Testing"));

            mvcExceptionMapper.SetResponse(filterContext);

            Assert.IsNotNull(filterContext.Result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, filterContext.HttpContext.Response.StatusCode);
        }

        [TestMethod]
        public void MvcExceptionMapper_MapException_DemoInputValidationException()
        {
            var mvcExceptionMapper = new MvcExceptionMapper();
            var filterContext = this.CreateExceptionContext(new DemoInputValidationException("Testing"));

            mvcExceptionMapper.SetResponse(filterContext);

            Assert.IsNotNull(filterContext.Result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, filterContext.HttpContext.Response.StatusCode);
        }

        [TestMethod]
        public void MvcExceptionMapper_MapException_DemoInvalidOperationException()
        {
            var mvcExceptionMapper = new MvcExceptionMapper();
            var filterContext = this.CreateExceptionContext(new DemoInvalidOperationException("Testing"));

            mvcExceptionMapper.SetResponse(filterContext);

            Assert.IsNotNull(filterContext.Result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, filterContext.HttpContext.Response.StatusCode);
        }

        [TestMethod]
        public void MvcExceptionMapper_MapException_RemoteEntityNotFoundException()
        {
            var mvcExceptionMapper = new MvcExceptionMapper();
            var filterContext = this.CreateExceptionContext(new RemoteEntityNotFoundException("Testing"));

            mvcExceptionMapper.SetResponse(filterContext);

            Assert.IsNotNull(filterContext.Result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, filterContext.HttpContext.Response.StatusCode);
        }

        [TestMethod]
        public void MvcExceptionMapper_MapException_RemoteException()
        {
            var mvcExceptionMapper = new MvcExceptionMapper();
            var filterContext = this.CreateExceptionContext(new RemoteException("Testing"));

            mvcExceptionMapper.SetResponse(filterContext);

            Assert.IsNotNull(filterContext.Result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, filterContext.HttpContext.Response.StatusCode);
        }

        [TestMethod]
        public void MvcExceptionMapper_MapException_RemoteInvalidOperationException()
        {
            var mvcExceptionMapper = new MvcExceptionMapper();
            var filterContext = this.CreateExceptionContext(new RemoteInvalidOperationException("Testing"));

            mvcExceptionMapper.SetResponse(filterContext);

            Assert.IsNotNull(filterContext.Result);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, filterContext.HttpContext.Response.StatusCode);
        }

        [TestMethod]
        public void MvcExceptionMapper_MapException_SystemException()
        {
            var mvcExceptionMapper = new MvcExceptionMapper();
            var filterContext = this.CreateExceptionContext(new Exception("Testing"));

            mvcExceptionMapper.SetResponse(filterContext);

            Assert.IsNotNull(filterContext.Result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, filterContext.HttpContext.Response.StatusCode);
        }

        [TestMethod]
        public void MvcExceptionMapper_MapException_SystemException_InnerException()
        {
            var mvcExceptionMapper = new MvcExceptionMapper();
            var exception = new Exception("Test 1", new Exception("Test 2"));
            var filterContext = this.CreateExceptionContext(exception);

            mvcExceptionMapper.SetResponse(filterContext);

            Assert.IsNotNull(filterContext.Result);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, filterContext.HttpContext.Response.StatusCode);
        }

        private ExceptionContext CreateExceptionContext(Exception exception)
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);

            var controller = new TestController();
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Test");
            routeData.Values.Add("action", "Index");

            var request = new HttpRequest(string.Empty, "http://example.com/Test/Index", string.Empty);
            var response = new HttpResponse(TextWriter.Null);
            var httpContext = new HttpContextWrapper(new HttpContext(request, response));
            request.RequestContext = new RequestContext(httpContext, routeData);
            controller.ControllerContext = new ControllerContext(httpContext, routeData, controller)
            {
                RequestContext = request.RequestContext
            };
            controller.Url = new UrlHelper(request.RequestContext, routeCollection);

            return new ExceptionContext(controller.ControllerContext, exception);
        }

        private class TestController : BaseController
        {
        }
    }
}