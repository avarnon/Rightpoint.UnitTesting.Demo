using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Mvc.Exceptions;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.Exceptions
{
    [TestClass]
    public class DemoInvalidOperationExceptionTests
    {
        [TestMethod]
        public void DemoInvalidOperationException_Constructor_NoArguments()
        {
            var ex = new DemoInvalidOperationException();

            Assert.AreEqual("Error in the application.", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInvalidOperationException_Constructor_MessageArgument_Null()
        {
            var ex = new DemoInvalidOperationException(null);

            Assert.AreEqual("Exception of type 'Rightpoint.UnitTesting.Demo.Mvc.Exceptions.DemoInvalidOperationException' was thrown.", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInvalidOperationException_Constructor_MessageArgument_Empty()
        {
            var ex = new DemoInvalidOperationException(string.Empty);

            Assert.AreEqual(string.Empty, ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInvalidOperationException_Constructor_MessageArgument_WhiteSpace()
        {
            var ex = new DemoInvalidOperationException("     ");

            Assert.AreEqual("     ", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInvalidOperationException_Constructor_MessageArgument_Valid()
        {
            var ex = new DemoInvalidOperationException("test");

            Assert.AreEqual("test", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInvalidOperationException_Constructor_MessageAndInnerExArgument_NullMessage()
        {
            var ex = new DemoInvalidOperationException(null, new Exception("Inner"));

            Assert.AreEqual("Exception of type 'Rightpoint.UnitTesting.Demo.Mvc.Exceptions.DemoInvalidOperationException' was thrown.", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoInvalidOperationException_Constructor_MessageAndInnerExArgument_EmptyMessage()
        {
            var ex = new DemoInvalidOperationException(string.Empty, new Exception("Inner"));

            Assert.AreEqual(string.Empty, ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoInvalidOperationException_Constructor_MessageAndInnerExArgument_WhiteSpaceMessage()
        {
            var ex = new DemoInvalidOperationException("     ", new Exception("Inner"));

            Assert.AreEqual("     ", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoInvalidOperationException_Constructor_MessageAndInnerExArgument_NullInnerEx()
        {
            var ex = new DemoInvalidOperationException("test", null);

            Assert.AreEqual("test", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInvalidOperationException_Constructor_MessageAndInnerExArgument_Valid()
        {
            var ex = new DemoInvalidOperationException("test", new Exception("Inner"));

            Assert.AreEqual("test", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoInvalidOperationException_Constructor_Serialization()
        {
            DemoInvalidOperationException inputException = new DemoInvalidOperationException("test", new Exception("Inner"));

            byte[] bytes = BinarySerializer.Serialize(inputException);
            Assert.IsNotNull(bytes);

            DemoInvalidOperationException deserializedException = BinarySerializer.Deserialize<DemoInvalidOperationException>(bytes);

            Assert.IsNotNull(deserializedException);
            Assert.AreEqual(inputException.Message, deserializedException.Message);
            Assert.IsNotNull(deserializedException.InnerException);
            Assert.AreEqual(typeof(Exception), deserializedException.InnerException.GetType());
            Assert.AreEqual(inputException.InnerException.Message, deserializedException.InnerException.Message);
            Assert.IsNull(deserializedException.InnerException.InnerException);
        }
    }
}
