using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rightpoint.UnitTesting.Demo.Domain.Models;
using Rightpoint.UnitTesting.Demo.Infrastructure.Data;
using Rightpoint.UnitTesting.Demo.Infrastructure.Migrations;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Tests.Migrations
{
    [TestClass]
    public class SeedExtensionsTests
    {
        private Mock<DemoContext> _context;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new Mock<DemoContext>();
        }

        [TestMethod]
        public void SeedExtensions_AddPrimaryObjects_DbSet_Empty()
        {
            var modelDbSet = new TestDbSet<PrimaryObject>();
            var source = new List<PrimaryObject>();
            modelDbSet.AddRange(source);
            _context.Setup(_ => _.Set<PrimaryObject>()).Returns(modelDbSet);
            _context.Object.AddPrimaryObjects();

            var destination = modelDbSet.ToList();
            Assert.IsNotNull(destination);
            Assert.AreNotEqual(source.Count, destination.Count);
        }

        [TestMethod]
        public void SeedExtensions_AddPrimaryObjects_DbSet_HasItems()
        {
            var modelDbSet = new TestDbSet<PrimaryObject>();
            var source = new List<PrimaryObject>()
            {
                new PrimaryObject(Guid.NewGuid()),
            };
            modelDbSet.AddRange(source);
            _context.Setup(_ => _.Set<PrimaryObject>()).Returns(modelDbSet);
            _context.Object.AddPrimaryObjects();

            var destination = modelDbSet.ToList();
            Assert.IsNotNull(destination);
            Assert.AreEqual(source.Count, destination.Count);
        }

        [TestMethod]
        public void SeedExtensions_AddSecondaryObjects_DbSet_Empty()
        {
            var modelDbSetPrimary = new TestDbSet<PrimaryObject>();
            var modelDbSetSecondary = new TestDbSet<SecondaryObject>();
            var source = new List<SecondaryObject>();
            modelDbSetPrimary.AddRange(new[]
            {
                new PrimaryObject(Guid.NewGuid()),
                new PrimaryObject(Guid.NewGuid()),
                new PrimaryObject(Guid.NewGuid()),
                new PrimaryObject(Guid.NewGuid()),
            });
            modelDbSetSecondary.AddRange(source);
            _context.Setup(_ => _.Set<PrimaryObject>()).Returns(modelDbSetPrimary);
            _context.Setup(_ => _.Set<SecondaryObject>()).Returns(modelDbSetSecondary);
            _context.Object.AddSecondaryObjects();

            var destination = modelDbSetSecondary.ToList();
            Assert.IsNotNull(destination);
            Assert.AreNotEqual(source.Count, destination.Count);
        }

        [TestMethod]
        public void SeedExtensions_AddSecondaryObjects_DbSet_HasItems()
        {
            var modelDbSetPrimary = new TestDbSet<PrimaryObject>();
            var modelDbSetSecondary = new TestDbSet<SecondaryObject>();
            var source = new List<SecondaryObject>()
            {
                new SecondaryObject(Guid.NewGuid()),
            };
            modelDbSetPrimary.AddRange(new[]
            {
                new PrimaryObject(Guid.NewGuid()),
                new PrimaryObject(Guid.NewGuid()),
                new PrimaryObject(Guid.NewGuid()),
                new PrimaryObject(Guid.NewGuid()),
            });
            modelDbSetSecondary.AddRange(source);
            _context.Setup(_ => _.Set<PrimaryObject>()).Returns(modelDbSetPrimary);
            _context.Setup(_ => _.Set<SecondaryObject>()).Returns(modelDbSetSecondary);
            _context.Object.AddSecondaryObjects();

            var destination = modelDbSetSecondary.ToList();
            Assert.IsNotNull(destination);
            Assert.AreEqual(source.Count, destination.Count);
        }
    }
}
