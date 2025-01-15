using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class updateSubjectTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Semesters_SemesterID",
                table: "Subjects");

            migrationBuilder.AlterColumn<string>(
                name: "SemesterID",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Semesters_SemesterID",
                table: "Subjects",
                column: "SemesterID",
                principalTable: "Semesters",
                principalColumn: "SemesterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Semesters_SemesterID",
                table: "Subjects");

            migrationBuilder.AlterColumn<string>(
                name: "SemesterID",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Semesters_SemesterID",
                table: "Subjects",
                column: "SemesterID",
                principalTable: "Semesters",
                principalColumn: "SemesterID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
