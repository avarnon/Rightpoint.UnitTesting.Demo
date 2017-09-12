using System;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Api.Controllers;

namespace Rightpoint.UnitTesting.Demo.Api.Tests.App_Start
{
    [TestClass]
    public class UnityConfigTests
    {
        [TestMethod]
        public void UnityConfig_GetConfiguredContainer()
        {
            // This test verifies that your Unity registration logic does not error out.
            var container = UnityConfig.GetConfiguredContainer();
            Assert.IsNotNull(container);
        }

        [TestMethod]
        public void UnityConfig_ResolveControllers()
        {
            // This test verifies that all of your controllers can be resolved successfully.
            using (var container = UnityConfig.GetConfiguredContainer())
            {
                var controllerTypes = typeof(UnityConfig).Assembly.GetTypes()
                    .Where(_ => IsApiControllerType(_) &&
                                _.IsAbstract == false)
                    .ToArray();
                foreach (var type in controllerTypes)
                {
                    var resolvedObject = container.Resolve(type);
                    Assert.IsNotNull(resolvedObject);
                    var baseController = resolvedObject as BaseController;
                    Assert.IsNotNull(baseController);
                }
            }
        }

        private static bool IsApiControllerType(Type type)
        {
            if (type == null || type == typeof(object))
            {
                return false;
            }
            else if (type == typeof(BaseController))
            {
                return true;
            }
            else if (type.BaseType == null || type.BaseType == typeof(object))
            {
                return false;
            }
            else if (type.BaseType == typeof(BaseController))
            {
                return true;
            }
            else
            {
                return IsApiControllerType(type.BaseType);
            }
        }
    }
}
