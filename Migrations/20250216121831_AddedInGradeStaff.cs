using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class AddedInGradeStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StaffID",
                table: "Grades",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StaffID",
                table: "Grades",
                column: "StaffID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Staffs_StaffID",
                table: "Grades",
                column: "StaffID",
                principalTable: "Staffs",
                principalColumn: "StaffID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Staffs_StaffID",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_StaffID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "StaffID",
                table: "Grades");
        }
    }
}
