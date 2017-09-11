using System.Web.Mvc;

namespace Rightpoint.UnitTesting.Demo.Mvc.Controllers
{
    [RoutePrefix("")]
    public class HomeController : BaseController
    {
        // GET:
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "PrimaryObjects");
        }
    }
}