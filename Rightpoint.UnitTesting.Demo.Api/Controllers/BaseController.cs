using System.Web.Http;
using Rightpoint.UnitTesting.Demo.Api.Attributes;

namespace Rightpoint.UnitTesting.Demo.Api.Controllers
{
    [DemoExceptionFilter]
    public abstract class BaseController : ApiController
    {
    }
}
