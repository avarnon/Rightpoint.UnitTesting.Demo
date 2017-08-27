using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;

namespace Rightpoint.UnitTesting.Demo.Api
{
    public class FilterConfig
    {
        [ExcludeFromCodeCoverage]
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
