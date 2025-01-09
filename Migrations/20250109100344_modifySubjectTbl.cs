using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class modifySubjectTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YearID",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_YearID",
                table: "Subjects",
                column: "YearID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Year_YearID",
                table: "Subjects",
                column: "YearID",
                principalTable: "Year",
                principalColumn: "YearID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Year_YearID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_YearID",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "YearID",
                table: "Subjects");
        }
    }
}
