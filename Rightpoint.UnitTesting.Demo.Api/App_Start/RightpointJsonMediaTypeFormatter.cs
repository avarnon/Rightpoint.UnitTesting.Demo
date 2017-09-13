using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Rightpoint.UnitTesting.Demo.Api
{
    /// <summary>
    /// Web API Configuration.
    /// </summary>
    /// <remarks>
    /// We're using <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> on the class because we can't really unit test it.
    /// This is just setting the Serializer settings on construction so there's no benefit in testing it.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class RightpointJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public RightpointJsonMediaTypeFormatter()
        {
            this.SerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new List<JsonConverter>()
                {
                    new StringEnumConverter(),
                }
            };
        }
    }
}
