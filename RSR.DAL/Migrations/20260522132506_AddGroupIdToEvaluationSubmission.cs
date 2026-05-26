using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupIdToEvaluationSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "EvaluationSubmissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationSubmissions_GroupId",
                table: "EvaluationSubmissions",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationSubmissions_Groups_GroupId",
                table: "EvaluationSubmissions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationSubmissions_Groups_GroupId",
                table: "EvaluationSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationSubmissions_GroupId",
                table: "EvaluationSubmissions");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "EvaluationSubmissions");
        }
    }
}
