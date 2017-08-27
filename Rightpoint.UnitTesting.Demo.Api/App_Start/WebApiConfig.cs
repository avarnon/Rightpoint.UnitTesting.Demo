using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Rightpoint.UnitTesting.Demo.Api
{
    [ExcludeFromCodeCoverage]
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Use custom binding for JSON Formatter
            var defaultJsonformatter = config.Formatters.OfType<JsonMediaTypeFormatter>().SingleOrDefault();
            if (defaultJsonformatter != null)
            {
                config.Formatters.Remove(defaultJsonformatter);
            }

            config.Formatters.Insert(0, new RightpointJsonMediaTypeFormatter());
        }
    }
}
