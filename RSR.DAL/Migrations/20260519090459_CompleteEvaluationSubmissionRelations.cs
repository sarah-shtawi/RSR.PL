using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CompleteEvaluationSubmissionRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationSubmissions_EvaluationForms_EvaluationFormId",
                table: "EvaluationSubmissions");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubmissions_EvaluationForms_EvaluationFormId",
                table: "EvaluationSubmissions",
                column: "EvaluationFormId",
                principalTable: "EvaluationForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationSubmissions_EvaluationForms_EvaluationFormId",
                table: "EvaluationSubmissions");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubmissions_EvaluationForms_EvaluationFormId",
                table: "EvaluationSubmissions",
                column: "EvaluationFormId",
                principalTable: "EvaluationForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
