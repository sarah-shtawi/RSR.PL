using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SemeterIdWithEvaluationForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "GroupId",
            //    table: "EvaluationForms");

            migrationBuilder.AddColumn<Guid>(
                name: "SemesterId",
                table: "EvaluationForms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationForms_SemesterId",
                table: "EvaluationForms",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationForms_Semesters_SemesterId",
                table: "EvaluationForms",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "SemesterId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationForms_Semesters_SemesterId",
                table: "EvaluationForms");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationForms_SemesterId",
                table: "EvaluationForms");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "EvaluationForms");

            //migrationBuilder.AddColumn<int>(
            //    name: "GroupId",
            //    table: "EvaluationForms",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);
        }
    }
}
