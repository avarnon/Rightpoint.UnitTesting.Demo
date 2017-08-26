using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Domain.Tests.Models
{
    [TestClass]
    public class PrimaryObjectTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PrimaryObject_Constructor_Id_Empty()
        {
            var primaryObject = new PrimaryObject(Guid.Empty);
            Assert.Fail("Expected Exception was not thrown");
        }

        [TestMethod]
        public void PrimaryObject_Constructor_Valid()
        {
            var primaryObject = new PrimaryObject(Guid.NewGuid());
        }

        [TestMethod]
        public void PrimaryObject_Description()
        {
            string value = "test";
            var primaryObject = new PrimaryObject(Guid.NewGuid());
            primaryObject.Description = value;
            Assert.AreEqual(value, primaryObject.Description);
        }

        [TestMethod]
        public void PrimaryObject_Id()
        {
            Guid value = Guid.NewGuid();
            var primaryObject = new PrimaryObject(value);
            Assert.AreEqual(value, primaryObject.Id);
        }

        [TestMethod]
        public void PrimaryObject_Name()
        {
            string value = "test";
            var primaryObject = new PrimaryObject(Guid.NewGuid());
            primaryObject.Name = value;
            Assert.AreEqual(value, primaryObject.Name);
        }

        [TestMethod]
        public void PrimaryObject_SecondaryObjects()
        {
            ICollection<SecondaryObject> value = new Collection<SecondaryObject>();
            var primaryObject = new PrimaryObject(Guid.NewGuid());
            primaryObject.SecondaryObjects = value;
            Assert.AreEqual(value, primaryObject.SecondaryObjects);
        }
    }
}
