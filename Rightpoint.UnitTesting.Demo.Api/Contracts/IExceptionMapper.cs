using System.Web.Http.Filters;

namespace Rightpoint.UnitTesting.Demo.Api.Contracts
{
    public interface IExceptionMapper
    {
        void MapException(HttpActionExecutedContext actionExecutedContext);
    }
}
