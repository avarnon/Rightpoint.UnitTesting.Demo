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
    public class PrimaryObjectServiceTests
    {
        private Mock<IApiClient> _apiClient;

        [TestInitialize]
        public void TestInitialize()
        {
            _apiClient = new Mock<IApiClient>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrimaryObjectService_Constructor_ApiClient_Null()
        {
            // This test verifies that the service will not accept null dependencies
            var service = new PrimaryObjectService(null);
        }

        [TestMethod]
        public void PrimaryObjectService_Constructor_Valid()
        {
            // This test verifies that the service can be constructed successfully
            var service = new PrimaryObjectService(_apiClient.Object);
        }

        [TestMethod]
        public async Task PrimaryObjectService_CreateAsync()
        {
            // This test verifies that CreateAsync works with valid inputs
            var service = new PrimaryObjectService(_apiClient.Object);
            _apiClient.Setup(_ => _.CreateAsync(It.IsAny<string>(), It.IsAny<Mvc.Contracts.Models.PrimaryObject>()))
                .ReturnsAsync(new Mvc.Contracts.Models.PrimaryObject());
            var result = await service.CreateAsync(new Mvc.Models.PrimaryObject());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PrimaryObjectService_DeleteAsync()
        {
            // This test verifies that DeleteAsync works with valid inputs
            var service = new PrimaryObjectService(_apiClient.Object);
            await service.DeleteAsync(Guid.NewGuid());
        }

        [TestMethod]
        public async Task PrimaryObjectService_GetAllAsync()
        {
            // This test verifies that GetAllAsync works with valid inputs
            var service = new PrimaryObjectService(_apiClient.Object);
            _apiClient.Setup(_ => _.GetAsync<IEnumerable<Mvc.Contracts.Models.PrimaryObject>>(It.IsAny<string>()))
                .ReturnsAsync(new Mvc.Contracts.Models.PrimaryObject[0]);
            var result = await service.GetAllAsync();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PrimaryObjectService_GetAsync()
        {
            // This test verifies that GetAsync works with valid inputs
            var service = new PrimaryObjectService(_apiClient.Object);
            _apiClient.Setup(_ => _.GetAsync<Mvc.Contracts.Models.PrimaryObject>(It.IsAny<string>()))
                .ReturnsAsync(new Mvc.Contracts.Models.PrimaryObject());
            var result = await service.GetAsync(Guid.NewGuid());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PrimaryObjectService_UpdateAsync()
        {
            // This test verifies that UpdateAsync works with valid inputs
            var service = new PrimaryObjectService(_apiClient.Object);
            _apiClient.Setup(_ => _.UpdateAsync(It.IsAny<string>(), It.IsAny<Mvc.Contracts.Models.PrimaryObject>()))
                .ReturnsAsync(new Mvc.Contracts.Models.PrimaryObject());
            var result = await service.UpdateAsync(Guid.NewGuid(), new Mvc.Models.PrimaryObject());
            Assert.IsNotNull(result);
        }
    }
}
