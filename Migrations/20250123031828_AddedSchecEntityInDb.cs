using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class AddedSchecEntityInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleOfSubjects_Professors_ProfessorID",
                table: "ScheduleOfSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleOfSubjects_Subjects_SubjectID",
                table: "ScheduleOfSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Subjects_SubjectsEntitySubjectID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SubjectsEntitySubjectID",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleOfSubjects",
                table: "ScheduleOfSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectsEntitySubjectID",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "ScheduleOfSubjects",
                newName: "Schedule");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleOfSubjects_SubjectID",
                table: "Schedule",
                newName: "IX_Schedule_SubjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleOfSubjects_ProfessorID",
                table: "Schedule",
                newName: "IX_Schedule_ProfessorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule",
                column: "ScheduleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Professors_ProfessorID",
                table: "Schedule",
                column: "ProfessorID",
                principalTable: "Professors",
                principalColumn: "ProfessorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Subjects_SubjectID",
                table: "Schedule",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Professors_ProfessorID",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Subjects_SubjectID",
                table: "Schedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule");

            migrationBuilder.RenameTable(
                name: "Schedule",
                newName: "ScheduleOfSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_SubjectID",
                table: "ScheduleOfSubjects",
                newName: "IX_ScheduleOfSubjects_SubjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_ProfessorID",
                table: "ScheduleOfSubjects",
                newName: "IX_ScheduleOfSubjects_ProfessorID");

            migrationBuilder.AddColumn<string>(
                name: "SubjectsEntitySubjectID",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleOfSubjects",
                table: "ScheduleOfSubjects",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SubjectsEntitySubjectID",
                table: "Students",
                column: "SubjectsEntitySubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleOfSubjects_Professors_ProfessorID",
                table: "ScheduleOfSubjects",
                column: "ProfessorID",
                principalTable: "Professors",
                principalColumn: "ProfessorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleOfSubjects_Subjects_SubjectID",
                table: "ScheduleOfSubjects",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Subjects_SubjectsEntitySubjectID",
                table: "Students",
                column: "SubjectsEntitySubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID");
        }
    }
}
