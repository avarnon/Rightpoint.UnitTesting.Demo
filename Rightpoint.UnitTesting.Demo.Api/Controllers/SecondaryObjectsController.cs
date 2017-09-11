using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
using ApiModels = Rightpoint.UnitTesting.Demo.Api.Models;
using DomainModels = Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Controllers
{
    [RoutePrefix("api/secondaryobjects")]
    public class SecondaryObjectsController : BaseController
    {
        private readonly ISecondaryObjectService _secondaryObjectService;

        public SecondaryObjectsController(ISecondaryObjectService secondaryObjectService)
        {
            Ensure.That(secondaryObjectService, nameof(secondaryObjectService)).IsNotNull();

            this._secondaryObjectService = secondaryObjectService;
        }

        // POST /api/primaryobjects/00000000-0000-0000-0000-000000000000/secondaryobjects
        [HttpPost]
        [Route("~/api/primaryobjects/{primaryObjectd:guid}/secondaryobjects")]
        public async Task<ApiModels.SecondaryObject> CreateAsync(Guid primaryObjectd, ApiModels.SecondaryObject inputModel)
        {
            var domainSecondaryObject = await _secondaryObjectService.CreateAsync(primaryObjectd, inputModel);

            Ensure.That(domainSecondaryObject, nameof(domainSecondaryObject))
                .WithException(_ => new HttpResponseException(HttpStatusCode.BadRequest))
                .IsNotNull();

            return this.Map(domainSecondaryObject);
        }

        // DELETE /api/secondaryobjects/00000000-0000-0000-0000-000000000000
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task DeleteAsync(Guid id)
        {
            await _secondaryObjectService.DeleteAsync(id);
        }

        // GET /api/secondaryobjects
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ApiModels.SecondaryObject>> GetAllAsync()
        {
            var domainSecondaryObjects = await _secondaryObjectService.GetAllAsync();

            Ensure.That(domainSecondaryObjects, nameof(domainSecondaryObjects))
                .WithException(_ => new HttpResponseException(HttpStatusCode.NotFound))
                .IsNotNull();

            return domainSecondaryObjects.Select(this.Map).ToArray();
        }

        // GET /api/secondaryobjects/00000000-0000-0000-0000-000000000000
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ApiModels.SecondaryObject> GetAsync(Guid id)
        {
            var domainSecondaryObject = await _secondaryObjectService.GetAsync(id);

            Ensure.That(domainSecondaryObject, nameof(domainSecondaryObject))
                .WithException(_ => new HttpResponseException(HttpStatusCode.NotFound))
                .IsNotNull();

            return this.Map(domainSecondaryObject);
        }

        // PUT /api/secondaryobjects/00000000-0000-0000-0000-000000000000
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ApiModels.SecondaryObject> UpdateAsync(Guid id, ApiModels.SecondaryObject inputModel)
        {
            var domainSecondaryObject = await _secondaryObjectService.UpdateAsync(id, inputModel);

            Ensure.That(domainSecondaryObject, nameof(domainSecondaryObject))
                .WithException(_ => new HttpResponseException(HttpStatusCode.NotFound))
                .IsNotNull();

            return this.Map(domainSecondaryObject);
        }

        private ApiModels.SecondaryObject Map(DomainModels.SecondaryObject domainSecondaryObject)
        {
            return new ApiModels.SecondaryObject()
            {
                Description = domainSecondaryObject.Description,
                Id = domainSecondaryObject.Id,
                Name = domainSecondaryObject.Name,
                PrimaryObject = new ApiModels.PrimaryObject()
                {
                    Id = domainSecondaryObject.PrimaryObject_Id,
                },
            };
        }
    }
}
