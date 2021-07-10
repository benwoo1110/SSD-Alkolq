using Microsoft.EntityFrameworkCore.Migrations;

namespace SSD_Alkolq.Migrations
{
    public partial class FixSpellingIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AlchoholProduct",
                table: "AlchoholProduct");

            migrationBuilder.RenameTable(
                name: "AlchoholProduct",
                newName: "AlcoholProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlcoholProduct",
                table: "AlcoholProduct",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AlcoholProduct",
                table: "AlcoholProduct");

            migrationBuilder.RenameTable(
                name: "AlcoholProduct",
                newName: "AlchoholProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlchoholProduct",
                table: "AlchoholProduct",
                column: "ID");
        }
    }
}
