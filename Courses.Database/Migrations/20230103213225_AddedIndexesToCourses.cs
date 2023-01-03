using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexesToCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Courses_CurrencyName",
                table: "Courses",
                column: "CurrencyName");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Date",
                table: "Courses",
                column: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Courses_CurrencyName",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_Date",
                table: "Courses");
        }
    }
}
