using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using EnsureThat;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Controllers
{
    [RoutePrefix("api/secondaryobjects")]
    public class SecondaryObjectsController : ApiController
    {
        private readonly ISecondaryObjectService _secondaryObjectService;

        public SecondaryObjectsController(ISecondaryObjectService secondaryObjectService)
        {
            Ensure.That(secondaryObjectService, nameof(secondaryObjectService)).IsNotNull();

            this._secondaryObjectService = secondaryObjectService;
        }

        // GET /api/secondaryobjects
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<SecondaryObject>> GetAllAsync()
        {
            return await _secondaryObjectService.GetAllAsync();
        }

        // GET /api/secondaryobjects/00000000-0000-0000-0000-000000000000
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<SecondaryObject> GetAsync(Guid id)
        {
            return await _secondaryObjectService.GetAsync(id);
        }
    }
}
