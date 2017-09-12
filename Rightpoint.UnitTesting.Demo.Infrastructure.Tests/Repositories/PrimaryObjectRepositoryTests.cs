using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rightpoint.UnitTesting.Demo.Domain.Models;
using Rightpoint.UnitTesting.Demo.Infrastructure.Data;
using Rightpoint.UnitTesting.Demo.Infrastructure.Repositories;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class PrimaryObjectRepositoryTests : BaseRepositoryTests<DemoContext, PrimaryObject>
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PrimaryObjectRepository_Constructor_Context_Null()
        {
            // This test verifies that the repository will not accept null dependencies
            var repository = this.ConstructRepository(null);
        }

        [TestMethod]
        public void PrimaryObjectRepository_Constructor_Valid()
        {
            // This test verifies that the repository can be constructed successfully
            var repository = this.ConstructRepository(this.Context.Object);
        }

        protected override BaseRepository<PrimaryObject> ConstructRepository(DemoContext context)
        {
            return new PrimaryObjectRepository(context);
        }

        protected override PrimaryObject ContstructModel(Guid id)
        {
            var primaryObject = new PrimaryObject(id)
            {
                Name = $"Name {id}",
                Description = $"Description {id}",
                SecondaryObjects = Enumerable.Range(1, 10).Select(i => new SecondaryObject(Guid.NewGuid())).ToList(),
            };

            foreach (var secondaryObject in primaryObject.SecondaryObjects)
            {
                secondaryObject.Name = $"Name {secondaryObject.Id}";
                secondaryObject.Description = $"Description {secondaryObject.Id}";
                secondaryObject.PrimaryObject = primaryObject;
                secondaryObject.PrimaryObject_Id = primaryObject.Id;
            }

            return primaryObject;
        }

        protected override Expression<Func<DemoContext, DbSet<PrimaryObject>>> GetDbSetProperty()
        {
            return context => context.Set<PrimaryObject>();
        }

        protected override List<PrimaryObject> GetSeedModels()
        {
            return Enumerable.Range(0, 10).Select(_ => this.ContstructModel(Guid.NewGuid())).ToList();
        }
    }
}
