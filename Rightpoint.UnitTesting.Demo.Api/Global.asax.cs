using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Rightpoint.UnitTesting.Demo.Api
{
    /// <summary>
    /// Defines the methods, properties, and events that are common to all application objects in this ASP.NET application.
    /// This class is the base class for the application defined by the user in the Global.asax file.
    /// </summary>
    /// <remarks>
    /// We're using <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> on the class because we can't really unit test it.
    /// This is canned code that comes with a Web API project anyway.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
