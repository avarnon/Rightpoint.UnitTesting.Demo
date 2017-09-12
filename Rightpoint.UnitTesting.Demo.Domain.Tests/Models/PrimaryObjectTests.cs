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
            // This test verifies that the object's constructor will not accept null dependencies
            var primaryObject = new PrimaryObject(Guid.Empty);
        }

        [TestMethod]
        public void PrimaryObject_Constructor_Valid()
        {
            // This test verifies that the object can be constructed successfully
            var primaryObject = new PrimaryObject(Guid.NewGuid());
        }

        [TestMethod]
        public void PrimaryObject_Description()
        {
            // This test verifies the Description auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            string value = "test";
            var primaryObject = new PrimaryObject(Guid.NewGuid());
            primaryObject.Description = value;
            Assert.AreEqual(value, primaryObject.Description);
        }

        [TestMethod]
        public void PrimaryObject_Id()
        {
            // This test verifies the Id auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            Guid value = Guid.NewGuid();
            var primaryObject = new PrimaryObject(value);
            Assert.AreEqual(value, primaryObject.Id);
        }

        [TestMethod]
        public void PrimaryObject_Name()
        {
            // This test verifies the Name auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            string value = "test";
            var primaryObject = new PrimaryObject(Guid.NewGuid());
            primaryObject.Name = value;
            Assert.AreEqual(value, primaryObject.Name);
        }

        [TestMethod]
        public void PrimaryObject_SecondaryObjects()
        {
            // This test verifies the SecondaryObjects auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            ICollection<SecondaryObject> value = new Collection<SecondaryObject>();
            var primaryObject = new PrimaryObject(Guid.NewGuid());
            primaryObject.SecondaryObjects = value;
            Assert.AreEqual(value, primaryObject.SecondaryObjects);
        }
    }
}
