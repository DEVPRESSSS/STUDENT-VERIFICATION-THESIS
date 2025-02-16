using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedTheName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_SubjectsEnrolled_EnrollementID",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "EnrollementID",
                table: "Grades",
                newName: "EnrollmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_EnrollementID",
                table: "Grades",
                newName: "IX_Grades_EnrollmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_SubjectsEnrolled_EnrollmentID",
                table: "Grades",
                column: "EnrollmentID",
                principalTable: "SubjectsEnrolled",
                principalColumn: "EnrollmentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_SubjectsEnrolled_EnrollmentID",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "EnrollmentID",
                table: "Grades",
                newName: "EnrollementID");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_EnrollmentID",
                table: "Grades",
                newName: "IX_Grades_EnrollementID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_SubjectsEnrolled_EnrollementID",
                table: "Grades",
                column: "EnrollementID",
                principalTable: "SubjectsEnrolled",
                principalColumn: "EnrollmentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
