namespace SportsStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageData", c => c.Binary());
            AddColumn("dbo.Products", "ImageMymeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageMymeType");
            DropColumn("dbo.Products", "ImageData");
        }
    }
}
