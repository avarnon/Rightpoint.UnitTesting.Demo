using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rightpoint.UnitTesting.Demo.Domain.Models;
using Rightpoint.UnitTesting.Demo.Infrastructure.Repositories;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Tests.Repositories
{
    /// <summary>
    /// Base Repository tests
    /// </summary>
    /// <typeparam name="TContext">The type of the DB Context</typeparam>
    /// <typeparam name="TModel">The type of the model operated on by the repository</typeparam>
    /// <remarks>
    /// This class contains tests that are common to all repositories that inherit from BaseRepository.
    /// </remarks>
    public abstract class BaseRepositoryTests<TContext, TModel>
        where TContext : DbContext
        where TModel : class, IIdentifiable<Guid>
    {
        protected Mock<TContext> Context { get; private set; }

        protected List<TModel> TestModels { get; private set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.Context = new Mock<TContext>();
            var modelDbSet = new TestDbSet<TModel>();
            this.TestModels = this.GetSeedModels();

            modelDbSet.AddRange(this.TestModels);

            this.Context.Setup(this.GetDbSetProperty()).Returns(modelDbSet);
            this.Context.Setup(x => x.Set<TModel>()).Returns(modelDbSet);
        }

        [TestMethod]
        public async Task Repository_Add()
        {
            // This test verifies that Add successfully adds the item to the backing DbSet
            var newModel = this.ContstructModel(Guid.NewGuid());
            var repository = this.ConstructRepository(this.Context.Object);

            repository.Add(newModel);
            var models = await repository.GetAllAsync();
            Assert.IsNotNull(models);
            Assert.AreEqual(this.TestModels.Count + 1, models.Count);

            var addedModel = models.SingleOrDefault(x => x.Id == newModel.Id);
            Assert.IsNotNull(addedModel);
        }

        [TestMethod]
        public async Task Repository_AddMany()
        {
            // This test verifies that AddMany successfully adds the items to the backing DbSet
            var newModels = new[]
            {
                this.ContstructModel(Guid.NewGuid()),
                this.ContstructModel(Guid.NewGuid()),
                this.ContstructModel(Guid.NewGuid()),
                this.ContstructModel(Guid.NewGuid()),
            };
            var repository = this.ConstructRepository(this.Context.Object);

            repository.AddMany(newModels);
            var models = await repository.GetAllAsync();
            Assert.IsNotNull(models);
            Assert.AreEqual(this.TestModels.Count + 4, models.Count);

            foreach (var newModel in newModels)
            {
                var addedModel = models.SingleOrDefault(x => x.Id == newModel.Id);
                Assert.IsNotNull(addedModel);
            }
        }

        [TestMethod]
        public async Task Repository_GetAllAsync()
        {
            // This test verifies that GetAllAsync successfully all items from the backing DbSet
            var repository = this.ConstructRepository(this.Context.Object);
            var models = await repository.GetAllAsync();
            Assert.IsNotNull(models);
            Assert.AreEqual(this.TestModels.Count, models.Count);

            foreach (var testModel in this.TestModels)
            {
                var model = models.SingleOrDefault(x => x.Id == testModel.Id);
                Assert.IsNotNull(model);
            }
        }

        [TestMethod]
        public async Task Repository_GetByIdAsync_Valid()
        {
            // This test verifies that GetByIdAsync successfully an existing item from the backing DbSet
            var repository = this.ConstructRepository(this.Context.Object);
            var selectedTestModel = this.TestModels.First();
            var selectedId = selectedTestModel.Id;
            var model = await repository.GetByIdAsync(selectedId);
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public async Task Repository_GetByIdAsync_NotPresent()
        {
            // This test verifies that GetByIdAsync returns null for a non-existant item in the backing DbSet
            var repository = this.ConstructRepository(this.Context.Object);
            var selectedId = Guid.Empty;
            var model = await repository.GetByIdAsync(selectedId);
            Assert.IsNull(model);
        }

        [TestMethod]
        public async Task Repository_GetByIdsAsync_Valid()
        {
            // This test verifies that GetByIdAsync successfully existing items from the backing DbSet
            var repository = this.ConstructRepository(this.Context.Object);
            var selectedTestModels = this.TestModels.Take(2).ToList();
            var selectedIds = selectedTestModels.Select(x => x.Id).ToList();
            var models = await repository.GetByIdsAsync(selectedIds);
            Assert.IsNotNull(models);
            Assert.AreEqual(selectedTestModels.Count, models.Count);

            foreach (var testModel in selectedTestModels)
            {
                var model = models.SingleOrDefault(x => x.Id == testModel.Id);
                Assert.IsNotNull(model);
            }
        }

        [TestMethod]
        public async Task Repository_GetByIdsAsync_NotPresent()
        {
            // This test verifies that GetByIdAsync returns an empty set for non-existant items in the backing DbSet
            var repository = this.ConstructRepository(this.Context.Object);
            var selectedIds = Enumerable.Range(0, 4).Select(x => Guid.Empty).ToArray();
            var models = await repository.GetByIdsAsync(selectedIds);
            Assert.IsNotNull(models);
            Assert.AreEqual(0, models.Count);
        }

        [TestMethod]
        public async Task Repository_Remove()
        {
            // This test verifies that Remove successfully removes the item from the backing DbSet
            var repository = this.ConstructRepository(this.Context.Object);
            var source = this.TestModels.First();
            var removed = repository.Remove(source);
            Assert.IsNotNull(removed);
            Assert.AreEqual(source.Id, removed.Id);
        }

        [TestMethod]
        public async Task Repository_Set()
        {
            // This test verifies that Set method returns an IQueryable of the correct model
            var repository = this.ConstructRepository(this.Context.Object);
            var setProperties = repository.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(p => p.Name == "Set")
                .ToArray();
            Assert.IsNotNull(setProperties);
            Assert.IsTrue(setProperties.Length > 0);

            PropertyInfo setProperty = null;

            if (setProperties.Length == 1)
            {
                setProperty = setProperties[0];
            }
            else
            {
                setProperty = setProperties.SingleOrDefault(x => x.DeclaringType == repository.GetType());
                if (setProperty == null)
                {
                    setProperty = setProperties[0];
                }
            }

            var set = setProperty.GetValue(repository);
            Assert.IsNotNull(set);
            Assert.IsInstanceOfType(set, typeof(IQueryable<TModel>));
        }

        protected abstract BaseRepository<TModel> ConstructRepository(TContext context);

        protected abstract List<TModel> GetSeedModels();

        protected abstract Expression<Func<TContext, DbSet<TModel>>> GetDbSetProperty();

        protected abstract TModel ContstructModel(Guid id);
    }
}
