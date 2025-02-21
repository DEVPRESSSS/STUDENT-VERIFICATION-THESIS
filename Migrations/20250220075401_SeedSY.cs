using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class SeedSY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Scholarship",
                columns: new[] { "ScholarshipID", "Name", "ScholarshipName" },
                values: new object[] { "SCHO-1000", "All", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Scholarship",
                keyColumn: "ScholarshipID",
                keyValue: "SCHO-1000");
        }
    }
}
