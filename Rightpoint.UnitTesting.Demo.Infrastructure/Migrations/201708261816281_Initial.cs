using System;
using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Migrations
{
    /// <summary>
    /// Initial database migration.
    /// </summary>
    /// <remarks>
    /// We're using <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> on the class because we can't really unit test it.
    /// There's not much benefit in verifying that EF really applied the configuration defined below.
    /// </remarks>
    [ExcludeFromCodeCoverage]
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrimaryObjects",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(nullable: false),
                    Description = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SecondaryObjects",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    PrimaryObject_Id = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrimaryObjects", t => t.PrimaryObject_Id, cascadeDelete: true)
                .Index(t => t.PrimaryObject_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.SecondaryObjects", "PrimaryObject_Id", "dbo.PrimaryObjects");
            DropIndex("dbo.SecondaryObjects", new[] { "PrimaryObject_Id" });
            DropTable("dbo.SecondaryObjects");
            DropTable("dbo.PrimaryObjects");
        }
    }
}
