using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedStudentTableAndAddedScholarshipModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScholarshipID",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Programs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Acronym",
                table: "Programs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Scholarship",
                columns: table => new
                {
                    ScholarshipID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScholarshipName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scholarship", x => x.ScholarshipID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ScholarshipID",
                table: "Students",
                column: "ScholarshipID");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_Name_Acronym",
                table: "Programs",
                columns: new[] { "Name", "Acronym" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [Acronym] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Scholarship_ScholarshipID",
                table: "Students",
                column: "ScholarshipID",
                principalTable: "Scholarship",
                principalColumn: "ScholarshipID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Scholarship_ScholarshipID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Scholarship");

            migrationBuilder.DropIndex(
                name: "IX_Students_ScholarshipID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Programs_Name_Acronym",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ScholarshipID",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Acronym",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
