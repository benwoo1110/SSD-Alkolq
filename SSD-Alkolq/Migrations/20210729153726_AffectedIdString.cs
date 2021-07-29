using Microsoft.EntityFrameworkCore.Migrations;

namespace SSD_Alkolq.Migrations
{
    public partial class AffectedIdString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AffectedDataID",
                table: "AuditRecords",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AffectedDataID",
                table: "AuditRecords",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
