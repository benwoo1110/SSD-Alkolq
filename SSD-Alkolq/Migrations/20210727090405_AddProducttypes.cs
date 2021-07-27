using Microsoft.EntityFrameworkCore.Migrations;

namespace SSD_Alkolq.Migrations
{
    public partial class AddProducttypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_AlcoholProduct_AlcoholProductID",
                table: "ShoppingCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlcoholProduct",
                table: "AlcoholProduct");

            migrationBuilder.RenameTable(
                name: "AlcoholProduct",
                newName: "AlcoholProducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlcoholProducts",
                table: "AlcoholProducts",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_AlcoholProducts_AlcoholProductID",
                table: "ShoppingCart",
                column: "AlcoholProductID",
                principalTable: "AlcoholProducts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_AlcoholProducts_AlcoholProductID",
                table: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlcoholProducts",
                table: "AlcoholProducts");

            migrationBuilder.RenameTable(
                name: "AlcoholProducts",
                newName: "AlcoholProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlcoholProduct",
                table: "AlcoholProduct",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_AlcoholProduct_AlcoholProductID",
                table: "ShoppingCart",
                column: "AlcoholProductID",
                principalTable: "AlcoholProduct",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
