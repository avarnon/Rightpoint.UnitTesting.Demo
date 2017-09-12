using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Common.Tests;
using Rightpoint.UnitTesting.Demo.Mvc.Exceptions;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.Exceptions
{
    [TestClass]
    public class RemoteExceptionTests
    {
        [TestMethod]
        public void RemoteException_Constructor_NoArguments()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new RemoteException();

            Assert.AreEqual("Error in the application.", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void DemoException_Constructor_MessageArgument_Null()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new RemoteException(null);

            Assert.AreEqual("Exception of type 'Rightpoint.UnitTesting.Demo.Mvc.Exceptions.RemoteException' was thrown.", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void RemoteException_Constructor_MessageArgument_Empty()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new RemoteException(string.Empty);

            Assert.AreEqual(string.Empty, ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void RemoteException_Constructor_MessageArgument_WhiteSpace()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new RemoteException("     ");

            Assert.AreEqual("     ", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void RemoteException_Constructor_MessageArgument_Valid()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new RemoteException("test");

            Assert.AreEqual("test", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void RemoteException_Constructor_MessageAndInnerExArgument_NullMessage()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new RemoteException(null, new Exception("Inner"));

            Assert.AreEqual("Exception of type 'Rightpoint.UnitTesting.Demo.Mvc.Exceptions.RemoteException' was thrown.", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void RemoteException_Constructor_MessageAndInnerExArgument_EmptyMessage()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new RemoteException(string.Empty, new Exception("Inner"));

            Assert.AreEqual(string.Empty, ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void RemoteException_Constructor_MessageAndInnerExArgument_WhiteSpaceMessage()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new RemoteException("     ", new Exception("Inner"));

            Assert.AreEqual("     ", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void RemoteException_Constructor_MessageAndInnerExArgument_NullInnerEx()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new RemoteException("test", null);

            Assert.AreEqual("test", ex.Message);
            Assert.IsNull(ex.InnerException);
        }

        [TestMethod]
        public void RemoteException_Constructor_MessageAndInnerExArgument_Valid()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the constructor with no custom logic.
            var ex = new RemoteException("test", new Exception("Inner"));

            Assert.AreEqual("test", ex.Message);
            Assert.IsNotNull(ex.InnerException);
            Assert.AreEqual("Inner", ex.InnerException.Message);
            Assert.IsNull(ex.InnerException.InnerException);
        }

        [TestMethod]
        public void RemoteException_Constructor_Serialization()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing default serialization.
            RemoteException inputException = new RemoteException("test", new Exception("Inner"));

            byte[] bytes = BinarySerializer.Serialize(inputException);
            Assert.IsNotNull(bytes);

            RemoteException deserializedException = BinarySerializer.Deserialize<RemoteException>(bytes);

            Assert.IsNotNull(deserializedException);
            Assert.AreEqual(inputException.Message, deserializedException.Message);
            Assert.IsNotNull(deserializedException.InnerException);
            Assert.AreEqual(typeof(Exception), deserializedException.InnerException.GetType());
            Assert.AreEqual(inputException.InnerException.Message, deserializedException.InnerException.Message);
            Assert.IsNull(deserializedException.InnerException.InnerException);
        }
    }
}
