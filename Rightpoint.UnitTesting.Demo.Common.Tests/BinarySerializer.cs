using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Rightpoint.UnitTesting.Demo.Common.Tests
{
    /// <summary>
    /// Helper class for testing binary serialization
    /// </summary>
    /// <remarks>
    /// This class is added as a link to projects outside of Rightpoint.UnitTesting.Demo.Common
    /// because adding a reference to Rightpoint.UnitTesting.Demo.Common will copy it to the bin folder of the second project.
    /// Have a test DLL copied to another BIN folder can cause issues on the build server when running unit tests.
    /// </remarks>
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
