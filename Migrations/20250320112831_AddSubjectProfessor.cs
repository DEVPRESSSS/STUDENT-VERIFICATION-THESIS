using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectProfessor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubjectProfessor",
                columns: table => new
                {
                    SubjectProfessorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfessorID = table.Column<string>(type: "nvarchar(450)", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectProfessor");
        }
    }
}
