using Microsoft.EntityFrameworkCore.Migrations;

namespace SSD_Alkolq.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditRecords",
                table: "AuditRecords");

            migrationBuilder.DropColumn(
                name: "Audit_ID",
                table: "AuditRecords");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "AuditRecords",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditRecords",
                table: "AuditRecords",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditRecords",
                table: "AuditRecords");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "AuditRecords");

            migrationBuilder.AddColumn<int>(
                name: "Audit_ID",
                table: "AuditRecords",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditRecords",
                table: "AuditRecords",
                column: "Audit_ID");
        }
    }
}
