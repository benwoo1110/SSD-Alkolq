using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SSD_Alkolq.Migrations
{
    public partial class AddRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductRatings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    DateTimeStamp = table.Column<DateTime>(nullable: false),
                    RatingContent = table.Column<string>(nullable: false),
                    RatingStar = table.Column<int>(nullable: false),
                    AlcoholProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRatings", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRatings");
        }
    }
}
