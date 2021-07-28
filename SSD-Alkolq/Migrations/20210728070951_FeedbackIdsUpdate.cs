using Microsoft.EntityFrameworkCore.Migrations;

namespace SSD_Alkolq.Migrations
{
    public partial class FeedbackIdsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedbackRecords",
                table: "FeedbackRecords");

            migrationBuilder.DropColumn(
                name: "Feedback_ID",
                table: "FeedbackRecords");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "FeedbackRecords");

            migrationBuilder.AlterColumn<string>(
                name: "FeedbackType",
                table: "FeedbackRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FeedbackContent",
                table: "FeedbackRecords",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "FeedbackRecords",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "FeedbackRecords",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedbackRecords",
                table: "FeedbackRecords",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackRecords_UserID",
                table: "FeedbackRecords",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackRecords_AspNetUsers_UserID",
                table: "FeedbackRecords",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackRecords_AspNetUsers_UserID",
                table: "FeedbackRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedbackRecords",
                table: "FeedbackRecords");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackRecords_UserID",
                table: "FeedbackRecords");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "FeedbackRecords");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "FeedbackRecords");

            migrationBuilder.AlterColumn<string>(
                name: "FeedbackType",
                table: "FeedbackRecords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FeedbackContent",
                table: "FeedbackRecords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048);

            migrationBuilder.AddColumn<int>(
                name: "Feedback_ID",
                table: "FeedbackRecords",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "FeedbackRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedbackRecords",
                table: "FeedbackRecords",
                column: "Feedback_ID");
        }
    }
}
