using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class ModifyGradeTblWithEnrollmentFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnrollementID",
                table: "Grades",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_EnrollementID",
                table: "Grades",
                column: "EnrollementID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_SubjectsEnrolled_EnrollementID",
                table: "Grades",
                column: "EnrollementID",
                principalTable: "SubjectsEnrolled",
                principalColumn: "EnrollmentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_SubjectsEnrolled_EnrollementID",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_EnrollementID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "EnrollementID",
                table: "Grades");
        }
    }
}
