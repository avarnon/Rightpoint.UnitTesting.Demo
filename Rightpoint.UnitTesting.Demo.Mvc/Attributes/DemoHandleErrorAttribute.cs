using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Rightpoint.UnitTesting.Demo.Mvc.Contracts;

namespace Rightpoint.UnitTesting.Demo.Mvc.Attributes
{
    /// <summary>
    /// Demo MVC project's Handle Error Attribute.
    /// </summary>
    /// <remarks>
    /// We're using <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> on the class because we can't really unit test it.
    /// We can't easily test this because of it's dependency on the HTTP pipeline for dependency resolution. There wouldn't be much benefit in testing it anyway.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class DemoHandleErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The action-filter context.</param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            try
            {
                SetResponseFromException(filterContext, filterContext.Exception);
            }
            catch (Exception ex)
            {
                SetResponseFromUnhandledException(filterContext, ex);
            }

            filterContext.ExceptionHandled = true;
        }

        protected T GetService<T>()
        {
            return (T)DependencyResolver.Current.GetService(typeof(T));
        }

        protected virtual void SetResponseFromException(ExceptionContext filterContext, Exception ex)
        {
            var mvcExceptionMapper = this.GetService<IMvcExceptionMapper>();
            mvcExceptionMapper.SetResponse(filterContext);
        }

        protected virtual void SetResponseFromUnhandledException(ExceptionContext filterContext, Exception ex)
        {
            UrlHelper helper = new UrlHelper(filterContext.RequestContext);
            string errorUrl = helper.Action("Error");
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.StatusDescription = "Internal Server Error";
            filterContext.Result = new RedirectResult(errorUrl);
        }
    }
}