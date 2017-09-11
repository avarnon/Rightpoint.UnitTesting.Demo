using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Common.Exceptions;

namespace Rightpoint.UnitTesting.Demo.Common.Tests.Exceptions
{
    [TestClass]
    public class DemoInputValidationExceptionTests
    {
        [TestMethod]
        public void DemoInputValidationException_Constructor_NoArguments()
        {
            var ex = new DemoInputValidationException();

            Assert.AreEqual("Error in the application.", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInputValidationException_Constructor_MessageArgument_Null()
        {
            var ex = new DemoInputValidationException(null);

            Assert.AreEqual("Exception of type 'Rightpoint.UnitTesting.Demo.Common.Exceptions.DemoInputValidationException' was thrown.", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInputValidationException_Constructor_MessageArgument_Empty()
        {
            var ex = new DemoInputValidationException(string.Empty);

            Assert.AreEqual(string.Empty, ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInputValidationException_Constructor_MessageArgument_WhiteSpace()
        {
            var ex = new DemoInputValidationException("     ");

            Assert.AreEqual("     ", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInputValidationException_Constructor_MessageArgument_Valid()
        {
            var ex = new DemoInputValidationException("test");

            Assert.AreEqual("test", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInputValidationException_Constructor_MessageAndInnerExArgument_NullMessage()
        {
            var ex = new DemoInputValidationException(null, new Exception("Inner"));

            Assert.AreEqual("Exception of type 'Rightpoint.UnitTesting.Demo.Common.Exceptions.DemoInputValidationException' was thrown.", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoInputValidationException_Constructor_MessageAndInnerExArgument_EmptyMessage()
        {
            var ex = new DemoInputValidationException(string.Empty, new Exception("Inner"));

            Assert.AreEqual(string.Empty, ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoInputValidationException_Constructor_MessageAndInnerExArgument_WhiteSpaceMessage()
        {
            var ex = new DemoInputValidationException("     ", new Exception("Inner"));

            Assert.AreEqual("     ", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoInputValidationException_Constructor_MessageAndInnerExArgument_NullInnerEx()
        {
            var ex = new DemoInputValidationException("test", null);

            Assert.AreEqual("test", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoInputValidationException_Constructor_MessageAndInnerExArgument_Valid()
        {
            var ex = new DemoInputValidationException("test", new Exception("Inner"));

            Assert.AreEqual("test", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoInputValidationException_Constructor_Serialization()
        {
            DemoException inputException = new DemoInputValidationException("test", new Exception("Inner"));

            byte[] bytes = BinarySerializer.Serialize(inputException);
            Assert.IsNotNull(bytes);

            DemoInputValidationException deserializedException = BinarySerializer.Deserialize<DemoInputValidationException>(bytes);

            Assert.IsNotNull(deserializedException);
            Assert.AreEqual(inputException.Message, deserializedException.Message);
            Assert.IsNotNull(deserializedException.InnerException);
            Assert.AreEqual(typeof(Exception), deserializedException.InnerException.GetType());
            Assert.AreEqual(inputException.InnerException.Message, deserializedException.InnerException.Message);
            Assert.IsNull(deserializedException.InnerException.InnerException);
        }
    }
}
