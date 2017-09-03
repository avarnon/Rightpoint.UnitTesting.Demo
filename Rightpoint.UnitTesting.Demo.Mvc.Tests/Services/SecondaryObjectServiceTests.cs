using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rightpoint.UnitTesting.Demo.Mvc.Contracts;
using Rightpoint.UnitTesting.Demo.Mvc.Services;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.Services
{
    [TestClass]
    public class SecondaryObjectServiceTests
    {
        private Mock<IApiClient> _apiClient;

        [TestInitialize]
        public void TestInitialize()
        {
            _apiClient = new Mock<IApiClient>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SecondaryObjectService_Constructor_ApiClient_Null()
        {
            var service = new SecondaryObjectService(null);
        }

        [TestMethod]
        public void SecondaryObjectService_Constructor_Valid()
        {
            var service = new SecondaryObjectService(_apiClient.Object);
        }

        [TestMethod]
        public async Task SecondaryObjectService_CreateAsync()
        {
            var service = new SecondaryObjectService(_apiClient.Object);
            _apiClient.Setup(_ => _.CreateAsync(It.IsAny<string>(), It.IsAny<Mvc.Contracts.Models.SecondaryObject>()))
                .ReturnsAsync(new Mvc.Contracts.Models.SecondaryObject());
            var result = await service.CreateAsync(Guid.NewGuid(), new Mvc.Models.SecondaryObject());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SecondaryObjectService_DeleteAsync()
        {
            var service = new SecondaryObjectService(_apiClient.Object);
            await service.DeleteAsync(Guid.NewGuid());
        }

        [TestMethod]
        public async Task SecondaryObjectService_GetAllAsync()
        {
            var service = new SecondaryObjectService(_apiClient.Object);
            _apiClient.Setup(_ => _.GetAsync<IEnumerable<Mvc.Contracts.Models.SecondaryObject>>(It.IsAny<string>()))
                .ReturnsAsync(new Mvc.Contracts.Models.SecondaryObject[0]);
            var result = await service.GetAllAsync();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SecondaryObjectService_GetAsync()
        {
            var service = new SecondaryObjectService(_apiClient.Object);
            _apiClient.Setup(_ => _.GetAsync<Mvc.Contracts.Models.SecondaryObject>(It.IsAny<string>()))
                .ReturnsAsync(new Mvc.Contracts.Models.SecondaryObject());
            var result = await service.GetAsync(Guid.NewGuid());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SecondaryObjectService_UpdateAsync()
        {
            var service = new SecondaryObjectService(_apiClient.Object);
            _apiClient.Setup(_ => _.UpdateAsync(It.IsAny<string>(), It.IsAny<Mvc.Contracts.Models.SecondaryObject>()))
                .ReturnsAsync(new Mvc.Contracts.Models.SecondaryObject());
            var result = await service.UpdateAsync(Guid.NewGuid(), new Mvc.Models.SecondaryObject());
            Assert.IsNotNull(result);
        }
    }
}
