namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBasket2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreateAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.BasketItems", "BasketId", c => c.String(maxLength: 128));
            CreateIndex("dbo.BasketItems", "BasketId");
            AddForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            AlterColumn("dbo.BasketItems", "BasketId", c => c.String());
            DropTable("dbo.Baskets");
        }
    }
}
