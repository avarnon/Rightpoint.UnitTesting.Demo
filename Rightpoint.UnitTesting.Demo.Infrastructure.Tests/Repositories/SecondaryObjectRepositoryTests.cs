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
    public class SecondaryObjectRepositoryTests : BaseRepositoryTests<DemoContext, SecondaryObject>
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SecondaryObjectRepository_Constructor_Context_Null()
        {
            // This test verifies that the repository will not accept null dependencies
            var repository = this.ConstructRepository(null);
        }

        [TestMethod]
        public void SecondaryObjectRepository_Constructor_Valid()
        {
            // This test verifies that the repository can be constructed successfully
            var repository = this.ConstructRepository(this.Context.Object);
        }

        protected override BaseRepository<SecondaryObject> ConstructRepository(DemoContext context)
        {
            return new SecondaryObjectRepository(context);
        }

        protected override SecondaryObject ContstructModel(Guid id)
        {
            var primaryObjectId = Guid.NewGuid();
            var primaryObject = new PrimaryObject(primaryObjectId)
            {
                Name = $"Name {id}",
                Description = $"Description {id}",
                SecondaryObjects = new List<SecondaryObject>()
                {
                    new SecondaryObject(id),
                },
            };
            foreach (var secondaryObject in primaryObject.SecondaryObjects)
            {
                secondaryObject.Name = $"Name {secondaryObject.Id}";
                secondaryObject.Description = $"Description {secondaryObject.Id}";
                secondaryObject.PrimaryObject = primaryObject;
                secondaryObject.PrimaryObject_Id = primaryObject.Id;
            }

            return primaryObject.SecondaryObjects.Single();
        }

        protected override Expression<Func<DemoContext, DbSet<SecondaryObject>>> GetDbSetProperty()
        {
            return context => context.Set<SecondaryObject>();
        }

        protected override List<SecondaryObject> GetSeedModels()
        {
            return Enumerable.Range(0, 10).Select(_ => this.ContstructModel(Guid.NewGuid())).ToList();
        }
    }
}
