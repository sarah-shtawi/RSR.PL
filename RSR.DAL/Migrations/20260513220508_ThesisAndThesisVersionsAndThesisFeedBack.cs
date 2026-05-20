using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ThesisAndThesisVersionsAndThesisFeedBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmissionComments_Users_UserId",
                table: "TaskSubmissionComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmissions_Students_StudentId",
                table: "TaskSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmissions_Tasks_TaskId",
                table: "TaskSubmissions");

            migrationBuilder.AddColumn<Guid>(
                name: "ThesisId",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Thesis",
                columns: table => new
                {
                    ThesisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThesisFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thesis", x => x.ThesisId);
                    table.ForeignKey(
                        name: "FK_Thesis_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThesisVersions",
                columns: table => new
                {
                    VersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    IsFrozen = table.Column<bool>(type: "bit", nullable: false),
                    VisibleByExaminer = table.Column<bool>(type: "bit", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    studentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThesisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThesisVersions", x => x.VersionId);
                    table.ForeignKey(
                        name: "FK_ThesisVersions_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ThesisVersions_Thesis_ThesisId",
                        column: x => x.ThesisId,
                        principalTable: "Thesis",
                        principalColumn: "ThesisId");
                });

            migrationBuilder.CreateTable(
                name: "ThesisFeedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    decision = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviwerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThesisFeedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_ThesisFeedbacks_ThesisVersions_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "ThesisVersions",
                        principalColumn: "VersionId");
                    table.ForeignKey(
                        name: "FK_ThesisFeedbacks_Users_ReviwerId",
                        column: x => x.ReviwerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thesis_GroupId",
                table: "Thesis",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThesisFeedbacks_ReviwerId",
                table: "ThesisFeedbacks",
                column: "ReviwerId");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisVersions_studentId",
                table: "ThesisVersions",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisVersions_ThesisId",
                table: "ThesisVersions",
                column: "ThesisId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmissionComments_Users_UserId",
                table: "TaskSubmissionComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmissions_Students_StudentId",
                table: "TaskSubmissions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmissions_Tasks_TaskId",
                table: "TaskSubmissions",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmissionComments_Users_UserId",
                table: "TaskSubmissionComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmissions_Students_StudentId",
                table: "TaskSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmissions_Tasks_TaskId",
                table: "TaskSubmissions");

            migrationBuilder.DropTable(
                name: "ThesisFeedbacks");

            migrationBuilder.DropTable(
                name: "ThesisVersions");

            migrationBuilder.DropTable(
                name: "Thesis");

            migrationBuilder.DropColumn(
                name: "ThesisId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmissionComments_Users_UserId",
                table: "TaskSubmissionComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmissions_Students_StudentId",
                table: "TaskSubmissions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmissions_Tasks_TaskId",
                table: "TaskSubmissions",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
