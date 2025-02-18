using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedTheStaffDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Staffs_StaffID",
                table: "Grades");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Staffs_StaffID",
                table: "Grades",
                column: "StaffID",
                principalTable: "Staffs",
                principalColumn: "StaffID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Staffs_StaffID",
                table: "Grades");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Staffs_StaffID",
                table: "Grades",
                column: "StaffID",
                principalTable: "Staffs",
                principalColumn: "StaffID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
