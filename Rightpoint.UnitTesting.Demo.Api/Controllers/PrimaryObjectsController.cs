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
    [RoutePrefix("api/primaryobjects")]
    public class PrimaryObjectsController : ApiController
    {
        private readonly IPrimaryObjectService _primaryObjectService;

        public PrimaryObjectsController(IPrimaryObjectService primaryObjectService)
        {
            Ensure.That(primaryObjectService, nameof(primaryObjectService)).IsNotNull();

            this._primaryObjectService = primaryObjectService;
        }

        // POST /api/primaryobjects
        [HttpPost]
        [Route("")]
        public async Task<ApiModels.PrimaryObject> CreateAsync(ApiModels.PrimaryObject inputModel)
        {
            var domainPrimaryObject = await _primaryObjectService.CreateAsync(inputModel);

            Ensure.That(domainPrimaryObject, nameof(domainPrimaryObject))
                .WithException(_ => new HttpResponseException(HttpStatusCode.BadRequest))
                .IsNotNull();

            return this.Map(domainPrimaryObject);
        }

        // DELETE /api/primaryobjects/00000000-0000-0000-0000-000000000000
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task DeleteAsync(Guid id)
        {
            await _primaryObjectService.DeleteAsync(id);
        }

        // GET /api/primaryobjects
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ApiModels.PrimaryObject>> GetAllAsync()
        {
            var domainPrimaryObjects = await _primaryObjectService.GetAllAsync();

            Ensure.That(domainPrimaryObjects, nameof(domainPrimaryObjects))
                .WithException(_ => new HttpResponseException(HttpStatusCode.NotFound))
                .IsNotNull();

            return domainPrimaryObjects.Select(this.Map).ToArray();
        }

        // GET /api/primaryobjects/00000000-0000-0000-0000-000000000000
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ApiModels.PrimaryObject> GetAsync(Guid id)
        {
            var domainPrimaryObject = await _primaryObjectService.GetAsync(id);

            Ensure.That(domainPrimaryObject, nameof(domainPrimaryObject))
                .WithException(_ => new HttpResponseException(HttpStatusCode.NotFound))
                .IsNotNull();

            return this.Map(domainPrimaryObject);
        }

        // PUT /api/primaryobjects/00000000-0000-0000-0000-000000000000
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ApiModels.PrimaryObject> UpdateAsync(Guid id, ApiModels.PrimaryObject inputModel)
        {
            var domainPrimaryObject = await _primaryObjectService.UpdateAsync(id, inputModel);

            Ensure.That(domainPrimaryObject, nameof(domainPrimaryObject))
                .WithException(_ => new HttpResponseException(HttpStatusCode.NotFound))
                .IsNotNull();

            return this.Map(domainPrimaryObject);
        }

        private ApiModels.PrimaryObject Map(DomainModels.PrimaryObject domainPrimaryObject)
        {
            return new ApiModels.PrimaryObject()
            {
                Description = domainPrimaryObject.Description,
                Id = domainPrimaryObject.Id,
                Name = domainPrimaryObject.Name,
                SecondaryObjects = domainPrimaryObject.SecondaryObjects
                    .Select(domainSecondaryObject => new ApiModels.SecondaryObject()
                    {
                        Id = domainSecondaryObject.Id,
                    })
                    .ToArray(),
            };
        }
    }
}
