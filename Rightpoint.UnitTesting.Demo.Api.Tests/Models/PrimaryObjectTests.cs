using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Api.Models;

namespace Rightpoint.UnitTesting.Demo.Api.Tests.Models
{
    [TestClass]
    public class PrimaryObjectTests
    {
        [TestMethod]
        public void PrimaryObject_Constructor()
        {
            var primaryObject = new PrimaryObject();
        }

        [TestMethod]
        public void PrimaryObject_Description()
        {
            string value = "test";
            var primaryObject = new PrimaryObject();
            primaryObject.Description = value;
            Assert.AreEqual(value, primaryObject.Description);
        }

        [TestMethod]
        public void PrimaryObject_Id()
        {
            Guid value = Guid.NewGuid();
            var primaryObject = new PrimaryObject();
            primaryObject.Id = value;
            Assert.AreEqual(value, primaryObject.Id);
        }

        [TestMethod]
        public void PrimaryObject_Name()
        {
            string value = "test";
            var primaryObject = new PrimaryObject();
            primaryObject.Name = value;
            Assert.AreEqual(value, primaryObject.Name);
        }

        [TestMethod]
        public void PrimaryObject_SecondaryObjects()
        {
            IEnumerable<SecondaryObject> value = new SecondaryObject[0];
            var primaryObject = new PrimaryObject();
            primaryObject.SecondaryObjects = value;
            Assert.AreEqual(value, primaryObject.SecondaryObjects);
        }
    }
}
