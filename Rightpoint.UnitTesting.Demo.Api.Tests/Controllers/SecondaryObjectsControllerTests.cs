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
    public class SecondaryObjectsControllerTests
    {
        private Mock<ISecondaryObjectService> _secondaryObjectService;

        [TestInitialize]
        public void TestInitialize()
        {
            _secondaryObjectService = new Mock<ISecondaryObjectService>();
            _secondaryObjectService.Setup(_ => _.GetAllAsync())
                .ReturnsAsync(new List<SecondaryObject>());
            _secondaryObjectService.Setup(_ => _.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var primaryObjectId = Guid.NewGuid();
                    var primaryObject = new PrimaryObject(primaryObjectId)
                    {
                        Name = $"Name {id}",
                        Description = $"Description {id}",
                        SecondaryObjects = new List<SecondaryObject>()
                        {
                            new SecondaryObject(id),
                        },
                    };
                    foreach (var secondaryObject in primaryObject.SecondaryObjects)
                    {
                        secondaryObject.Name = $"Name {secondaryObject.Id}";
                        secondaryObject.Description = $"Description {secondaryObject.Id}";
                        secondaryObject.PrimaryObject = primaryObject;
                        secondaryObject.PrimaryObject_Id = primaryObject.Id;
                    }

                    return primaryObject.SecondaryObjects.Single();
                });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SecondaryObjectsController_Constructor_SecondaryObjectService_Null()
        {
            var secondaryObjectsController = new SecondaryObjectsController(null);
            Assert.Fail("Expected exception was not thrown");
        }

        [TestMethod]
        public void SecondaryObjectsController_Constructor_Valid()
        {
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_GetAllAsync()
        {
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
            var results = await secondaryObjectsController.GetAllAsync();
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_GetAsync()
        {
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
            var result = await secondaryObjectsController.GetAsync(Guid.NewGuid());
            Assert.IsNotNull(result);
        }
    }
}
