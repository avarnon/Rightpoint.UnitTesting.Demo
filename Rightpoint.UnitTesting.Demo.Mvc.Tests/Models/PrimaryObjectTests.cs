using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Mvc.Models;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.Models
{
    [TestClass]
    public class PrimaryObjectTests
    {
        [TestMethod]
        public void PrimaryObject_Constructor()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the default constructor with no custom logic.
            var primaryObject = new PrimaryObject();
        }

        [TestMethod]
        public void PrimaryObject_Description()
        {
            // This test verifies the Description auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            string value = "test";
            var primaryObject = new PrimaryObject();
            primaryObject.Description = value;
            Assert.AreEqual(value, primaryObject.Description);
        }

        [TestMethod]
        public void PrimaryObject_Id()
        {
            // This test verifies the Id auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            Guid value = Guid.NewGuid();
            var primaryObject = new PrimaryObject();
            primaryObject.Id = value;
            Assert.AreEqual(value, primaryObject.Id);
        }

        [TestMethod]
        public void PrimaryObject_Name()
        {
            // This test verifies the Name auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            string value = "test";
            var primaryObject = new PrimaryObject();
            primaryObject.Name = value;
            Assert.AreEqual(value, primaryObject.Name);
        }

        [TestMethod]
        public void PrimaryObject_SecondaryObjects()
        {
            // This test verifies the SecondaryObjects auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            IEnumerable<SecondaryObject> value = new SecondaryObject[0];
            var primaryObject = new PrimaryObject();
            primaryObject.SecondaryObjects = value;
            Assert.AreEqual(value, primaryObject.SecondaryObjects);
        }
    }
}
