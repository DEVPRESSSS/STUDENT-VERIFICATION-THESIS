using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class AddedStudentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Professors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "ProgramEntity",
                columns: table => new
                {
                    ProgramID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramEntity", x => x.ProgramID);
                });

            migrationBuilder.CreateTable(
                name: "Year",
                columns: table => new
                {
                    YearID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Year", x => x.YearID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    IDnumber = table.Column<long>(type: "bigint", nullable: false),
                    Contact = table.Column<long>(type: "bigint", nullable: false),
                    Gmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgramID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YearID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Students_ProgramEntity_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "ProgramEntity",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Year_YearID",
                        column: x => x.YearID,
                        principalTable: "Year",
                        principalColumn: "YearID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgramID",
                table: "Students",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_YearID",
                table: "Students",
                column: "YearID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ProgramEntity");

            migrationBuilder.DropTable(
                name: "Year");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "Professors",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "Departments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
