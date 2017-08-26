using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
using Rightpoint.UnitTesting.Demo.Domain.Models;

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
        public async Task<IEnumerable<PrimaryObject>> GetAllAsync()
        {
            return await _primaryObjectService.GetAllAsync();
        }

        // GET /api/primaryobjects/00000000-0000-0000-0000-000000000000
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<PrimaryObject> GetAsync(Guid id)
        {
            return await _primaryObjectService.GetAsync(id);
        }
    }
}
