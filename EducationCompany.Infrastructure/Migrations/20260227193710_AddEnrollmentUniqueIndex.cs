using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationCompany.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrollmentUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId_ParticipantId",
                table: "Enrollments",
                columns: new[] { "CourseId", "ParticipantId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrollments_CourseId_ParticipantId",
                table: "Enrollments");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");
        }
    }
}
