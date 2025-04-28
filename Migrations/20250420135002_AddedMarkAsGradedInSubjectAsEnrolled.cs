using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class AddedMarkAsGradedInSubjectAsEnrolled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professors_Subjects_SubjectsEntitySubjectID",
                table: "Professors");

            migrationBuilder.DropIndex(
                name: "IX_Professors_SubjectsEntitySubjectID",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "SubjectsEntitySubjectID",
                table: "Professors");

            migrationBuilder.AlterColumn<string>(
                name: "ProfessorName",
                table: "SubjectsEnrolled",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsMarkAsGraded",
                table: "SubjectsEnrolled",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMarkAsGraded",
                table: "SubjectsEnrolled");

            migrationBuilder.AlterColumn<string>(
                name: "ProfessorName",
                table: "SubjectsEnrolled",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectsEntitySubjectID",
                table: "Professors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professors_SubjectsEntitySubjectID",
                table: "Professors",
                column: "SubjectsEntitySubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_Subjects_SubjectsEntitySubjectID",
                table: "Professors",
                column: "SubjectsEntitySubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID");
        }
    }
}
