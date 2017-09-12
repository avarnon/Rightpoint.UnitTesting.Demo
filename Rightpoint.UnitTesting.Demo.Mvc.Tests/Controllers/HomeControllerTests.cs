using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Mvc.Controllers;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests : BaseControllerTests<HomeController>
    {
        [TestMethod]
        public void HomeController_Constructor()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the default constructor with no custom logic.
            var controller = new HomeController();
        }

        [TestMethod]
        public void HomeController_GET_Index()
        {
            // This test verifies that Index successfully returns a result
            var controller = new HomeController();
            var result = controller.Index();
            Assert.IsNotNull(result);
        }

        protected override HomeController GetControllerInstance()
        {
            return new HomeController();
        }
    }
}
