using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rightpoint.UnitTesting.Demo.Mvc.Contracts;
using Rightpoint.UnitTesting.Demo.Mvc.Controllers;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.Controllers
{
    [TestClass]
    public class SecondaryObjectsControllerTests : BaseControllerTests<SecondaryObjectsController>
    {
        private Mock<ISecondaryObjectService> _secondaryObjectService;

        [TestInitialize]
        public void TestInitialize()
        {
            _secondaryObjectService = new Mock<ISecondaryObjectService>();
            _secondaryObjectService.Setup(_ => _.CreateAsync(It.IsAny<Guid>(), It.IsAny<Mvc.Models.SecondaryObject>()))
                .ReturnsAsync((Guid primaryObjectId, Mvc.Models.SecondaryObject po) => new Mvc.Contracts.Models.SecondaryObject()
                {
                    Id = Guid.NewGuid(),
                    Description = po.Description,
                    Name = po.Name,
                    PrimaryObject = new Mvc.Contracts.Models.PrimaryObject() { Id = primaryObjectId, },
                });
            _secondaryObjectService.Setup(_ => _.GetAllAsync())
                .ReturnsAsync(Enumerable.Range(0, 100).Select(i => new Mvc.Contracts.Models.SecondaryObject()
                {
                    Id = Guid.NewGuid(),
                    Description = $"{i} Description",
                    Name = $"{i} Name",
                    PrimaryObject = new Mvc.Contracts.Models.PrimaryObject() { Id = Guid.NewGuid(), },
                }).ToArray());
            _secondaryObjectService.Setup(_ => _.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new Mvc.Contracts.Models.SecondaryObject()
                {
                    Id = id,
                    Description = $"{id} Description",
                    Name = $"{id} Name",
                    PrimaryObject = new Mvc.Contracts.Models.PrimaryObject() { Id = Guid.NewGuid(), },
                });
            _secondaryObjectService.Setup(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<Mvc.Models.SecondaryObject>()))
                .ReturnsAsync((Guid id, Mvc.Models.SecondaryObject po) => new Mvc.Contracts.Models.SecondaryObject()
                {
                    Id = po.Id,
                    Description = po.Description,
                    Name = po.Name,
                    PrimaryObject = new Mvc.Contracts.Models.PrimaryObject() { Id = Guid.NewGuid(), },
                });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SecondaryObjectsController_Constructor_SecondaryObjectService_Null()
        {
            // This test verifies that the controller will not accept null dependencies
            var controller = new SecondaryObjectsController(null);
        }

        [TestMethod]
        public void SecondaryObjectsController_Constructor_Valid()
        {
            // This test verifies that the controller returns the correct result when IPrimaryObjectService.CreateAsync returns an object
            var controller = new SecondaryObjectsController(_secondaryObjectService.Object);
        }

        [TestMethod]
        public void SecondaryObjectsController_GET_Create()
        {
            // This test verifies that Create successfully returns a result
            var controller = new SecondaryObjectsController(_secondaryObjectService.Object);
            var result = controller.Create(Guid.NewGuid());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_POST_Create()
        {
            // This test verifies that Create successfully returns a result
            var controller = new SecondaryObjectsController(_secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.SecondaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.SecondaryObject.Name)] = "Name";
            var result = await controller.Create(Guid.NewGuid(), collection);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_POST_Create_Error()
        {
            // This test verifies that Create successfully returns a result when an error is thrown
            var controller = new SecondaryObjectsController(_secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.PrimaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.PrimaryObject.Name)] = "Name";
            _secondaryObjectService.Setup(_ => _.CreateAsync(It.IsAny<Guid>(), It.IsAny<Mvc.Models.SecondaryObject>())).ThrowsAsync(new Exception());
            var result = await controller.Create(Guid.NewGuid(), collection);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_GET_Edit()
        {
            // This test verifies that Edit successfully returns a result
            var controller = new SecondaryObjectsController(_secondaryObjectService.Object);
            var result = await controller.Edit(Guid.NewGuid());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_POST_Edit()
        {
            // This test verifies that Edit successfully returns a result
            var controller = new SecondaryObjectsController(_secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.SecondaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.SecondaryObject.Name)] = "Name";
            var result = await controller.Edit(Guid.NewGuid(), collection);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_POST_Edit_Error()
        {
            // This test verifies that Edit successfully returns a result when an error is thrown
            var controller = new SecondaryObjectsController(_secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.PrimaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.PrimaryObject.Name)] = "Name";
            _secondaryObjectService.Setup(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<Mvc.Models.SecondaryObject>())).ThrowsAsync(new Exception());
            var result = await controller.Edit(Guid.NewGuid(), collection);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_GET_Delete()
        {
            // This test verifies that Delete successfully returns a result
            var controller = new SecondaryObjectsController(_secondaryObjectService.Object);
            var result = await controller.Delete(Guid.NewGuid());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_POST_Delete()
        {
            // This test verifies that Delete successfully returns a result
            var controller = new SecondaryObjectsController(_secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.SecondaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.SecondaryObject.Name)] = "Name";
            var result = await controller.Delete(Guid.NewGuid(), collection);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SecondaryObjectsController_POST_Delete_Error()
        {
            // This test verifies that Delete successfully returns a result when an error is thrown
            var controller = new SecondaryObjectsController(_secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.PrimaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.PrimaryObject.Name)] = "Name";
            _secondaryObjectService.Setup(_ => _.DeleteAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());
            var result = await controller.Delete(Guid.NewGuid(), collection);
            Assert.IsNotNull(result);
        }

        protected override SecondaryObjectsController GetControllerInstance()
        {
            return new SecondaryObjectsController(_secondaryObjectService.Object);
        }
    }
}
