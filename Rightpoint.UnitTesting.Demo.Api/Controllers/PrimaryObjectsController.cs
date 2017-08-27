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

        // GET /api/primaryobjects
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ApiModels.PrimaryObject>> GetAllAsync()
        {
            var domainPrimaryObjects = await _primaryObjectService.GetAllAsync();

            Ensure.That(domainPrimaryObjects, nameof(domainPrimaryObjects))
                .WithException(_ => new HttpResponseException(HttpStatusCode.NotFound))
                .IsNotNull();

            return domainPrimaryObjects.Select(domainPrimaryObject => new ApiModels.PrimaryObject()
            {
                Description = domainPrimaryObject.Description,
                Id = domainPrimaryObject.Id,
                Name = domainPrimaryObject.Name,
                SecondaryObjects = domainPrimaryObject.SecondaryObjects
                    ?.Select(domainSecondaryObject => new ApiModels.SecondaryObject()
                    {
                        Id = domainSecondaryObject.Id,
                    })
                    ?.ToArray(),
            })
            ?.ToArray();
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

            return new ApiModels.PrimaryObject()
            {
                Description = domainPrimaryObject.Description,
                Id = domainPrimaryObject.Id,
                Name = domainPrimaryObject.Name,
                SecondaryObjects = domainPrimaryObject.SecondaryObjects
                    ?.Select(domainSecondaryObject => new ApiModels.SecondaryObject()
                    {
                        Id = domainSecondaryObject.Id,
                    })
                    ?.ToArray(),
            };
        }
    }
}
