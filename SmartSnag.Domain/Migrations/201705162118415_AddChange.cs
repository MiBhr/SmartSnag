namespace SmartSnag.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        CompanyCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.WorkPackage",
                c => new
                    {
                        WorkPackageID = c.Int(nullable: false, identity: true),
                        WorkPackageCode = c.Int(nullable: false),
                        WorkPackageDescription = c.String(nullable: false, maxLength: 30),
                        CompanyID = c.Int(),
                    })
                .PrimaryKey(t => t.WorkPackageID)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .Index(t => t.CompanyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkPackage", "CompanyID", "dbo.Company");
            DropIndex("dbo.WorkPackage", new[] { "CompanyID" });
            DropTable("dbo.WorkPackage");
            DropTable("dbo.Company");
        }
    }
}
