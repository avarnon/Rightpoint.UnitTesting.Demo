using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rightpoint.UnitTesting.Demo.Infrastructure.Data;
using Rightpoint.UnitTesting.Demo.Infrastructure.Services;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Tests.Services
{
    [TestClass]
    public class UnitOfWorkTests
    {
        private Mock<DemoContext> _context;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new Mock<DemoContext>();
        }

        [TestMethod]
        public void UnitOfWork_Constructor_Valid()
        {
            var unitOfWork = new UnitOfWork(_context.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UnitOfWork_Constructor_Context_Null()
        {
            var unitOfWork = new UnitOfWork(null);
        }

        [TestMethod]
        public async Task UnitOfWork_SaveChangesAsync()
        {
            var unitOfWork = new UnitOfWork(_context.Object);
            _context.Setup(_ => _.SaveChangesAsync()).ReturnsAsync(1);
            var rowsAffected = await unitOfWork.SaveChangesAsync();
            Assert.AreEqual(1, rowsAffected);
        }
    }
}
