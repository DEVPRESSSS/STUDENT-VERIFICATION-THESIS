using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class AddedSchoolYearAndProfessorName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfessorName",
                table: "SubjectsEnrolled",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SyID",
                table: "SubjectsEnrolled",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsEnrolled_SyID",
                table: "SubjectsEnrolled",
                column: "SyID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsEnrolled_SchoolYear_SyID",
                table: "SubjectsEnrolled",
                column: "SyID",
                principalTable: "SchoolYear",
                principalColumn: "SchoolYearID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsEnrolled_SchoolYear_SyID",
                table: "SubjectsEnrolled");

            migrationBuilder.DropIndex(
                name: "IX_SubjectsEnrolled_SyID",
                table: "SubjectsEnrolled");

            migrationBuilder.DropColumn(
                name: "ProfessorName",
                table: "SubjectsEnrolled");

            migrationBuilder.DropColumn(
                name: "SyID",
                table: "SubjectsEnrolled");
        }
    }
}
