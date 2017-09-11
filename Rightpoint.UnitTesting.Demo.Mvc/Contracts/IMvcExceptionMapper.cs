using System.Web.Mvc;

namespace Rightpoint.UnitTesting.Demo.Mvc.Contracts
{
    public interface IMvcExceptionMapper
    {
        void SetResponse(ExceptionContext filterContext);
    }
}
