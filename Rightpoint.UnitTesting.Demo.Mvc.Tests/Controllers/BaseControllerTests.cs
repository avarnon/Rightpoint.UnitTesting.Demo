using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Mvc.Controllers;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.Controllers
{
    /// <summary>
    /// Base Controller Tests
    /// </summary>
    /// <typeparam name="TController">The type of the controller being tested</typeparam>
    /// <remarks>
    /// This class contains tests that are common to all controllers that inherit from BaseController.
    /// </remarks>
    public abstract class BaseControllerTests<TController>
        where TController : BaseController
    {
        [TestMethod]
        public void HomeController_GET_Error()
        {
            // This test verifies that Error successfully returns a result
            var controller = GetControllerInstance();
            var result = controller.Error();
            Assert.IsNotNull(result);
        }

        protected abstract TController GetControllerInstance();
    }
}
