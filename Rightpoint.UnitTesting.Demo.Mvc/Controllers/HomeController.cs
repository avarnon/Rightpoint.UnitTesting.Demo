using System.Web.Mvc;

namespace Rightpoint.UnitTesting.Demo.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}