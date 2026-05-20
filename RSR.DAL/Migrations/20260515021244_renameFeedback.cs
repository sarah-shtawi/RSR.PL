using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class renameFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "decision",
                table: "ThesisFeedbacks",
                newName: "Decision");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "ThesisFeedbacks",
                newName: "Feedback");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Decision",
                table: "ThesisFeedbacks",
                newName: "decision");

            migrationBuilder.RenameColumn(
                name: "Feedback",
                table: "ThesisFeedbacks",
                newName: "Content");
        }
    }
}
