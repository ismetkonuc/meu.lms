using Microsoft.EntityFrameworkCore.Migrations;

namespace meu.lms.dataaccess.Migrations
{
    public partial class AddedRelationShipBetweenArticlesAndCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Articles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CourseId",
                table: "Articles",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Courses_CourseId",
                table: "Articles",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Courses_CourseId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CourseId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Articles");
        }
    }
}
