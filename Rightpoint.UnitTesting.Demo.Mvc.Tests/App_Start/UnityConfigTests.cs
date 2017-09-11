using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Mvc.App_Start;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.App_Start
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
                container.RegisterInstance<string>("AppSettings:ApiUrl", "https://www.fakeurl.com");
                var controllerTypes = typeof(UnityConfig).Assembly.GetTypes()
                    .Where(_ => IsMvcControllerType(_) &&
                                _.IsAbstract == false)
                    .ToArray();
                foreach (var type in controllerTypes)
                {
                    var resolvedObject = container.Resolve(type);
                    Assert.IsNotNull(resolvedObject);
                    var apiController = resolvedObject as Controller;
                    Assert.IsNotNull(apiController);
                }
            }
        }

        private static bool IsMvcControllerType(Type type)
        {
            if (type == null || type == typeof(object))
            {
                return false;
            }
            else if (type == typeof(Controller))
            {
                return true;
            }
            else if (type.BaseType == null || type.BaseType == typeof(object))
            {
                return false;
            }
            else if (type.BaseType == typeof(Controller))
            {
                return true;
            }
            else
            {
                return IsMvcControllerType(type.BaseType);
            }
        }
    }
}
