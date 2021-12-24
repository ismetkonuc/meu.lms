using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace meu.lms.dataaccess.Migrations
{
    public partial class AddedPostedTimeANDTitleColumnsInArticleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PostedTime",
                table: "Articles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostedTime",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Articles");
        }
    }
}
