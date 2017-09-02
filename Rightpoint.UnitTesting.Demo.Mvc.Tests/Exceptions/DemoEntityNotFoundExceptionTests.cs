using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Mvc.Exceptions;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.Exceptions
{
    [TestClass]
    public class DemoEntityNotFoundExceptionTests
    {
        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_NoArguments()
        {
            var ex = new DemoEntityNotFoundException();

            Assert.AreEqual("Error in the application.", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageArgument_Null()
        {
            var ex = new DemoEntityNotFoundException(null);

            Assert.AreEqual("Exception of type 'Rightpoint.UnitTesting.Demo.Mvc.Exceptions.DemoEntityNotFoundException' was thrown.", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageArgument_Empty()
        {
            var ex = new DemoEntityNotFoundException(string.Empty);

            Assert.AreEqual(string.Empty, ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageArgument_WhiteSpace()
        {
            var ex = new DemoEntityNotFoundException("     ");

            Assert.AreEqual("     ", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageArgument_Valid()
        {
            var ex = new DemoEntityNotFoundException("test");

            Assert.AreEqual("test", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageAndInnerExArgument_NullMessage()
        {
            var ex = new DemoEntityNotFoundException(null, new Exception("Inner"));

            Assert.AreEqual("Exception of type 'Rightpoint.UnitTesting.Demo.Mvc.Exceptions.DemoEntityNotFoundException' was thrown.", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageAndInnerExArgument_EmptyMessage()
        {
            var ex = new DemoEntityNotFoundException(string.Empty, new Exception("Inner"));

            Assert.AreEqual(string.Empty, ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageAndInnerExArgument_WhiteSpaceMessage()
        {
            var ex = new DemoEntityNotFoundException("     ", new Exception("Inner"));

            Assert.AreEqual("     ", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageAndInnerExArgument_NullInnerEx()
        {
            var ex = new DemoEntityNotFoundException("test", null);

            Assert.AreEqual("test", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageAndInnerExArgument_Valid()
        {
            var ex = new DemoEntityNotFoundException("test", new Exception("Inner"));

            Assert.AreEqual("test", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_Serialization()
        {
            DemoEntityNotFoundException inputException = new DemoEntityNotFoundException("test", new Exception("Inner"));

            byte[] bytes = BinarySerializer.Serialize(inputException);
            Assert.IsNotNull(bytes);

            DemoEntityNotFoundException deserializedException = BinarySerializer.Deserialize<DemoEntityNotFoundException>(bytes);

            Assert.IsNotNull(deserializedException);
            Assert.AreEqual(inputException.Message, deserializedException.Message);
            Assert.IsNotNull(deserializedException.InnerException);
            Assert.AreEqual(typeof(Exception), deserializedException.InnerException.GetType());
            Assert.AreEqual(inputException.InnerException.Message, deserializedException.InnerException.Message);
            Assert.IsNull(deserializedException.InnerException.InnerException);
        }
    }
}
