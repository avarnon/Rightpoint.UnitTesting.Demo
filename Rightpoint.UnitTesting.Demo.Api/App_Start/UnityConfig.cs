using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Rightpoint.UnitTesting.Demo.Api.Contracts;
using Rightpoint.UnitTesting.Demo.Api.Services;
using Rightpoint.UnitTesting.Demo.Domain.Repositories;
using Rightpoint.UnitTesting.Demo.Infrastructure.Data;
using Rightpoint.UnitTesting.Demo.Infrastructure.Repositories;
using Rightpoint.UnitTesting.Demo.Infrastructure.Services;

namespace Rightpoint.UnitTesting.Demo.Api
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
            container.RegisterType<DemoContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IPrimaryObjectRepository, PrimaryObjectRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISecondaryObjectRepository, SecondaryObjectRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPrimaryObjectService, PrimaryObjectService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISecondaryObjectService, SecondaryObjectService>(new HierarchicalLifetimeManager());
        }
    }
}
