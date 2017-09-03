using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Mvc.Models;

namespace Rightpoint.UnitTesting.Demo.Mvc.Tests.Models
{
    [TestClass]
    public class SecondaryObjectTests
    {
        [TestMethod]
        public void SecondaryObject_Constructor()
        {
            var secondaryObject = new SecondaryObject();
        }

        [TestMethod]
        public void SecondaryObject_Description()
        {
            string value = "test";
            var secondaryObject = new SecondaryObject();
            secondaryObject.Description = value;
            Assert.AreEqual(value, secondaryObject.Description);
        }

        [TestMethod]
        public void SecondaryObject_Id()
        {
            Guid value = Guid.NewGuid();
            var secondaryObject = new SecondaryObject();
            secondaryObject.Id = value;
            Assert.AreEqual(value, secondaryObject.Id);
        }

        [TestMethod]
        public void SecondaryObject_Name()
        {
            string value = "test";
            var secondaryObject = new SecondaryObject();
            secondaryObject.Name = value;
            Assert.AreEqual(value, secondaryObject.Name);
        }

        [TestMethod]
        public void SecondaryObject_PrimaryObjectId()
        {
            Guid value = Guid.NewGuid();
            var secondaryObject = new SecondaryObject();
            secondaryObject.PrimaryObjectId = value;
            Assert.AreEqual(value, secondaryObject.PrimaryObjectId);
        }
    }
}
