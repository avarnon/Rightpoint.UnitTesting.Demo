using System;
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
            var service = new PrimaryObjectService(null);
        }

        [TestMethod]
        public void PrimaryObjectService_Constructor_Valid()
        {
            var service = new PrimaryObjectService(_apiClient.Object);
        }
    }
}
