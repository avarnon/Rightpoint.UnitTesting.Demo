using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Data
{
    /// <summary>
    /// Demo database context.
    /// </summary>
    /// <remarks>
    /// We're using <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> on the class because we can't really unit test it.
    /// There's not much benefit in verifying that EF really applied the configuration defined below.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public class DemoContext : DbContext
    {
        public DemoContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Initialize();
        }

        public DemoContext()
        {
            this.Initialize();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrimaryObject>()
                .Property(x => x.Name)
                .IsRequired();
            modelBuilder.Entity<PrimaryObject>()
                .Property(x => x.Description)
                .IsRequired();
            modelBuilder.Entity<PrimaryObject>()
                .HasMany(x => x.SecondaryObjects)
                .WithRequired(x => x.PrimaryObject)
                .HasForeignKey(x => x.PrimaryObject_Id);

            modelBuilder.Entity<SecondaryObject>()
                .Property(x => x.Name)
                .IsRequired();
            modelBuilder.Entity<SecondaryObject>()
                .Property(x => x.Description)
                .IsRequired();
        }

        private void Initialize()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
