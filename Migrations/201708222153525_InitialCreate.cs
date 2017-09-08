namespace TurAfrikb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModelOrSize = c.String(),
                        Price = c.Int(nullable: false),
                        Brand = c.String(),
                        Gender = c.String(),
                        OtherDetails = c.String(),
                        Country = c.String(),
                        Quantity = c.Int(nullable: false),
                        InPromotion = c.Boolean(nullable: false),
                        category_CategoryId = c.Int(),
                        SellerShop_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.category_CategoryId)
                .ForeignKey("dbo.Stores", t => t.SellerShop_Id)
                .Index(t => t.category_CategoryId)
                .Index(t => t.SellerShop_Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Country = c.String(),
                        Manager_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Manager_Id)
                .Index(t => t.Manager_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SellerShop_Id", "dbo.Stores");
            DropForeignKey("dbo.Stores", "Manager_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Stores", new[] { "Manager_Id" });
            DropIndex("dbo.Products", new[] { "SellerShop_Id" });
            DropIndex("dbo.Products", new[] { "category_CategoryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Stores");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
