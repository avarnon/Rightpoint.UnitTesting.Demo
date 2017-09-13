using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Migrations
{
    /// <summary>
    /// Demo database context.
    /// </summary>
    /// <remarks>
    /// We're using <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> on the class because we can't really unit test it.
    /// There's not much benefit in verifying that EF really applied the configuration defined below.
    /// We're testing <see cref="Rightpoint.UnitTesting.Demo.Infrastructure.Migrations.SeedExtensions"/> separately.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    internal sealed class Configuration : DbMigrationsConfiguration<Rightpoint.UnitTesting.Demo.Infrastructure.Data.DemoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rightpoint.UnitTesting.Demo.Infrastructure.Data.DemoContext context)
        {
            context.AddPrimaryObjects();
            context.SaveChanges();

            context.AddSecondaryObjects();
            context.SaveChanges();
        }
    }
}
