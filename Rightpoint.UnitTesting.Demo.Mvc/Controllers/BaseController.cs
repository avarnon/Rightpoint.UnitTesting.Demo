using System.Web.Mvc;
using Rightpoint.UnitTesting.Demo.Mvc.Attributes;

namespace Rightpoint.UnitTesting.Demo.Mvc.Controllers
{
    [DemoHandleError]
    public abstract class BaseController : Controller
    {
        // GET: ~/Error
        [HttpGet]
        [Route("")]
        public ActionResult Error()
        {
            return View();
        }
    }
}