using System.Web.Mvc;
using Rightpoint.UnitTesting.Demo.Mvc.Attributes;

namespace Rightpoint.UnitTesting.Demo.Mvc.Controllers
{
    [DemoHandleError]
    public abstract class BaseController : Controller
    {
    }
}