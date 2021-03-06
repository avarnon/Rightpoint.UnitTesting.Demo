﻿using System;
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
            // This test verifies that the object's constructor will not accept null dependencies
            var secondaryObject = new SecondaryObject(Guid.Empty);
        }

        [TestMethod]
        public void SecondaryObject_Constructor_Valid()
        {
            // This test verifies that the object can be constructed successfully
            var secondaryObject = new SecondaryObject(Guid.NewGuid());
        }

        [TestMethod]
        public void SecondaryObject_Description()
        {
            // This test verifies the Description auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            string value = "test";
            var secondaryObject = new SecondaryObject(Guid.NewGuid());
            secondaryObject.Description = value;
            Assert.AreEqual(value, secondaryObject.Description);
        }

        [TestMethod]
        public void SecondaryObject_Id()
        {
            // This test verifies the Id auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            Guid value = Guid.NewGuid();
            var secondaryObject = new SecondaryObject(value);
            Assert.AreEqual(value, secondaryObject.Id);
        }

        [TestMethod]
        public void SecondaryObject_Name()
        {
            // This test verifies the Name auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            string value = "test";
            var secondaryObject = new SecondaryObject(Guid.NewGuid());
            secondaryObject.Name = value;
            Assert.AreEqual(value, secondaryObject.Name);
        }

        [TestMethod]
        public void SecondaryObject_PrimaryObject()
        {
            // This test verifies the PrimaryObject auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            PrimaryObject value = new PrimaryObject(Guid.NewGuid());
            var secondaryObject = new SecondaryObject(Guid.NewGuid());
            secondaryObject.PrimaryObject = value;
            Assert.AreEqual(value, secondaryObject.PrimaryObject);
        }

        [TestMethod]
        public void SecondaryObject_PrimaryObject_Id()
        {
            // This test verifies the PrimaryObject_Id auto-property works.
            // Note: this test is useless except for code coverage since we are testing that an auto-property works.
            Guid value = Guid.NewGuid();
            var secondaryObject = new SecondaryObject(Guid.NewGuid());
            secondaryObject.PrimaryObject_Id = value;
            Assert.AreEqual(value, secondaryObject.PrimaryObject_Id);
        }
    }
}
