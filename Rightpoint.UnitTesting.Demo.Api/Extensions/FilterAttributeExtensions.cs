using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Rightpoint.UnitTesting.Demo.Api
{
    /// <summary>
    /// Filter Attribute extensions.
    /// </summary>
    /// <remarks>
    /// We're using <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> on the class because we can't really unit test it.
    /// We can't easily test this because of it's dependency on the HTTP pipeline for dependency resolution. There wouldn't be much benefit in testing it anyway.
    /// </remarks>
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