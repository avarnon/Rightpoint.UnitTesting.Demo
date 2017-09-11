using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using Rightpoint.UnitTesting.Demo.Mvc.Attributes;

namespace Rightpoint.UnitTesting.Demo.Mvc
{
    [ExcludeFromCodeCoverage]
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new DemoHandleErrorAttribute());
        }
    }
}
