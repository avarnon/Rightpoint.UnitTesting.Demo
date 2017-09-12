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
    public class PrimaryObjectsControllerTests : BaseControllerTests<PrimaryObjectsController>
    {
        private Mock<IPrimaryObjectService> _primaryObjectService;
        private Mock<ISecondaryObjectService> _secondaryObjectService;

        [TestInitialize]
        public void TestInitialize()
        {
            _primaryObjectService = new Mock<IPrimaryObjectService>();
            _secondaryObjectService = new Mock<ISecondaryObjectService>();
            _primaryObjectService.Setup(_ => _.CreateAsync(It.IsAny<Mvc.Models.PrimaryObject>()))
                .ReturnsAsync((Mvc.Models.PrimaryObject po) => new Mvc.Contracts.Models.PrimaryObject()
                {
                    Id = Guid.NewGuid(),
                    Description = po.Description,
                    Name = po.Name,
                    SecondaryObjects = new Mvc.Contracts.Models.SecondaryObject[0],
                });
            _primaryObjectService.Setup(_ => _.GetAllAsync())
                .ReturnsAsync(Enumerable.Range(0, 100).Select(i => new Mvc.Contracts.Models.PrimaryObject()
                {
                    Id = Guid.NewGuid(),
                    Description = $"{i} Description",
                    Name = $"{i} Name",
                    SecondaryObjects = Enumerable.Range(0, 100).Select(j => new Mvc.Contracts.Models.SecondaryObject()
                    {
                        Id = Guid.NewGuid(),
                        Description = $"{i} {j} Description",
                        Name = $"{i} {j} Name",
                    }),
                }).ToArray());
            _primaryObjectService.Setup(_ => _.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new Mvc.Contracts.Models.PrimaryObject()
                {
                    Id = id,
                    Description = $"{id} Description",
                    Name = $"{id} Name",
                    SecondaryObjects = Enumerable.Range(0, 100).Select(i => new Mvc.Contracts.Models.SecondaryObject()
                    {
                        Id = Guid.NewGuid(),
                        Description = $"{id} {i} Description",
                        Name = $"{id} {i} Name",
                    }),
                });
            _primaryObjectService.Setup(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<Mvc.Models.PrimaryObject>()))
                .ReturnsAsync((Guid id, Mvc.Models.PrimaryObject po) => new Mvc.Contracts.Models.PrimaryObject()
                {
                    Id = po.Id,
                    Description = po.Description,
                    Name = po.Name,
                    SecondaryObjects = po.SecondaryObjects.Select(so => new Mvc.Contracts.Models.SecondaryObject()
                    {
                        Id = so.Id,
                        Description = so.Description,
                        Name = so.Name,
                    }),
                });
            _secondaryObjectService.Setup(_ => _.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new Mvc.Contracts.Models.SecondaryObject()
                {
                    Id = id,
                    Description = $"{id} Description",
                    Name = $"{id} Name",
                });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrimaryObjectsController_Constructor_PrimaryObjectService_Null()
        {
            // This test verifies that the controller will not accept null dependencies
            var controller = new PrimaryObjectsController(null, _secondaryObjectService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrimaryObjectsController_Constructor_SecondaryObjectService_Null()
        {
            // This test verifies that the controller will not accept null dependencies
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, null);
        }

        [TestMethod]
        public void PrimaryObjectsController_Constructor_Valid()
        {
            // This test verifies that the controller returns the correct result when IPrimaryObjectService.CreateAsync returns an object
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_GET_Index()
        {
            // This test verifies that Index successfully returns a result
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
            var result = await controller.Index();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PrimaryObjectsController_GET_Create()
        {
            // This test verifies that Create successfully returns a result
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
            var result = controller.Create();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_POST_Create()
        {
            // This test verifies that Create successfully returns a result
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.PrimaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.PrimaryObject.Name)] = "Name";
            var result = await controller.Create(collection);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_POST_Create_Error()
        {
            // This test verifies that Create successfully returns a result when an error is thrown
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.PrimaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.PrimaryObject.Name)] = "Name";
            _primaryObjectService.Setup(_ => _.CreateAsync(It.IsAny<Mvc.Models.PrimaryObject>())).ThrowsAsync(new Exception());
            var result = await controller.Create(collection);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_GET_Edit()
        {
            // This test verifies that Edit successfully returns a result
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
            var result = await controller.Edit(Guid.NewGuid());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_POST_Edit()
        {
            // This test verifies that Edit successfully returns a result
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.PrimaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.PrimaryObject.Name)] = "Name";
            var result = await controller.Edit(Guid.NewGuid(), collection);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_POST_Edit_Error()
        {
            // This test verifies that Edit successfully returns a result when an error is thrown
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.PrimaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.PrimaryObject.Name)] = "Name";
            _primaryObjectService.Setup(_ => _.UpdateAsync(It.IsAny<Guid>(), It.IsAny<Mvc.Models.PrimaryObject>())).ThrowsAsync(new Exception());
            var result = await controller.Edit(Guid.NewGuid(), collection);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_GET_Delete()
        {
            // This test verifies that Delete successfully returns a result
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
            var result = await controller.Delete(Guid.NewGuid());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_POST_Delete()
        {
            // This test verifies that Delete successfully returns a result
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.PrimaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.PrimaryObject.Name)] = "Name";
            var result = await controller.Delete(Guid.NewGuid(), collection);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PrimaryObjectsController_POST_Delete_Error()
        {
            // This test verifies that Delete successfully returns a result when an error is thrown
            var controller = new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
            var collection = new FormCollection();
            collection[nameof(Mvc.Models.PrimaryObject.Description)] = "Description";
            collection[nameof(Mvc.Models.PrimaryObject.Name)] = "Name";
            _primaryObjectService.Setup(_ => _.DeleteAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());
            var result = await controller.Delete(Guid.NewGuid(), collection);
            Assert.IsNotNull(result);
        }

        protected override PrimaryObjectsController GetControllerInstance()
        {
            return new PrimaryObjectsController(_primaryObjectService.Object, _secondaryObjectService.Object);
        }
    }
}
