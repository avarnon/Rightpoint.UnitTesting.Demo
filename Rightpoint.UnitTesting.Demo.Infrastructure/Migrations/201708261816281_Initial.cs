    using System;
    using System.Data.Entity.Migrations;

namespace Rightpoint.UnitTesting.Demo.Infrastructure.Migrations
{
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
