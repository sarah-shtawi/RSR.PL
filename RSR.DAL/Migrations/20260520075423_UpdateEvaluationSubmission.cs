using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEvaluationSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubmittedByUserId",
                table: "EvaluationSubmissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationSubmissions_SubmittedByUserId",
                table: "EvaluationSubmissions",
                column: "SubmittedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubmissions_Users_SubmittedByUserId",
                table: "EvaluationSubmissions",
                column: "SubmittedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationSubmissions_Users_SubmittedByUserId",
                table: "EvaluationSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationSubmissions_SubmittedByUserId",
                table: "EvaluationSubmissions");

            migrationBuilder.DropColumn(
                name: "SubmittedByUserId",
                table: "EvaluationSubmissions");
        }
    }
}
