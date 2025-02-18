using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class SeedMySchoolYearTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SchoolYear",
                columns: new[] { "SchoolYearID", "SY" },
                values: new object[,]
                {
                    { "SCH0101", "2023-2024" },
                    { "SCH0102", "2024-2025" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SchoolYear",
                keyColumn: "SchoolYearID",
                keyValue: "SCH0101");

            migrationBuilder.DeleteData(
                table: "SchoolYear",
                keyColumn: "SchoolYearID",
                keyValue: "SCH0102");
        }
    }
}
