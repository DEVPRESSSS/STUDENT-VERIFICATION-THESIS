using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class NullableYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Year_YearID",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "YearID",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Year_YearID",
                table: "Students",
                column: "YearID",
                principalTable: "Year",
                principalColumn: "YearID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Year_YearID",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "YearID",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Year_YearID",
                table: "Students",
                column: "YearID",
                principalTable: "Year",
                principalColumn: "YearID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
