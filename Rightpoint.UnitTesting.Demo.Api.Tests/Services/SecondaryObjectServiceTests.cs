using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rightpoint.UnitTesting.Demo.Api.Services;
using Rightpoint.UnitTesting.Demo.Common.Exceptions;
using Rightpoint.UnitTesting.Demo.Domain.Repositories;
using ApiModels = Rightpoint.UnitTesting.Demo.Api.Models;
using DomainModels = Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Tests.Services
{
    [TestClass]
    public class SecondaryObjectServiceTests
    {
        private Mock<IPrimaryObjectRepository> _primaryObjectRepository;
        private Mock<ISecondaryObjectRepository> _secondaryObjectRepository;
        private Mock<IUnitOfWork> _unitOfWork;

        [TestInitialize]
        public void TestInitialize()
        {
            _primaryObjectRepository = new Mock<IPrimaryObjectRepository>();
            _secondaryObjectRepository = new Mock<ISecondaryObjectRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [TestMethod]
        public void SecondaryObjectService_Constructor_Valid()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SecondaryObjectService_Constructor_PrimaryObjectRepository_Null()
        {
            var secondaryObjectService = new SecondaryObjectService(null, _secondaryObjectRepository.Object, _unitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SecondaryObjectService_Constructor_SecondaryObjectRepository_Null()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, null, _unitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SecondaryObjectService_Constructor_UnitOfWork_Null()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, null);
        }

        [TestMethod]
        public async Task SecondaryObjectService_CreateAsync_Valid()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = "New Name",
            };
            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id));
            var destination = await secondaryObjectService.CreateAsync(Guid.NewGuid(), source);
            Assert.IsNotNull(destination);
            Assert.AreEqual(source.Description, destination.Description);
            Assert.AreNotEqual(Guid.Empty, destination.Id);
            Assert.AreEqual(source.Name, destination.Name);
            Assert.IsNotNull(destination.PrimaryObject);
            _unitOfWork.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SecondaryObjectService_CreateAsync_InputModel_Null()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            ApiModels.SecondaryObject source = null;
            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id));
            var destination = await secondaryObjectService.CreateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_CreateAsync_InputModel_Description_Null()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = null,
                Name = "New Name",
            };
            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id));
            var destination = await secondaryObjectService.CreateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_CreateAsync_InputModel_Description_Empty()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = string.Empty,
                Name = "New Name",
            };
            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id));
            var destination = await secondaryObjectService.CreateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_CreateAsync_InputModel_Description_WhiteSpace()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "     ",
                Name = "New Name",
            };
            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id));
            var destination = await secondaryObjectService.CreateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_CreateAsync_InputModel_Name_Null()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = null,
            };
            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id));
            var destination = await secondaryObjectService.CreateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_CreateAsync_InputModel_Name_Empty()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = string.Empty,
            };
            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id));
            var destination = await secondaryObjectService.CreateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_CreateAsync_InputModel_Name_WhiteSpace()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = "     ",
            };
            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id));
            var destination = await secondaryObjectService.CreateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SecondaryObjectService_CreateAsync_PrimaryObjectId_Empty()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = "New Name",
            };
            var destination = await secondaryObjectService.CreateAsync(Guid.Empty, source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoEntityNotFoundException))]
        public async Task SecondaryObjectService_CreateAsync_PrimaryObject_NotFound()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = "New Name",
            };
            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as DomainModels.PrimaryObject);
            var destination = await secondaryObjectService.CreateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        public async Task SecondaryObjectService_DeleteAsync_Valid()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = Guid.NewGuid();
            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new DomainModels.SecondaryObject(id));
            await secondaryObjectService.DeleteAsync(source);
            _unitOfWork.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SecondaryObjectService_DeleteAsync_Id_Empty()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            await secondaryObjectService.DeleteAsync(Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoEntityNotFoundException))]
        public async Task SecondaryObjectService_DeleteAsync_NotFound()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = Guid.NewGuid();
            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as DomainModels.SecondaryObject);
            await secondaryObjectService.DeleteAsync(source);
        }

        [TestMethod]
        public async Task SecondaryObjectService_GetAllAsync_Valid()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            _secondaryObjectRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(new List<DomainModels.SecondaryObject>());
            var destination = await secondaryObjectService.GetAllAsync();
            Assert.IsNotNull(destination);
            _unitOfWork.Verify(_ => _.SaveChangesAsync(), Times.Never);
        }

        [TestMethod]
        public async Task SecondaryObjectService_GetByIdAsync_Valid()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new DomainModels.SecondaryObject(Guid.NewGuid())
            {
                Description = "Description 1",
                Name = "Name 1",
                PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                {
                    Description = "Description",
                    Name = "Name",
                },
            };
            source.PrimaryObject_Id = source.PrimaryObject.Id;
            source.PrimaryObject.SecondaryObjects = new List<DomainModels.SecondaryObject>() { source };

            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(source.Id)).ReturnsAsync(source);
            var destination = await secondaryObjectService.GetAsync(source.Id);
            Assert.AreEqual(source, destination);

            _unitOfWork.Verify(_ => _.SaveChangesAsync(), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SecondaryObjectService_GetByIdAsync_Id_Empty()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var destination = await secondaryObjectService.GetAsync(Guid.Empty);
        }

        [TestMethod]
        public async Task SecondaryObjectService_GetByIdAsync_NotFound()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = Guid.NewGuid();
            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as DomainModels.SecondaryObject);
            var destination = await secondaryObjectService.GetAsync(source);
            Assert.IsNull(destination);
        }

        [TestMethod]
        public async Task SecondaryObjectService_UpdateAsync_Valid()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = "New Name",
            };

            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.SecondaryObject(id)
                {
                    Description = "Description 1",
                    Name = "Name 1",
                    PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                    {
                        Description = "Description",
                        Name = "Name",
                    },
                });
            var destination = await secondaryObjectService.UpdateAsync(Guid.NewGuid(), source);
            Assert.IsNotNull(destination);
            Assert.AreEqual(source.Description, destination.Description);
            Assert.AreNotEqual(Guid.Empty, destination.Id);
            Assert.AreEqual(source.Name, destination.Name);
            Assert.IsNotNull(destination.PrimaryObject);
            _unitOfWork.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SecondaryObjectService_UpdateAsync_Id_Empty()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = "New Name",
            };

            await secondaryObjectService.UpdateAsync(Guid.Empty, source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SecondaryObjectService_UpdateAsync_InputModel_Null()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            ApiModels.SecondaryObject source = null;

            await secondaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_UpdateAsync_InputModel_Description_Null()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = null,
                Name = "New Name",
            };

            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.SecondaryObject(id)
                {
                    Description = "Description 1",
                    Name = "Name 1",
                    PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                    {
                        Description = "Description",
                        Name = "Name",
                    },
                });
            var destination = await secondaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_UpdateAsync_InputModel_Description_Empty()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = string.Empty,
                Name = "New Name",
            };

            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.SecondaryObject(id)
                {
                    Description = "Description 1",
                    Name = "Name 1",
                    PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                    {
                        Description = "Description",
                        Name = "Name",
                    },
                });
            var destination = await secondaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_UpdateAsync_InputModel_Description_WhiteSpace()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "     ",
                Name = "New Name",
            };

            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.SecondaryObject(id)
                {
                    Description = "Description 1",
                    Name = "Name 1",
                    PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                    {
                        Description = "Description",
                        Name = "Name",
                    },
                });
            var destination = await secondaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_UpdateAsync_InputModel_Name_Null()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = null,
            };

            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.SecondaryObject(id)
                {
                    Description = "Description 1",
                    Name = "Name 1",
                    PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                    {
                        Description = "Description",
                        Name = "Name",
                    },
                });
            var destination = await secondaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_UpdateAsync_InputModel_Name_Empty()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = string.Empty,
            };

            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.SecondaryObject(id)
                {
                    Description = "Description 1",
                    Name = "Name 1",
                    PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                    {
                        Description = "Description",
                        Name = "Name",
                    },
                });
            var destination = await secondaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task SecondaryObjectService_UpdateAsync_InputModel_Name_WhiteSpace()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = "     ",
            };

            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.SecondaryObject(id)
                {
                    Description = "Description 1",
                    Name = "Name 1",
                    PrimaryObject = new DomainModels.PrimaryObject(Guid.NewGuid())
                    {
                        Description = "Description",
                        Name = "Name",
                    },
                });
            var destination = await secondaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoEntityNotFoundException))]
        public async Task SecondaryObjectService_UpdateAsync_NotFound()
        {
            var secondaryObjectService = new SecondaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.SecondaryObject()
            {
                Description = "New Description",
                Name = "New Name",
            };

            _secondaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as DomainModels.SecondaryObject);
            await secondaryObjectService.UpdateAsync(Guid.NewGuid(), source);
            Assert.Fail("Expected exception was not thrown");
        }
    }
}
