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
            using (var container = UnityConfig.GetConfiguredContainer())
            {
            }
        }

        [TestMethod]
        public void UnityConfig_ResolveControllers()
        {
            using (var container = UnityConfig.GetConfiguredContainer())
            {
                var controllerTypes = typeof(UnityConfig).Assembly.GetTypes()
                    .Where(_ => _.Namespace == "Rightpoint.UnitTesting.Demo.Api.Controllers" &&
                                _.Name.EndsWith("Controller"))
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
    }
}
