using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class ModifedSchedProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleOfSubjects",
                table: "ScheduleOfSubjects");

            migrationBuilder.RenameColumn(
                name: "SchedID",
                table: "ScheduleOfSubjects",
                newName: "ProfessorID");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleID",
                table: "ScheduleOfSubjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "ScheduleOfSubjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "ScheduleOfSubjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleOfSubjects",
                table: "ScheduleOfSubjects",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleOfSubjects_ProfessorID",
                table: "ScheduleOfSubjects",
                column: "ProfessorID");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleOfSubjects_Professors_ProfessorID",
                table: "ScheduleOfSubjects",
                column: "ProfessorID",
                principalTable: "Professors",
                principalColumn: "ProfessorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleOfSubjects_Professors_ProfessorID",
                table: "ScheduleOfSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleOfSubjects",
                table: "ScheduleOfSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleOfSubjects_ProfessorID",
                table: "ScheduleOfSubjects");

            migrationBuilder.DropColumn(
                name: "ScheduleID",
                table: "ScheduleOfSubjects");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "ScheduleOfSubjects");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "ScheduleOfSubjects");

            migrationBuilder.RenameColumn(
                name: "ProfessorID",
                table: "ScheduleOfSubjects",
                newName: "SchedID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleOfSubjects",
                table: "ScheduleOfSubjects",
                column: "SchedID");
        }
    }
}
