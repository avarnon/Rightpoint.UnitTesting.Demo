using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests
{
    [ExcludeFromCodeCoverage]
    public static class BinarySerializer
    {
        public static byte[] Serialize<T>(T exception)
        {
            byte[] bytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(ms, exception);
                bytes = ms.ToArray();
            }

            return bytes;
        }

        public static T Deserialize<T>(byte[] bytes)
            where T : class
        {
            T result = null;

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                var binaryFormatter = new BinaryFormatter();
                result = (T)binaryFormatter.Deserialize(ms);
            }

            return result;
        }
    }
}
