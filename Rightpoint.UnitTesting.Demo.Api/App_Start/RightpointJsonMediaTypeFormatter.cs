using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Rightpoint.UnitTesting.Demo.Api
{
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
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                }
            };
        }
    }
}
