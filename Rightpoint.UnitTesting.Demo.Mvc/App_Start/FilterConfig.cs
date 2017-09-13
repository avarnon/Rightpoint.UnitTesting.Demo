using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using Rightpoint.UnitTesting.Demo.Mvc.Attributes;

namespace Rightpoint.UnitTesting.Demo.Mvc
{
    /// <summary>
    /// Filter Configuration.
    /// </summary>
    /// <remarks>
    /// We're using <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> on the class because we can't really unit test it.
    /// This is canned code that comes with an MVC project anyway.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new DemoHandleErrorAttribute());
        }
    }
}
