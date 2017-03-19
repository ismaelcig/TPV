namespace TPV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration0802 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VentaProductoes", "Venta_VentaID", "dbo.Ventas");
            DropForeignKey("dbo.VentaProductoes", "Producto_ProductoID", "dbo.Productoes");
            DropIndex("dbo.VentaProductoes", new[] { "Venta_VentaID" });
            DropIndex("dbo.VentaProductoes", new[] { "Producto_ProductoID" });
            DropTable("dbo.VentaProductoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VentaProductoes",
                c => new
                    {
                        Venta_VentaID = c.Int(nullable: false),
                        Producto_ProductoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Venta_VentaID, t.Producto_ProductoID });
            
            CreateIndex("dbo.VentaProductoes", "Producto_ProductoID");
            CreateIndex("dbo.VentaProductoes", "Venta_VentaID");
            AddForeignKey("dbo.VentaProductoes", "Producto_ProductoID", "dbo.Productoes", "ProductoID", cascadeDelete: true);
            AddForeignKey("dbo.VentaProductoes", "Venta_VentaID", "dbo.Ventas", "VentaID", cascadeDelete: true);
        }
    }
}
