using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddEvaluationSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluationFormId = table.Column<int>(type: "int", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationSubmissions_EvaluationForms_EvaluationFormId",
                        column: x => x.EvaluationFormId,
                        principalTable: "EvaluationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationSubmissionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluationSubmissionId = table.Column<int>(type: "int", nullable: false),
                    EvaluationFieldId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSubmissionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationSubmissionAnswers_EvaluationFields_EvaluationFieldId",
                        column: x => x.EvaluationFieldId,
                        principalTable: "EvaluationFields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvaluationSubmissionAnswers_EvaluationSubmissions_EvaluationSubmissionId",
                        column: x => x.EvaluationSubmissionId,
                        principalTable: "EvaluationSubmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationSubmissionAnswers_EvaluationFieldId",
                table: "EvaluationSubmissionAnswers",
                column: "EvaluationFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationSubmissionAnswers_EvaluationSubmissionId",
                table: "EvaluationSubmissionAnswers",
                column: "EvaluationSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationSubmissions_EvaluationFormId",
                table: "EvaluationSubmissions",
                column: "EvaluationFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationSubmissionAnswers");

            migrationBuilder.DropTable(
                name: "EvaluationSubmissions");
        }
    }
}
