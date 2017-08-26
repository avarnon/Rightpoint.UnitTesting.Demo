using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
using Rightpoint.UnitTesting.Demo.Api.Controllers;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Tests.Controllers
{
    [TestClass]
    public class PrimaryObjectsControllerTests
    {
        private Mock<IPrimaryObjectService> _primaryObjectService;

        [TestInitialize]
        public void TestInitialize()
        {
            _primaryObjectService = new Mock<IPrimaryObjectService>();
            _primaryObjectService.Setup(_ => _.GetAllAsync())
                .ReturnsAsync(new List<PrimaryObject>());
            _primaryObjectService.Setup(_ => _.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var primaryObject = new PrimaryObject(id)
                    {
                        Name = $"Name {id}",
                        Description = $"Description {id}",
                        SecondaryObjects = Enumerable.Range(1, 10).Select(i => new SecondaryObject(Guid.NewGuid())).ToList(),
                    };

                    foreach (var secondaryObject in primaryObject.SecondaryObjects)
                    {
                        secondaryObject.Name = $"Name {secondaryObject.Id}";
                        secondaryObject.Description = $"Description {secondaryObject.Id}";
                        secondaryObject.PrimaryObject = primaryObject;
                        secondaryObject.PrimaryObject_Id = primaryObject.Id;
                    }

                    return primaryObject;
                });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrimaryObjectsController_Constructor_PrimaryObjectService_Null()
        {
            var primaryObjectsController = new PrimaryObjectsController(null);
            Assert.Fail("Expected exception was not thrown");
        }

        [TestMethod]
        public void PrimaryObjectsController_Constructor_Valid()
        {
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_GetAllAsync()
        {
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
            var results = await primaryObjectsController.GetAllAsync();
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_GetAsync()
        {
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
            var result = await primaryObjectsController.GetAsync(Guid.NewGuid());
            Assert.IsNotNull(result);
        }
    }
}
