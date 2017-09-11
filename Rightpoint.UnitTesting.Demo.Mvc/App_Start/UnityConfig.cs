using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Rightpoint.UnitTesting.Demo.Mvc.Contracts;
using Rightpoint.UnitTesting.Demo.Mvc.Services;

namespace Rightpoint.UnitTesting.Demo.Mvc.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        public static void RegisterTypes(IUnityContainer container)
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                container.RegisterInstance<string>($"AppSettings:{key}", ConfigurationManager.AppSettings[key]);
            }

            foreach (ConnectionStringSettings connectionStringSettings in ConfigurationManager.ConnectionStrings)
            {
                container.RegisterInstance<string>($"ConnectionStrings:{connectionStringSettings.Name}", connectionStringSettings.ConnectionString);
            }

            container.RegisterType<IApiClient, ApiClient>(
                new HierarchicalLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<string>("AppSettings:ApiUrl")));
            container.RegisterType<IMvcExceptionMapper, MvcExceptionMapper>(new HierarchicalLifetimeManager());
            container.RegisterType<IPrimaryObjectService, PrimaryObjectService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISecondaryObjectService, SecondaryObjectService>(new HierarchicalLifetimeManager());
        }
    }
}
