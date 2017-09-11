using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Filters;
using Rightpoint.UnitTesting.Demo.Api.Contracts;

namespace Rightpoint.UnitTesting.Demo.Api.Attributes
{
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