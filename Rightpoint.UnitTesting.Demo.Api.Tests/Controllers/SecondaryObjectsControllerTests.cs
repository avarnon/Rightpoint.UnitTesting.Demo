using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
using Rightpoint.UnitTesting.Demo.Api.Controllers;
using ApiModels = Rightpoint.UnitTesting.Demo.Api.Models;
using DomainModels = Rightpoint.UnitTesting.Demo.Domain.Models;

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
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SecondaryObjectsController_Constructor_SecondaryObjectService_Null()
        {
            // This test verifies that the controller will not accept null dependencies
            var secondaryObjectsController = new SecondaryObjectsController(null);
        }

        [TestMethod]
        public void SecondaryObjectsController_Constructor_Valid()
        {
            // This test verifies that the controller can be constructed successfully
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_CreateAsync_Valid()
        {
            // This test verifies that the controller returns the correct result when ISecondaryObjectService.CreateAsync returns an object
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
            var sourceSecondaryObject = new ApiModels.SecondaryObject()
            {
                Description = "Description 1",
                Name = "Name 1",
            };

            _secondaryObjectService.Setup(_ => _.CreateAsync(It.IsAny<Guid>(), It.IsAny<ApiModels.SecondaryObject>())).ReturnsAsync(new DomainModels.SecondaryObject(Guid.NewGuid()));

            var destinationSecondaryObject = await secondaryObjectsController.CreateAsync(Guid.NewGuid(), sourceSecondaryObject);
            Assert.IsNotNull(destinationSecondaryObject);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_CreateAsync_NotCreated()
        {
            // This test verifies that the controller throws the correct HttpResponseException when no object is returned from ISecondaryObjectService.CreateAsync
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
            ApiModels.SecondaryObject sourceSecondaryObject = null;

            _secondaryObjectService.Setup(_ => _.CreateAsync(It.IsAny<Guid>(), It.IsAny<ApiModels.SecondaryObject>())).ReturnsAsync(null as DomainModels.SecondaryObject);

            try
            {
                await secondaryObjectsController.CreateAsync(Guid.NewGuid(), sourceSecondaryObject);
                Assert.Fail();
            }
            catch (HttpResponseException ex)
            {
                Assert.IsNotNull(ex.Response);
                Assert.AreEqual(HttpStatusCode.BadRequest, ex.Response.StatusCode);
            }
        }

        [TestMethod]
        public async Task SecondaryObjectsController_DeleteAsync()
        {
            // This test verifies that the controller does not throw an error when deleting an object
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
            await secondaryObjectsController.DeleteAsync(Guid.NewGuid());
        }

        [TestMethod]
        public async Task SecondaryObjectsController_GetAllAsync_Found()
        {
            // This test verifies that the controller does not throw an error when getting all objects
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
            var source = new List<DomainModels.SecondaryObject>()
            {
                new DomainModels.SecondaryObject(Guid.NewGuid())
                {
                    Description = "Description 1.1",
                    Name = "Name 1.1",
                    PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                    {
                        Description = "Description 1",
                        Name = "Name 1",
                    },
                },
                new DomainModels.SecondaryObject(Guid.NewGuid())
                {
                    Description = "Description 2.1",
                    Name = "Name 2.1",
                    PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                    {
                        Description = "Description 2",
                        Name = "Name 2",
                    },
                },
            };

            foreach (var sourceSecondaryObject in source)
            {
                sourceSecondaryObject.PrimaryObject_Id = sourceSecondaryObject.PrimaryObject.Id;
                sourceSecondaryObject.PrimaryObject.SecondaryObjects = new List<DomainModels.SecondaryObject>() { sourceSecondaryObject };
            }

            _secondaryObjectService.Setup(_ => _.GetAllAsync()).ReturnsAsync(source);

            var destination = await secondaryObjectsController.GetAllAsync();
            Assert.IsNotNull(destination);
            foreach (var destinationSecondaryObject in destination)
            {
                var sourceSecondaryObject = source.SingleOrDefault(_ => _.Id == destinationSecondaryObject.Id);
                Assert.IsNotNull(source);
                Assert.AreEqual(sourceSecondaryObject.Description, destinationSecondaryObject.Description);
                Assert.AreEqual(sourceSecondaryObject.Id, destinationSecondaryObject.Id);
                Assert.AreEqual(sourceSecondaryObject.Name, destinationSecondaryObject.Name);
                Assert.IsNotNull(destinationSecondaryObject.PrimaryObject);
                Assert.IsNull(destinationSecondaryObject.PrimaryObject.Description);
                Assert.AreEqual(sourceSecondaryObject.PrimaryObject.Id, destinationSecondaryObject.PrimaryObject.Id);
                Assert.IsNull(destinationSecondaryObject.PrimaryObject.Name);
                Assert.IsNull(destinationSecondaryObject.PrimaryObject.SecondaryObjects);
            }
        }

        [TestMethod]
        public async Task SecondaryObjectsController_GetAllAsync_NotFound()
        {
            // This test verifies that the controller does not throw an error when getting all objects and null is returned from the service
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
            ICollection<DomainModels.SecondaryObject> source = null;

            _secondaryObjectService.Setup(_ => _.GetAllAsync()).ReturnsAsync(source);

            try
            {
                await secondaryObjectsController.GetAllAsync();
                Assert.Fail();
            }
            catch (HttpResponseException ex)
            {
                Assert.IsNotNull(ex.Response);
                Assert.AreEqual(HttpStatusCode.NotFound, ex.Response.StatusCode);
            }
        }

        [TestMethod]
        public async Task SecondaryObjectsController_GetAsync_Found()
        {
            // This test verifies that the controller does not throw an error when get returns an object
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
            var sourceSecondaryObject = new DomainModels.SecondaryObject(Guid.NewGuid())
            {
                Description = "Description 1.1",
                Name = "Name 1.1",
                PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                {
                    Description = "Description 1",
                    Name = "Name 1",
                },
            };
            sourceSecondaryObject.PrimaryObject_Id = sourceSecondaryObject.PrimaryObject.Id;
            sourceSecondaryObject.PrimaryObject.SecondaryObjects = new List<DomainModels.SecondaryObject>() { sourceSecondaryObject };

            _secondaryObjectService.Setup(_ => _.GetAsync(sourceSecondaryObject.Id)).ReturnsAsync(sourceSecondaryObject);

            var destinationSecondaryObject = await secondaryObjectsController.GetAsync(sourceSecondaryObject.Id);
            Assert.IsNotNull(destinationSecondaryObject);
            Assert.AreEqual(sourceSecondaryObject.Description, destinationSecondaryObject.Description);
            Assert.AreEqual(sourceSecondaryObject.Id, destinationSecondaryObject.Id);
            Assert.AreEqual(sourceSecondaryObject.Name, destinationSecondaryObject.Name);
            Assert.IsNotNull(destinationSecondaryObject.PrimaryObject);
            Assert.IsNull(destinationSecondaryObject.PrimaryObject.Description);
            Assert.AreEqual(sourceSecondaryObject.PrimaryObject.Id, destinationSecondaryObject.PrimaryObject.Id);
            Assert.IsNull(destinationSecondaryObject.PrimaryObject.Name);
            Assert.IsNull(destinationSecondaryObject.PrimaryObject.SecondaryObjects);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_GetAsync_NotFound()
        {
            // This test verifies that the controller throws the correct HttpResponseException when get does not return an object
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
            DomainModels.SecondaryObject sourceSecondaryObject = null;

            _secondaryObjectService.Setup(_ => _.GetAsync(It.IsAny<Guid>())).ReturnsAsync(sourceSecondaryObject);

            try
            {
                await secondaryObjectsController.GetAsync(Guid.NewGuid());
                Assert.Fail();
            }
            catch (HttpResponseException ex)
            {
                Assert.IsNotNull(ex.Response);
                Assert.AreEqual(HttpStatusCode.NotFound, ex.Response.StatusCode);
            }
        }

        [TestMethod]
        public async Task SecondaryObjectsController_UpdateAsync_Found()
        {
            // This test verifies that the controller does not throw an error when updating a valid object
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
            var sourceSecondaryObject = new DomainModels.SecondaryObject(Guid.NewGuid())
            {
                Description = "Description 1.1",
                Name = "Name 1.1",
                PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                {
                    Description = "Description 1",
                    Name = "Name 1",
                },
            };
            sourceSecondaryObject.PrimaryObject_Id = sourceSecondaryObject.PrimaryObject.Id;
            sourceSecondaryObject.PrimaryObject.SecondaryObjects = new List<DomainModels.SecondaryObject>() { sourceSecondaryObject };

            _secondaryObjectService.Setup(_ => _.UpdateAsync(sourceSecondaryObject.Id, It.IsAny<ApiModels.SecondaryObject>())).ReturnsAsync(sourceSecondaryObject);

            var destinationSecondaryObject = await secondaryObjectsController.UpdateAsync(sourceSecondaryObject.Id, new ApiModels.SecondaryObject());
            Assert.IsNotNull(destinationSecondaryObject);
            Assert.AreEqual(sourceSecondaryObject.Description, destinationSecondaryObject.Description);
            Assert.AreEqual(sourceSecondaryObject.Id, destinationSecondaryObject.Id);
            Assert.AreEqual(sourceSecondaryObject.Name, destinationSecondaryObject.Name);
            Assert.IsNotNull(destinationSecondaryObject.PrimaryObject);
            Assert.IsNull(destinationSecondaryObject.PrimaryObject.Description);
            Assert.AreEqual(sourceSecondaryObject.PrimaryObject.Id, destinationSecondaryObject.PrimaryObject.Id);
            Assert.IsNull(destinationSecondaryObject.PrimaryObject.Name);
            Assert.IsNull(destinationSecondaryObject.PrimaryObject.SecondaryObjects);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_UpdateAsync_NotFound()
        {
            // This test verifies that the controller throws the correct HttpResponseException when a non-existant object is updated
            var secondaryObjectsController = new SecondaryObjectsController(_secondaryObjectService.Object);
            DomainModels.SecondaryObject sourceSecondaryObject = null;

            _secondaryObjectService.Setup(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<ApiModels.SecondaryObject>())).ReturnsAsync(sourceSecondaryObject);

            try
            {
                await secondaryObjectsController.UpdateAsync(Guid.NewGuid(), new ApiModels.SecondaryObject());
                Assert.Fail();
            }
            catch (HttpResponseException ex)
            {
                Assert.IsNotNull(ex.Response);
                Assert.AreEqual(HttpStatusCode.NotFound, ex.Response.StatusCode);
            }
        }
    }
}
