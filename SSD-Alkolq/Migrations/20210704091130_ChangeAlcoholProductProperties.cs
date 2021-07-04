using Microsoft.EntityFrameworkCore.Migrations;

namespace SSD_Alkolq.Migrations
{
    public partial class ChangeAlcoholProductProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "AlchoholProduct");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "AlchoholProduct",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AlchoholProduct",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "AlchoholProduct");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AlchoholProduct");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "AlchoholProduct",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
