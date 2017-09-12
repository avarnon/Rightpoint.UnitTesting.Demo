using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Api.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Tests.Models
{
    [TestClass]
    public class SecondaryObjectTests
    {
        [TestMethod]
        public void SecondaryObject_Constructor()
        {
            // This test verifies that the default constructor works.
            // Note: this test is useless except for code coverage since we are testing the default constructor with no custom logic.
            var secondaryObject = new SecondaryObject();
        }

        [TestMethod]
        public void SecondaryObject_Description()
        {
            // This test verifies the Description auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            string value = "test";
            var secondaryObject = new SecondaryObject();
            secondaryObject.Description = value;
            Assert.AreEqual(value, secondaryObject.Description);
        }

        [TestMethod]
        public void SecondaryObject_Id()
        {
            // This test verifies the Id auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            Guid value = Guid.NewGuid();
            var secondaryObject = new SecondaryObject();
            secondaryObject.Id = value;
            Assert.AreEqual(value, secondaryObject.Id);
        }

        [TestMethod]
        public void SecondaryObject_Name()
        {
            // This test verifies the Name auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            string value = "test";
            var secondaryObject = new SecondaryObject();
            secondaryObject.Name = value;
            Assert.AreEqual(value, secondaryObject.Name);
        }

        [TestMethod]
        public void SecondaryObject_PrimaryObject()
        {
            // This test verifies the PrimaryObject auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            PrimaryObject value = new PrimaryObject();
            var secondaryObject = new SecondaryObject();
            secondaryObject.PrimaryObject = value;
            Assert.AreEqual(value, secondaryObject.PrimaryObject);
        }
    }
}
