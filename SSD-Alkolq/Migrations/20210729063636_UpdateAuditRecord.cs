using Microsoft.EntityFrameworkCore.Migrations;

namespace SSD_Alkolq.Migrations
{
    public partial class UpdateAuditRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditActionType",
                table: "AuditRecords");

            migrationBuilder.DropColumn(
                name: "KeyAlcoholFieldID",
                table: "AuditRecords");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "AuditRecords",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AffectedData",
                table: "AuditRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AffectedDataID",
                table: "AuditRecords",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "AuditRecords");

            migrationBuilder.DropColumn(
                name: "AffectedData",
                table: "AuditRecords");

            migrationBuilder.DropColumn(
                name: "AffectedDataID",
                table: "AuditRecords");

            migrationBuilder.AddColumn<string>(
                name: "AuditActionType",
                table: "AuditRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KeyAlcoholFieldID",
                table: "AuditRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
