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
    public class PrimaryObjectServiceTests
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
        public void PrimaryObjectService_Constructor_Valid()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrimaryObjectService_Constructor_PrimaryObjectRepository_Null()
        {
            var primaryObjectService = new PrimaryObjectService(null, _secondaryObjectRepository.Object, _unitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrimaryObjectService_Constructor_SecondaryObjectRepository_Null()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, null, _unitOfWork.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrimaryObjectService_Constructor_UnitOfWork_Null()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, null);
        }

        [TestMethod]
        public async Task PrimaryObjectService_CreateAsync_Valid()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "New Description",
                Name = "New Name",
            };
            var destination = await primaryObjectService.CreateAsync(source);
            Assert.IsNotNull(destination);
            Assert.AreEqual(source.Description, destination.Description);
            Assert.AreNotEqual(Guid.Empty, destination.Id);
            Assert.AreEqual(source.Name, destination.Name);
            Assert.IsNotNull(destination.SecondaryObjects);
            Assert.AreEqual(0, destination.SecondaryObjects.Count);
            _unitOfWork.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task PrimaryObjectService_CreateAsync_InputModel_Null()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            ApiModels.PrimaryObject source = null;
            var destination = await primaryObjectService.CreateAsync(source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_CreateAsync_InputModel_Description_Null()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = null,
                Name = "New Name",
            };
            var destination = await primaryObjectService.CreateAsync(source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_CreateAsync_InputModel_Description_Empty()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = string.Empty,
                Name = "New Name",
            };
            var destination = await primaryObjectService.CreateAsync(source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_CreateAsync_InputModel_Description_WhiteSpace()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "     ",
                Name = "New Name",
            };
            var destination = await primaryObjectService.CreateAsync(source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_CreateAsync_InputModel_Name_Null()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "New Description",
                Name = null,
            };
            var destination = await primaryObjectService.CreateAsync(source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_CreateAsync_InputModel_Name_Empty()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "New Description",
                Name = string.Empty,
            };
            var destination = await primaryObjectService.CreateAsync(source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_CreateAsync_InputModel_Name_WhiteSpace()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "New Description",
                Name = "     ",
            };
            var destination = await primaryObjectService.CreateAsync(source);
        }

        [TestMethod]
        public async Task PrimaryObjectService_DeleteAsync_Valid()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new DomainModels.PrimaryObject(Guid.NewGuid())
            {
                Description = "Description",
                Name = "Name",
                SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1",
                            Name = "Name 1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 2",
                            Name = "Name 2",
                        },
                    },
            };
            foreach (var sourceSecondaryObject in source.SecondaryObjects)
            {
                sourceSecondaryObject.PrimaryObject = source;
                sourceSecondaryObject.PrimaryObject_Id = source.Id;
            }

            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(source.Id)).ReturnsAsync(source);
            await primaryObjectService.DeleteAsync(source.Id);
            _unitOfWork.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task PrimaryObjectService_DeleteAsync_Id_Empty()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);

            await primaryObjectService.DeleteAsync(Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoEntityNotFoundException))]
        public async Task PrimaryObjectService_DeleteAsync_NotFound()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = Guid.NewGuid();
            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as DomainModels.PrimaryObject);
            await primaryObjectService.DeleteAsync(source);
        }

        [TestMethod]
        public async Task PrimaryObjectService_GetAllAsync_Valid()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            _primaryObjectRepository.Setup(_ => _.GetAllAsync()).ReturnsAsync(new List<DomainModels.PrimaryObject>());
            var destination = await primaryObjectService.GetAllAsync();
            Assert.IsNotNull(destination);
            _unitOfWork.Verify(_ => _.SaveChangesAsync(), Times.Never);
        }

        [TestMethod]
        public async Task PrimaryObjectService_GetByIdAsync_Valid()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new DomainModels.PrimaryObject(Guid.NewGuid())
            {
                Description = "Description",
                Name = "Name",
                SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1",
                            Name = "Name 1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 2",
                            Name = "Name 2",
                        },
                    },
            };
            foreach (var sourceSecondaryObject in source.SecondaryObjects)
            {
                sourceSecondaryObject.PrimaryObject = source;
                sourceSecondaryObject.PrimaryObject_Id = source.Id;
            }

            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(source.Id)).ReturnsAsync(source);
            var destination = await primaryObjectService.GetAsync(source.Id);
            Assert.AreEqual(source, destination);

            _unitOfWork.Verify(_ => _.SaveChangesAsync(), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task PrimaryObjectService_GetByIdAsync_Id_Empty()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);

            var destination = await primaryObjectService.GetAsync(Guid.Empty);
        }

        [TestMethod]
        public async Task PrimaryObjectService_GetByIdAsync_NotFound()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = Guid.NewGuid();
            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as DomainModels.PrimaryObject);
            var destination = await primaryObjectService.GetAsync(source);
            Assert.IsNull(destination);
        }

        [TestMethod]
        public async Task PrimaryObjectService_UpdateAsync_Valid()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "New Description",
                Name = "New Name",
            };

            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id)
                {
                    Description = "Description",
                    Name = "Name",
                    SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1",
                            Name = "Name 1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 2",
                            Name = "Name 2",
                        },
                    },
                });
            var destination = await primaryObjectService.UpdateAsync(Guid.NewGuid(), source);
            Assert.IsNotNull(destination);
            Assert.AreEqual(source.Description, destination.Description);
            Assert.AreNotEqual(Guid.Empty, destination.Id);
            Assert.AreEqual(source.Name, destination.Name);
            Assert.IsNotNull(destination.SecondaryObjects);
            _unitOfWork.Verify(_ => _.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task PrimaryObjectService_UpdateAsync_Id_Empty()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "New Description",
                Name = "New Name",
            };

            await primaryObjectService.UpdateAsync(Guid.Empty, source);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task PrimaryObjectService_UpdateAsync_InputModel_Null()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            ApiModels.PrimaryObject source = null;

            await primaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_UpdateAsync_InputModel_Description_Null()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = null,
                Name = "New Name",
            };

            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id)
                {
                    Description = "Description",
                    Name = "Name",
                    SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1",
                            Name = "Name 1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 2",
                            Name = "Name 2",
                        },
                    },
                });

            await primaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_UpdateAsync_InputModel_Description_Empty()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = string.Empty,
                Name = "New Name",
            };

            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id)
                {
                    Description = "Description",
                    Name = "Name",
                    SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1",
                            Name = "Name 1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 2",
                            Name = "Name 2",
                        },
                    },
                });

            await primaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_UpdateAsync_InputModel_Description_WhiteSpace()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "     ",
                Name = "New Name",
            };

            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id)
                {
                    Description = "Description",
                    Name = "Name",
                    SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1",
                            Name = "Name 1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 2",
                            Name = "Name 2",
                        },
                    },
                });

            await primaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_UpdateAsync_InputModel_Name_Null()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "New Description",
                Name = null,
            };

            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id)
                {
                    Description = "Description",
                    Name = "Name",
                    SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1",
                            Name = "Name 1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 2",
                            Name = "Name 2",
                        },
                    },
                });

            await primaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_UpdateAsync_InputModel_Name_Empty()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "New Description",
                Name = string.Empty,
            };

            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id)
                {
                    Description = "Description",
                    Name = "Name",
                    SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1",
                            Name = "Name 1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 2",
                            Name = "Name 2",
                        },
                    },
                });

            await primaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoInputValidationException))]
        public async Task PrimaryObjectService_UpdateAsync_InputModel_Name_WhiteSpace()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "New Description",
                Name = "     ",
            };

            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new DomainModels.PrimaryObject(id)
                {
                    Description = "Description",
                    Name = "Name",
                    SecondaryObjects = new List<DomainModels.SecondaryObject>()
                    {
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 1",
                            Name = "Name 1",
                        },
                        new DomainModels.SecondaryObject(Guid.NewGuid())
                        {
                            Description = "Description 2",
                            Name = "Name 2",
                        },
                    },
                });

            await primaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }

        [TestMethod]
        [ExpectedException(typeof(DemoEntityNotFoundException))]
        public async Task PrimaryObjectService_UpdateAsync_NotFound()
        {
            var primaryObjectService = new PrimaryObjectService(_primaryObjectRepository.Object, _secondaryObjectRepository.Object, _unitOfWork.Object);
            var source = new ApiModels.PrimaryObject()
            {
                Description = "New Description",
                Name = "New Name",
            };

            _primaryObjectRepository.Setup(_ => _.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as DomainModels.PrimaryObject);
            await primaryObjectService.UpdateAsync(Guid.NewGuid(), source);
        }
    }
}
