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
    public class PrimaryObjectsControllerTests
    {
        private Mock<IPrimaryObjectService> _primaryObjectService;

        [TestInitialize]
        public void TestInitialize()
        {
            _primaryObjectService = new Mock<IPrimaryObjectService>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrimaryObjectsController_Constructor_PrimaryObjectService_Null()
        {
            // This test verifies that the controller will not accept null dependencies
            var primaryObjectsController = new PrimaryObjectsController(null);
        }

        [TestMethod]
        public void PrimaryObjectsController_Constructor_Valid()
        {
            // This test verifies that the controller can be constructed successfully
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_CreateAsync_Valid()
        {
            // This test verifies that the controller returns the correct result when IPrimaryObjectService.CreateAsync returns an object
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
            var sourcePrimaryObject = new ApiModels.PrimaryObject()
            {
                Description = "Description 1",
                Name = "Name 1",
            };

            _primaryObjectService.Setup(_ => _.CreateAsync(It.IsAny<ApiModels.PrimaryObject>())).ReturnsAsync(new DomainModels.PrimaryObject(Guid.NewGuid()));

            var destinationPrimaryObject = await primaryObjectsController.CreateAsync(sourcePrimaryObject);
            Assert.IsNotNull(destinationPrimaryObject);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_CreateAsync_NotCreated()
        {
            // This test verifies that the controller throws the correct HttpResponseException when no object is returned from IPrimaryObjectService.CreateAsync
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
            ApiModels.PrimaryObject sourcePrimaryObject = null;

            _primaryObjectService.Setup(_ => _.CreateAsync(It.IsAny<ApiModels.PrimaryObject>())).ReturnsAsync(null as DomainModels.PrimaryObject);

            try
            {
                await primaryObjectsController.CreateAsync(sourcePrimaryObject);
                Assert.Fail();
            }
            catch (HttpResponseException ex)
            {
                Assert.IsNotNull(ex.Response);
                Assert.AreEqual(HttpStatusCode.BadRequest, ex.Response.StatusCode);
            }
        }

        [TestMethod]
        public async Task PrimaryObjectsController_DeleteAsync()
        {
            // This test verifies that the controller does not throw an error when deleting an object
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
            await primaryObjectsController.DeleteAsync(Guid.NewGuid());
        }

        [TestMethod]
        public async Task PrimaryObjectsController_GetAllAsync_Found()
        {
            // This test verifies that the controller does not throw an error when getting all objects
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
            var source = new List<DomainModels.PrimaryObject>()
            {
                new DomainModels.PrimaryObject(Guid.NewGuid())
                {
                    Description = "Description 1",
                    Name = "Name 1",
                    SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1.1",
                            Name = "Name 1.1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1.2",
                            Name = "Name 1.2",
                        },
                    },
                },
                new DomainModels.PrimaryObject(Guid.NewGuid())
                {
                    Description = "Description 2",
                    Name = "Name 2",
                    SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 2.1",
                            Name = "Name 2.1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 2.2",
                            Name = "Name 2.2",
                        },
                    },
                },
            };

            foreach (var sourcePrimaryObject in source)
            {
                foreach (var sourceSecondaryObject in sourcePrimaryObject.SecondaryObjects)
                {
                    sourceSecondaryObject.PrimaryObject = sourcePrimaryObject;
                    sourceSecondaryObject.PrimaryObject_Id = sourcePrimaryObject.Id;
                }
            }

            _primaryObjectService.Setup(_ => _.GetAllAsync()).ReturnsAsync(source);

            var destination = await primaryObjectsController.GetAllAsync();
            Assert.IsNotNull(destination);
            foreach (var destinationPrimaryObject in destination)
            {
                var sourcePrimaryObject = source.SingleOrDefault(_ => _.Id == destinationPrimaryObject.Id);
                Assert.IsNotNull(source);
                Assert.AreEqual(sourcePrimaryObject.Description, destinationPrimaryObject.Description);
                Assert.AreEqual(sourcePrimaryObject.Id, destinationPrimaryObject.Id);
                Assert.AreEqual(sourcePrimaryObject.Name, destinationPrimaryObject.Name);
                Assert.IsNotNull(destinationPrimaryObject.SecondaryObjects);
                Assert.AreEqual(sourcePrimaryObject.SecondaryObjects.Count, destinationPrimaryObject.SecondaryObjects.Count());
                foreach (var destinationSecondaryObject in destinationPrimaryObject.SecondaryObjects)
                {
                    var sourceSecondaryObject = sourcePrimaryObject.SecondaryObjects.SingleOrDefault(_ => _.Id == destinationSecondaryObject.Id);
                    Assert.IsNotNull(sourceSecondaryObject);
                    Assert.IsNull(destinationSecondaryObject.Description);
                    Assert.AreEqual(sourceSecondaryObject.Id, destinationSecondaryObject.Id);
                    Assert.IsNull(destinationSecondaryObject.Name);
                    Assert.IsNull(destinationSecondaryObject.PrimaryObject);
                }
            }
        }

        [TestMethod]
        public async Task PrimaryObjectsController_GetAllAsync_NotFound()
        {
            // This test verifies that the controller does not throw an error when getting all objects and null is returned from the service
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
            ICollection<DomainModels.PrimaryObject> source = null;

            _primaryObjectService.Setup(_ => _.GetAllAsync()).ReturnsAsync(source);

            try
            {
                await primaryObjectsController.GetAllAsync();
                Assert.Fail();
            }
            catch (HttpResponseException ex)
            {
                Assert.IsNotNull(ex.Response);
                Assert.AreEqual(HttpStatusCode.NotFound, ex.Response.StatusCode);
            }
        }

        [TestMethod]
        public async Task PrimaryObjectsController_GetAsync_Found()
        {
            // This test verifies that the controller does not throw an error when get returns an object
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
            var sourcePrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
            {
                Description = "Description 1",
                Name = "Name 1",
                SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1.1",
                            Name = "Name 1.1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1.2",
                            Name = "Name 1.2",
                        },
                    },
            };
            foreach (var sourceSecondaryObject in sourcePrimaryObject.SecondaryObjects)
            {
                sourceSecondaryObject.PrimaryObject = sourcePrimaryObject;
                sourceSecondaryObject.PrimaryObject_Id = sourcePrimaryObject.Id;
            }

            _primaryObjectService.Setup(_ => _.GetAsync(sourcePrimaryObject.Id)).ReturnsAsync(sourcePrimaryObject);

            var destinationPrimaryObject = await primaryObjectsController.GetAsync(sourcePrimaryObject.Id);
            Assert.IsNotNull(destinationPrimaryObject);
            Assert.AreEqual(sourcePrimaryObject.Description, destinationPrimaryObject.Description);
            Assert.AreEqual(sourcePrimaryObject.Id, destinationPrimaryObject.Id);
            Assert.AreEqual(sourcePrimaryObject.Name, destinationPrimaryObject.Name);
            Assert.IsNotNull(destinationPrimaryObject.SecondaryObjects);
            Assert.AreEqual(sourcePrimaryObject.SecondaryObjects.Count, destinationPrimaryObject.SecondaryObjects.Count());
            foreach (var destinationSecondaryObject in destinationPrimaryObject.SecondaryObjects)
            {
                var sourceSecondaryObject = sourcePrimaryObject.SecondaryObjects.SingleOrDefault(_ => _.Id == destinationSecondaryObject.Id);
                Assert.IsNotNull(sourceSecondaryObject);
                Assert.IsNull(destinationSecondaryObject.Description);
                Assert.AreEqual(sourceSecondaryObject.Id, destinationSecondaryObject.Id);
                Assert.IsNull(destinationSecondaryObject.Name);
                Assert.IsNull(destinationSecondaryObject.PrimaryObject);
            }
        }

        [TestMethod]
        public async Task PrimaryObjectsController_GetAsync_NotFound()
        {
            // This test verifies that the controller throws the correct HttpResponseException when get does not return an object
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
            DomainModels.PrimaryObject sourcePrimaryObject = null;

            _primaryObjectService.Setup(_ => _.GetAsync(It.IsAny<Guid>())).ReturnsAsync(sourcePrimaryObject);

            try
            {
                await primaryObjectsController.GetAsync(Guid.NewGuid());
                Assert.Fail();
            }
            catch (HttpResponseException ex)
            {
                Assert.IsNotNull(ex.Response);
                Assert.AreEqual(HttpStatusCode.NotFound, ex.Response.StatusCode);
            }
        }

        [TestMethod]
        public async Task PrimaryObjectsController_UpdateAsync_Found()
        {
            // This test verifies that the controller does not throw an error when updating a valid object
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
            var sourcePrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
            {
                Description = "Description 1",
                Name = "Name 1",
                SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1.1",
                            Name = "Name 1.1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1.2",
                            Name = "Name 1.2",
                        },
                    },
            };
            foreach (var sourceSecondaryObject in sourcePrimaryObject.SecondaryObjects)
            {
                sourceSecondaryObject.PrimaryObject = sourcePrimaryObject;
                sourceSecondaryObject.PrimaryObject_Id = sourcePrimaryObject.Id;
            }

            _primaryObjectService.Setup(_ => _.UpdateAsync(sourcePrimaryObject.Id, It.IsAny<ApiModels.PrimaryObject>())).ReturnsAsync(sourcePrimaryObject);

            var destinationPrimaryObject = await primaryObjectsController.UpdateAsync(sourcePrimaryObject.Id, new ApiModels.PrimaryObject());
            Assert.IsNotNull(destinationPrimaryObject);
            Assert.AreEqual(sourcePrimaryObject.Description, destinationPrimaryObject.Description);
            Assert.AreEqual(sourcePrimaryObject.Id, destinationPrimaryObject.Id);
            Assert.AreEqual(sourcePrimaryObject.Name, destinationPrimaryObject.Name);
            Assert.IsNotNull(destinationPrimaryObject.SecondaryObjects);
            Assert.AreEqual(sourcePrimaryObject.SecondaryObjects.Count, destinationPrimaryObject.SecondaryObjects.Count());
            foreach (var destinationSecondaryObject in destinationPrimaryObject.SecondaryObjects)
            {
                var sourceSecondaryObject = sourcePrimaryObject.SecondaryObjects.SingleOrDefault(_ => _.Id == destinationSecondaryObject.Id);
                Assert.IsNotNull(sourceSecondaryObject);
                Assert.IsNull(destinationSecondaryObject.Description);
                Assert.AreEqual(sourceSecondaryObject.Id, destinationSecondaryObject.Id);
                Assert.IsNull(destinationSecondaryObject.Name);
                Assert.IsNull(destinationSecondaryObject.PrimaryObject);
            }
        }

        [TestMethod]
        public async Task PrimaryObjectsController_UpdateAsync_NotFound()
        {
            // This test verifies that the controller throws the correct HttpResponseException when a non-existant object is updated
            var primaryObjectsController = new PrimaryObjectsController(_primaryObjectService.Object);
            DomainModels.PrimaryObject sourcePrimaryObject = null;

            _primaryObjectService.Setup(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<ApiModels.PrimaryObject>())).ReturnsAsync(sourcePrimaryObject);

            try
            {
                await primaryObjectsController.UpdateAsync(Guid.NewGuid(), new ApiModels.PrimaryObject());
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
