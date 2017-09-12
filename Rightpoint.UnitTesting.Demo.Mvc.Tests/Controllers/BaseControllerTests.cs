using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Mvc.Controllers;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.Controllers
{
    public abstract class BaseControllerTests<TController>
        where TController : BaseController
    {
        [TestMethod]
        public void HomeController_GET_Error()
        {
            var controller = GetControllerInstance();
            var result = controller.Error();
            Assert.IsNotNull(result);
        }

        protected abstract TController GetControllerInstance();
    }
}
