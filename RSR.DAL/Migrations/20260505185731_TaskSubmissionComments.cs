using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TaskSubmissionComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskSubmissionComments",
                columns: table => new
                {
                    TaskSubmissionCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskSubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSubmissionComments", x => x.TaskSubmissionCommentId);
                    table.ForeignKey(
                        name: "FK_TaskSubmissionComments_TaskSubmissionComments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "TaskSubmissionComments",
                        principalColumn: "TaskSubmissionCommentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskSubmissionComments_TaskSubmissions_TaskSubmissionId",
                        column: x => x.TaskSubmissionId,
                        principalTable: "TaskSubmissions",
                        principalColumn: "TaskSubmissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskSubmissionComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissionComments_ParentCommentId",
                table: "TaskSubmissionComments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissionComments_TaskSubmissionId",
                table: "TaskSubmissionComments",
                column: "TaskSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissionComments_UserId",
                table: "TaskSubmissionComments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskSubmissionComments");
        }
    }
}
