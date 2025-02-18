using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGradeTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SchoolYearID",
                table: "Grades",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SchoolYear",
                columns: table => new
                {
                    SchoolYearID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SY = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolYear", x => x.SchoolYearID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_SchoolYearID",
                table: "Grades",
                column: "SchoolYearID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_SchoolYear_SchoolYearID",
                table: "Grades",
                column: "SchoolYearID",
                principalTable: "SchoolYear",
                principalColumn: "SchoolYearID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_SchoolYear_SchoolYearID",
                table: "Grades");

            migrationBuilder.DropTable(
                name: "SchoolYear");

            migrationBuilder.DropIndex(
                name: "IX_Grades_SchoolYearID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "SchoolYearID",
                table: "Grades");
        }
    }
}
