using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFinalEvaluationResultTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinalEvaluationResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupervisorGrade = table.Column<double>(type: "float", nullable: false),
                    ExaminerGrade = table.Column<double>(type: "float", nullable: false),
                    FinalGrade = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalEvaluationResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalEvaluationResults_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinalEvaluationResults_GroupId",
                table: "FinalEvaluationResults",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinalEvaluationResults");
        }
    }
}
