using System.Data.Entity;
using Rightpoint.UnitTesting.Demo.Domain.Models;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Data
{
    public class DemoContext : DbContext
    {
        public DemoContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
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
    }
}
