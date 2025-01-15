using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class AddedSemestedTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Professors_ProfessorID",
                table: "Subjects");

            migrationBuilder.AlterColumn<string>(
                name: "ProfessorID",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "SemesterID",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    SemesterID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SemesterName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.SemesterID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SemesterID",
                table: "Subjects",
                column: "SemesterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Professors_ProfessorID",
                table: "Subjects",
                column: "ProfessorID",
                principalTable: "Professors",
                principalColumn: "ProfessorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Semesters_SemesterID",
                table: "Subjects",
                column: "SemesterID",
                principalTable: "Semesters",
                principalColumn: "SemesterID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Professors_ProfessorID",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Semesters_SemesterID",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SemesterID",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SemesterID",
                table: "Subjects");

            migrationBuilder.AlterColumn<string>(
                name: "ProfessorID",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Professors_ProfessorID",
                table: "Subjects",
                column: "ProfessorID",
                principalTable: "Professors",
                principalColumn: "ProfessorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
