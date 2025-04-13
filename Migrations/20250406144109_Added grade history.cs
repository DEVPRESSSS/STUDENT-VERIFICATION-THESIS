using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class Addedgradehistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GradeVersion",
                columns: table => new
                {
                    HistoryID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GradeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GradeValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrollmentID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolYearID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeVersion", x => x.HistoryID);
                    table.ForeignKey(
                        name: "FK_GradeVersion_Grades_GradeID",
                        column: x => x.GradeID,
                        principalTable: "Grades",
                        principalColumn: "GradeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeVersion_GradeID",
                table: "GradeVersion",
                column: "GradeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradeVersion");
        }
    }
}
