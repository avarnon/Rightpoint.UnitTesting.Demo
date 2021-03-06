﻿using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using Microsoft.Practices.Unity.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Rightpoint.UnitTesting.Demo.Api.UnityWebApiActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Rightpoint.UnitTesting.Demo.Api.UnityWebApiActivator), "Shutdown")]

namespace Rightpoint.UnitTesting.Demo.Api
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET
    /// </summary>
    /// <remarks>
    /// We're using <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> on the class because we can't really unit test it.
    /// This is canned code that comes with Unity anyway.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public static class UnityWebApiActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start()
        {
            // Use UnityHierarchicalDependencyResolver if you want to use a new child container for each IHttpController resolution.
            var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.GetConfiguredContainer());
            //// var resolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}
