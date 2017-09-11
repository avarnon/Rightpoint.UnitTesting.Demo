using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Mvc.Contracts;

namespace Rightpoint.UnitTesting.Demo.Mvc.Controllers
{
    public class SecondaryObjectsController : BaseController
    {
        private readonly ISecondaryObjectService _secondaryObjectService;

        public SecondaryObjectsController(ISecondaryObjectService secondaryObjectService)
        {
            Ensure.That(secondaryObjectService, nameof(secondaryObjectService)).IsNotNull();

            this._secondaryObjectService = secondaryObjectService;
        }

        // GET: SecondaryObjects/Create/5
        [HttpGet]
        [Route("create")]
        public ActionResult Create(Guid id)
        {
            var model = new Models.SecondaryObject()
            {
                PrimaryObjectId = id,
            };
            return View(model);
        }

        // POST: SecondaryObjects/Create/5
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Create(Guid id, FormCollection collection)
        {
            var model = new Models.SecondaryObject()
            {
                Description = collection[nameof(Models.SecondaryObject.Description)],
                Name = collection[nameof(Models.SecondaryObject.Name)],
                PrimaryObjectId = id,
            };

            try
            {
                await _secondaryObjectService.CreateAsync(id, model);

                return RedirectToAction("Edit", "PrimaryObjects", new { id = id });
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: SecondaryObjects/Edit/5
        [HttpGet]
        [Route("edit")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var secondaryObject = await _secondaryObjectService.GetAsync(id);

            var model = new Models.SecondaryObject()
            {
                Id = secondaryObject.Id,
                Description = secondaryObject.Description,
                Name = secondaryObject.Name,
                PrimaryObjectId = secondaryObject.PrimaryObject?.Id,
            };
            return View(model);
        }

        // POST: SecondaryObjects/Edit/5
        [HttpPost]
        [Route("edit")]
        public async Task<ActionResult> Edit(Guid id, FormCollection collection)
        {
            var primaryObjectIdString = collection[nameof(Models.SecondaryObject.PrimaryObjectId)];
            var model = new Models.SecondaryObject()
            {
                Id = id,
                Description = collection[nameof(Models.SecondaryObject.Description)],
                Name = collection[nameof(Models.SecondaryObject.Name)],
                PrimaryObjectId = string.IsNullOrWhiteSpace(primaryObjectIdString) ? null : Guid.Parse(primaryObjectIdString) as Guid?,
            };

            try
            {
                await _secondaryObjectService.UpdateAsync(id, model);

                return RedirectToAction("Edit", "PrimaryObjects", new { id = model.PrimaryObjectId });
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: SecondaryObjects/Delete/5
        [HttpGet]
        [Route("delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var secondaryObject = await _secondaryObjectService.GetAsync(id);
            var model = new Models.SecondaryObject()
            {
                Id = secondaryObject.Id,
                Description = secondaryObject.Description,
                Name = secondaryObject.Name,
                PrimaryObjectId = secondaryObject.PrimaryObject?.Id,
            };
            return View(model);
        }

        // POST: SecondaryObjects/Delete/5
        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
            Models.SecondaryObject model = null;
            try
            {
                var primaryObjectIdString = collection[nameof(Models.SecondaryObject.PrimaryObjectId)];
                model = new Models.SecondaryObject()
                {
                    Id = id,
                    Description = collection[nameof(Models.SecondaryObject.Description)],
                    Name = collection[nameof(Models.SecondaryObject.Name)],
                    PrimaryObjectId = string.IsNullOrWhiteSpace(primaryObjectIdString) ? null : Guid.Parse(primaryObjectIdString) as Guid?,
                };
                await _secondaryObjectService.DeleteAsync(id);

                return RedirectToAction("Edit", "PrimaryObjects", new { id = model.PrimaryObjectId });
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
    }
}
