using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Rightpoint.UnitTesting.Demo.Api
{
    [ExcludeFromCodeCoverage]
    public static class FilterAttributeExtensions
    {
        /// <summary>
        /// Resolve an instance of the default requested type.
        /// </summary>
        /// <typeparam name="T"><see cref="T:System.Type" /> of object to get.</typeparam>
        /// <param name="attribute">The current <see cref="FilterAttribute" /></param>
        /// <param name="request">The current <see cref="HttpRequestMessage" /></param>
        /// <returns>Returns the resolved object.</returns>
        public static T Resolve<T>(this FilterAttribute attribute, HttpRequestMessage request)
        {
            return (T)request.GetDependencyScope().GetService(typeof(T));
        }
    }
}