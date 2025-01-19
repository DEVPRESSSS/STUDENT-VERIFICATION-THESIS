using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class seedScholarshipTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Scholarship",
                columns: new[] { "ScholarshipID", "Name", "ScholarshipName" },
                values: new object[,]
                {
                    { "SCHO-1001", "Scholar", null },
                    { "SCHO-1002", "Non-scholar", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Scholarship",
                keyColumn: "ScholarshipID",
                keyValue: "SCHO-1001");

            migrationBuilder.DeleteData(
                table: "Scholarship",
                keyColumn: "ScholarshipID",
                keyValue: "SCHO-1002");
        }
    }
}
