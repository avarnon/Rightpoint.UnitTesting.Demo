using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Mvc.Contracts;

namespace Rightpoint.UnitTesting.Demo.Mvc.Controllers
{
    public class PrimaryObjectsController : Controller
    {
        private readonly IPrimaryObjectService _primaryObjectService;

        public PrimaryObjectsController(IPrimaryObjectService primaryObjectService)
        {
            Ensure.That(primaryObjectService, nameof(primaryObjectService)).IsNotNull();

            this._primaryObjectService = primaryObjectService;
        }

        // GET: PrimaryObjects
        public async Task<ActionResult> Index()
        {
            var primaryObjects = await _primaryObjectService.GetAllAsync();
            var models = primaryObjects.Select(po => new Models.PrimaryObject()
            {
                Id = po.Id,
                Description = po.Description,
                Name = po.Name,
                SecondaryObjects = po.SecondaryObjects?.Select(so => new Models.SecondaryObject()
                {
                    Id = so.Id,
                    Description = so.Description,
                    Name = so.Name,
                })?.ToArray() ?? new Models.SecondaryObject[0],
            }).ToArray();
            Array.ForEach(models, po => Array.ForEach(po.SecondaryObjects.ToArray(), so => so.PrimaryObject = po));

            return View(models);
        }

        // GET: PrimaryObjects/Create
        public ActionResult Create()
        {
            var model = new Models.PrimaryObject();
            return View(model);
        }

        // POST: PrimaryObjects/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            var model = new Models.PrimaryObject()
            {
                Description = collection[nameof(Models.PrimaryObject.Description)],
                Name = collection[nameof(Models.PrimaryObject.Name)],
                SecondaryObjects = new Models.SecondaryObject[0],
            };

            try
            {
                await _primaryObjectService.CreateAsync(model);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: PrimaryObjects/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var primaryObject = await _primaryObjectService.GetAsync(id);
            var model = new Models.PrimaryObject()
            {
                Id = primaryObject.Id,
                Description = primaryObject.Description,
                Name = primaryObject.Name,
                SecondaryObjects = primaryObject.SecondaryObjects?.Select(so => new Models.SecondaryObject()
                {
                    Id = so.Id,
                    Description = so.Description,
                    Name = so.Name,
                })?.ToArray() ?? new Models.SecondaryObject[0],
            };
            Array.ForEach(model.SecondaryObjects.ToArray(), so => so.PrimaryObject = model);
            return View(model);
        }

        // POST: PrimaryObjects/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, FormCollection collection)
        {
            var model = new Models.PrimaryObject()
            {
                Id = id,
                Description = collection[nameof(Models.PrimaryObject.Description)],
                Name = collection[nameof(Models.PrimaryObject.Name)],
                SecondaryObjects = new Models.SecondaryObject[0],
            };
            try
            {
                await _primaryObjectService.UpdateAsync(id, model);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: PrimaryObjects/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var primaryObject = await _primaryObjectService.GetAsync(id);
            var model = new Models.PrimaryObject()
            {
                Id = primaryObject.Id,
                Description = primaryObject.Description,
                Name = primaryObject.Name,
                SecondaryObjects = primaryObject.SecondaryObjects?.Select(so => new Models.SecondaryObject()
                {
                    Id = so.Id,
                    Description = so.Description,
                    Name = so.Name,
                })?.ToArray() ?? new Models.SecondaryObject[0],
            };
            Array.ForEach(model.SecondaryObjects.ToArray(), so => so.PrimaryObject = model);
            return View(model);
        }

        // POST: PrimaryObjects/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
            var model = new Models.PrimaryObject()
            {
                Id = id,
                Description = collection[nameof(Models.PrimaryObject.Description)],
                Name = collection[nameof(Models.PrimaryObject.Name)],
                SecondaryObjects = new Models.SecondaryObject[0],
            };
            try
            {
                await _primaryObjectService.DeleteAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
    }
}
