using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Mvc.Contracts;

namespace Rightpoint.UnitTesting.Demo.Mvc.Controllers
{
    [RoutePrefix("primaryobjects")]
    public class PrimaryObjectsController : BaseController
    {
        private readonly IPrimaryObjectService _primaryObjectService;
        private readonly ISecondaryObjectService _secondaryObjectService;

        public PrimaryObjectsController(
            IPrimaryObjectService primaryObjectService,
            ISecondaryObjectService secondaryObjectService)
        {
            Ensure.That(primaryObjectService, nameof(primaryObjectService)).IsNotNull();
            Ensure.That(secondaryObjectService, nameof(secondaryObjectService)).IsNotNull();

            this._primaryObjectService = primaryObjectService;
            this._secondaryObjectService = secondaryObjectService;
        }

        // GET: PrimaryObjects
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Index()
        {
            var primaryObjects = await _primaryObjectService.GetAllAsync();
            var models = primaryObjects.Select(po => new Models.PrimaryObject()
            {
                Id = po.Id,
                Description = po.Description,
                Name = po.Name,
                SecondaryObjects = new Models.SecondaryObject[0],
            })
            .OrderBy(_ => _.Name)
            .ToArray();

            return View(models);
        }

        // GET: PrimaryObjects/Create
        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            var model = new Models.PrimaryObject();
            return View(model);
        }

        // POST: PrimaryObjects/Create
        [HttpPost]
        [Route("create")]
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
        [HttpGet]
        [Route("edit")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var primaryObject = await _primaryObjectService.GetAsync(id);
            var secondaryObjectModels = new List<Models.SecondaryObject>();

            if (primaryObject.SecondaryObjects != null)
            {
                foreach (var secondaryObjectThin in primaryObject.SecondaryObjects.Where(so => so.Id.HasValue))
                {
                    var secondaryObject = await _secondaryObjectService.GetAsync(secondaryObjectThin.Id.Value);
                    secondaryObjectModels.Add(new Models.SecondaryObject()
                    {
                        Id = secondaryObject.Id,
                        Description = secondaryObject.Description,
                        Name = secondaryObject.Name,
                        PrimaryObjectId = primaryObject.Id,
                    });
                }
            }

            var model = new Models.PrimaryObject()
            {
                Id = primaryObject.Id,
                Description = primaryObject.Description,
                Name = primaryObject.Name,
                SecondaryObjects = secondaryObjectModels.OrderBy(_ => _.Name).ToArray(),
            };
            return View(model);
        }

        // POST: PrimaryObjects/Edit/5
        [HttpPost]
        [Route("edit")]
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
        [HttpGet]
        [Route("delete")]
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
                    PrimaryObjectId = primaryObject.Id,
                })?.ToArray() ?? new Models.SecondaryObject[0],
            };
            return View(model);
        }

        // POST: PrimaryObjects/Delete/5
        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult> Delete(Guid id, FormCollection collection)
        {
            try
            {
                await _primaryObjectService.DeleteAsync(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var model = new Models.PrimaryObject()
                {
                    Id = id,
                    Description = collection[nameof(Models.PrimaryObject.Description)],
                    Name = collection[nameof(Models.PrimaryObject.Name)],
                    SecondaryObjects = new Models.SecondaryObject[0],
                };

                return View(model);
            }
        }
    }
}
