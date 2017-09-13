using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Filters;
using Rightpoint.UnitTesting.Demo.Api.Contracts;

namespace Rightpoint.UnitTesting.Demo.Api.Attributes
{
    /// <summary>
    /// Demo Web API project's Exception Filter Attribute.
    /// </summary>
    /// <remarks>
    /// We're using <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> on the class because we can't really unit test it.
    /// We can't easily test this because of it's dependency on the HTTP pipeline for dependency resolution. There wouldn't be much benefit in testing it anyway.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class DemoExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            IApiExceptionMapper exceptionMapper = this.Resolve<IApiExceptionMapper>(actionExecutedContext.Request);
            exceptionMapper.MapException(actionExecutedContext);
        }
    }
}