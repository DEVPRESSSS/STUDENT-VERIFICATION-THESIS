using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class seddSy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Scholarship",
                keyColumn: "ScholarshipID",
                keyValue: "SCHO-1000");

            migrationBuilder.InsertData(
                table: "SchoolYear",
                columns: new[] { "SchoolYearID", "SY" },
                values: new object[] { "SCH0100", "All" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SchoolYear",
                keyColumn: "SchoolYearID",
                keyValue: "SCH0100");

            migrationBuilder.InsertData(
                table: "Scholarship",
                columns: new[] { "ScholarshipID", "Name", "ScholarshipName" },
                values: new object[] { "SCHO-1000", "All", null });
        }
    }
}
