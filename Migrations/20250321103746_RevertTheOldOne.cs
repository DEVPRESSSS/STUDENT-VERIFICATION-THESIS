using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class RevertTheOldOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectProfessor");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "SubjectProfessor",
                columns: table => new
                {
                    SubjectProfessorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfessorID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubjectID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectProfessor", x => x.SubjectProfessorID);
                    table.ForeignKey(
                        name: "FK_SubjectProfessor_Professors_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professors",
                        principalColumn: "ProfessorID");
                    table.ForeignKey(
                        name: "FK_SubjectProfessor_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectProfessor_ProfessorID",
                table: "SubjectProfessor",
                column: "ProfessorID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectProfessor_SubjectID",
                table: "SubjectProfessor",
                column: "SubjectID");
        }
    }
}
