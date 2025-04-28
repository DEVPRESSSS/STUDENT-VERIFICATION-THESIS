using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class AddedIcollections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
