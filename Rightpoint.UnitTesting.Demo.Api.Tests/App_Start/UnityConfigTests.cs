using System;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rightpoint.UnitTesting.Demo.Api.Tests.App_Start
{
    [TestClass]
    public class UnityConfigTests
    {
        [TestMethod]
        public void UnityConfig_GetConfiguredContainer()
        {
            var container = UnityConfig.GetConfiguredContainer();
            Assert.IsNotNull(container);
        }

        [TestMethod]
        public void UnityConfig_ResolveControllers()
        {
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
                    var apiController = resolvedObject as ApiController;
                    Assert.IsNotNull(apiController);
                }
            }
        }

        private static bool IsApiControllerType(Type type)
        {
            if (type == null || type == typeof(object))
            {
                return false;
            }
            else if (type == typeof(ApiController))
            {
                return true;
            }
            else if (type.BaseType == null || type.BaseType == typeof(object))
            {
                return false;
            }
            else if (type.BaseType == typeof(ApiController))
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
