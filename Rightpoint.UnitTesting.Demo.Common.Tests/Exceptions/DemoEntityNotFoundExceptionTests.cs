using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Common.Exceptions;

namespace Rightpoint.UnitTesting.Demo.Common.Tests.Exceptions
{
    [TestClass]
    public class DemoEntityNotFoundExceptionTests
    {
        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_NoArguments()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new DemoEntityNotFoundException();

            Assert.AreEqual("Error in the application.", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageArgument_Null()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new DemoEntityNotFoundException(null);

            Assert.AreEqual("Exception of type 'Rightpoint.UnitTesting.Demo.Common.Exceptions.DemoEntityNotFoundException' was thrown.", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageArgument_Empty()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new DemoEntityNotFoundException(string.Empty);

            Assert.AreEqual(string.Empty, ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageArgument_WhiteSpace()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new DemoEntityNotFoundException("     ");

            Assert.AreEqual("     ", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageArgument_Valid()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new DemoEntityNotFoundException("test");

            Assert.AreEqual("test", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageAndInnerExArgument_NullMessage()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new DemoEntityNotFoundException(null, new Exception("Inner"));

            Assert.AreEqual("Exception of type 'Rightpoint.UnitTesting.Demo.Common.Exceptions.DemoEntityNotFoundException' was thrown.", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageAndInnerExArgument_EmptyMessage()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new DemoEntityNotFoundException(string.Empty, new Exception("Inner"));

            Assert.AreEqual(string.Empty, ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageAndInnerExArgument_WhiteSpaceMessage()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new DemoEntityNotFoundException("     ", new Exception("Inner"));

            Assert.AreEqual("     ", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageAndInnerExArgument_NullInnerEx()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new DemoEntityNotFoundException("test", null);

            Assert.AreEqual("test", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_MessageAndInnerExArgument_Valid()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new DemoEntityNotFoundException("test", new Exception("Inner"));

            Assert.AreEqual("test", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void DemoEntityNotFoundException_Constructor_Serialization()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing default serialization.
            DemoException inputException = new DemoEntityNotFoundException("test", new Exception("Inner"));

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
