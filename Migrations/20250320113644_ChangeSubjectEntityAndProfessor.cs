using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSubjectEntityAndProfessor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Professors_ProfessorID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ProfessorID",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ProfessorID",
                table: "Subjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfessorID",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ProfessorID",
                table: "Subjects",
                column: "ProfessorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Professors_ProfessorID",
                table: "Subjects",
                column: "ProfessorID",
                principalTable: "Professors",
                principalColumn: "ProfessorID");
        }
    }
}
