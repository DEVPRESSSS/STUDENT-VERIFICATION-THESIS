using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class ChangedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeVersion_Grades_GradeID",
                table: "GradeVersion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradeVersion",
                table: "GradeVersion");

            migrationBuilder.RenameTable(
                name: "GradeVersion",
                newName: "GradeHistory");

            migrationBuilder.RenameIndex(
                name: "IX_GradeVersion_GradeID",
                table: "GradeHistory",
                newName: "IX_GradeHistory_GradeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradeHistory",
                table: "GradeHistory",
                column: "HistoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeHistory_Grades_GradeID",
                table: "GradeHistory",
                column: "GradeID",
                principalTable: "Grades",
                principalColumn: "GradeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeHistory_Grades_GradeID",
                table: "GradeHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GradeHistory",
                table: "GradeHistory");

            migrationBuilder.RenameTable(
                name: "GradeHistory",
                newName: "GradeVersion");

            migrationBuilder.RenameIndex(
                name: "IX_GradeHistory_GradeID",
                table: "GradeVersion",
                newName: "IX_GradeVersion_GradeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GradeVersion",
                table: "GradeVersion",
                column: "HistoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeVersion_Grades_GradeID",
                table: "GradeVersion",
                column: "GradeID",
                principalTable: "Grades",
                principalColumn: "GradeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
