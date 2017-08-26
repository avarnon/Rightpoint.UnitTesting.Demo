namespace Rightpoint.UnitTesting.Demo.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
