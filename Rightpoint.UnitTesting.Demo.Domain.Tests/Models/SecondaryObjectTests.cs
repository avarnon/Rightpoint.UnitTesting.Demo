using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Domain.Tests.Models
{
    [TestClass]
    public class SecondaryObjectTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SecondaryObject_Constructor_Id_Empty()
        {
            var secondaryObject = new SecondaryObject(Guid.Empty);
        }

        [TestMethod]
        public void SecondaryObject_Constructor_Valid()
        {
            var secondaryObject = new SecondaryObject(Guid.NewGuid());
        }

        [TestMethod]
        public void SecondaryObject_Description()
        {
            string value = "test";
            var secondaryObject = new SecondaryObject(Guid.NewGuid());
            secondaryObject.Description = value;
            Assert.AreEqual(value, secondaryObject.Description);
        }

        [TestMethod]
        public void SecondaryObject_Id()
        {
            Guid value = Guid.NewGuid();
            var secondaryObject = new SecondaryObject(value);
            Assert.AreEqual(value, secondaryObject.Id);
        }

        [TestMethod]
        public void SecondaryObject_Name()
        {
            string value = "test";
            var secondaryObject = new SecondaryObject(Guid.NewGuid());
            secondaryObject.Name = value;
            Assert.AreEqual(value, secondaryObject.Name);
        }

        [TestMethod]
        public void SecondaryObject_PrimaryObject()
        {
            PrimaryObject value = new PrimaryObject(Guid.NewGuid());
            var secondaryObject = new SecondaryObject(Guid.NewGuid());
            secondaryObject.PrimaryObject = value;
            Assert.AreEqual(value, secondaryObject.PrimaryObject);
        }

        [TestMethod]
        public void SecondaryObject_PrimaryObject_Id()
        {
            Guid value = Guid.NewGuid();
            var secondaryObject = new SecondaryObject(Guid.NewGuid());
            secondaryObject.PrimaryObject_Id = value;
            Assert.AreEqual(value, secondaryObject.PrimaryObject_Id);
        }
    }
}
