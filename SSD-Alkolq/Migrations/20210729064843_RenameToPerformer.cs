using Microsoft.EntityFrameworkCore.Migrations;

namespace SSD_Alkolq.Migrations
{
    public partial class RenameToPerformer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "AuditRecords");

            migrationBuilder.AddColumn<string>(
                name: "Performer",
                table: "AuditRecords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Performer",
                table: "AuditRecords");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "AuditRecords",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
