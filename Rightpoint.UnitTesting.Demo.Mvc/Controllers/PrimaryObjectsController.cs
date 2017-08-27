using System.Web.Mvc;

namespace Rightpoint.UnitTesting.Demo.Mvc.Controllers
{
    public class PrimaryObjectsController : Controller
    {
        // GET: PrimaryObjects
        public ActionResult Index()
        {
            return View();
        }

        // GET: PrimaryObjects/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrimaryObjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrimaryObjects/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //// TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PrimaryObjects/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrimaryObjects/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                //// TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PrimaryObjects/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrimaryObjects/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //// TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
