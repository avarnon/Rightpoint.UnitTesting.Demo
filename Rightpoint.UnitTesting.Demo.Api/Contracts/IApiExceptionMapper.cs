using System.Web.Http.Filters;

namespace Rightpoint.UnitTesting.Demo.Api.Contracts
{
    public interface IApiExceptionMapper
    {
        void MapException(HttpActionExecutedContext actionExecutedContext);
    }
}
