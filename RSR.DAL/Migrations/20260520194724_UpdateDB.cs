using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_EvaluationSubmissionAnswers_EvaluationFields_EvaluationFieldId",
            //    table: "EvaluationSubmissionAnswers");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_EvaluationSubmissionAnswers_EvaluationSubmissions_EvaluationSubmissionId",
            //    table: "EvaluationSubmissionAnswers");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_EvaluationSubmissions_EvaluationForms_EvaluationFormId",
            //    table: "EvaluationSubmissions");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_TaskSubmissionComments_Users_UserId",
            //    table: "TaskSubmissionComments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_TaskSubmissions_Students_StudentId",
            //    table: "TaskSubmissions");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_TaskSubmissions_Tasks_TaskId",
            //    table: "TaskSubmissions");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_EvaluationSubmissionAnswers",
            //    table: "EvaluationSubmissionAnswers");

            //migrationBuilder.RenameTable(
            //    name: "EvaluationSubmissionAnswers",
            //    newName: "EvaluationSubmissionAnswer");

            //migrationBuilder.RenameIndex(
            //    name: "IX_EvaluationSubmissionAnswers_EvaluationSubmissionId",
            //    table: "EvaluationSubmissionAnswer",
            //    newName: "IX_EvaluationSubmissionAnswer_EvaluationSubmissionId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_EvaluationSubmissionAnswers_EvaluationFieldId",
            //    table: "EvaluationSubmissionAnswer",
            //    newName: "IX_EvaluationSubmissionAnswer_EvaluationFieldId");

            //migrationBuilder.AddColumn<string>(
            //    name: "Role",
            //    table: "TaskSubmissionComments",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "ThesisId",
            //    table: "Groups",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_EvaluationSubmissionAnswer",
            //    table: "EvaluationSubmissionAnswer",
            //    column: "Id");

            //migrationBuilder.CreateTable(
            //    name: "Thesis",
            //    columns: table => new
            //    {
            //        ThesisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        ThesisFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DeadLine = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Thesis", x => x.ThesisId);
            //        table.ForeignKey(
            //            name: "FK_Thesis_Groups_GroupId",
            //            column: x => x.GroupId,
            //            principalTable: "Groups",
            //            principalColumn: "GroupId",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ThesisVersions",
            //    columns: table => new
            //    {
            //        VersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        FileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        VersionNumber = table.Column<int>(type: "int", nullable: false),
            //        IsLatest = table.Column<bool>(type: "bit", nullable: false),
            //        IsFrozen = table.Column<bool>(type: "bit", nullable: false),
            //        VisibleByExaminer = table.Column<bool>(type: "bit", nullable: false),
            //        UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        IsPublished = table.Column<bool>(type: "bit", nullable: false),
            //        PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        studentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ThesisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ThesisVersions", x => x.VersionId);
            //        table.ForeignKey(
            //            name: "FK_ThesisVersions_Students_studentId",
            //            column: x => x.studentId,
            //            principalTable: "Students",
            //            principalColumn: "UserId");
            //        table.ForeignKey(
            //            name: "FK_ThesisVersions_Thesis_ThesisId",
            //            column: x => x.ThesisId,
            //            principalTable: "Thesis",
            //            principalColumn: "ThesisId");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ThesisFeedbacks",
            //    columns: table => new
            //    {
            //        FeedbackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Decision = table.Column<int>(type: "int", nullable: false),
            //        Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        VersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        ReviwerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ThesisFeedbacks", x => x.FeedbackId);
            //        table.ForeignKey(
            //            name: "FK_ThesisFeedbacks_ThesisVersions_VersionId",
            //            column: x => x.VersionId,
            //            principalTable: "ThesisVersions",
            //            principalColumn: "VersionId");
            //        table.ForeignKey(
            //            name: "FK_ThesisFeedbacks_Users_ReviwerId",
            //            column: x => x.ReviwerId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Thesis_GroupId",
            //    table: "Thesis",
            //    column: "GroupId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_ThesisFeedbacks_ReviwerId",
            //    table: "ThesisFeedbacks",
            //    column: "ReviwerId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ThesisFeedbacks_VersionId",
            //    table: "ThesisFeedbacks",
            //    column: "VersionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ThesisVersions_studentId",
            //    table: "ThesisVersions",
            //    column: "studentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ThesisVersions_ThesisId",
            //    table: "ThesisVersions",
            //    column: "ThesisId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EvaluationSubmissionAnswer_EvaluationFields_EvaluationFieldId",
            //    table: "EvaluationSubmissionAnswer",
            //    column: "EvaluationFieldId",
            //    principalTable: "EvaluationFields",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EvaluationSubmissionAnswer_EvaluationSubmissions_EvaluationSubmissionId",
            //    table: "EvaluationSubmissionAnswer",
            //    column: "EvaluationSubmissionId",
            //    principalTable: "EvaluationSubmissions",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EvaluationSubmissions_EvaluationForms_EvaluationFormId",
            //    table: "EvaluationSubmissions",
            //    column: "EvaluationFormId",
            //    principalTable: "EvaluationForms",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TaskSubmissionComments_Users_UserId",
            //    table: "TaskSubmissionComments",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TaskSubmissions_Students_StudentId",
            //    table: "TaskSubmissions",
            //    column: "StudentId",
            //    principalTable: "Students",
            //    principalColumn: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TaskSubmissions_Tasks_TaskId",
            //    table: "TaskSubmissions",
            //    column: "TaskId",
            //    principalTable: "Tasks",
            //    principalColumn: "TaskId",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_EvaluationSubmissionAnswer_EvaluationFields_EvaluationFieldId",
            //    table: "EvaluationSubmissionAnswer");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_EvaluationSubmissionAnswer_EvaluationSubmissions_EvaluationSubmissionId",
            //    table: "EvaluationSubmissionAnswer");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_EvaluationSubmissions_EvaluationForms_EvaluationFormId",
            //    table: "EvaluationSubmissions");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_TaskSubmissionComments_Users_UserId",
            //    table: "TaskSubmissionComments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_TaskSubmissions_Students_StudentId",
            //    table: "TaskSubmissions");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_TaskSubmissions_Tasks_TaskId",
            //    table: "TaskSubmissions");

            //migrationBuilder.DropTable(
            //    name: "ThesisFeedbacks");

            //migrationBuilder.DropTable(
            //    name: "ThesisVersions");

            //migrationBuilder.DropTable(
            //    name: "Thesis");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_EvaluationSubmissionAnswer",
            //    table: "EvaluationSubmissionAnswer");

            //migrationBuilder.DropColumn(
            //    name: "Role",
            //    table: "TaskSubmissionComments");

            //migrationBuilder.DropColumn(
            //    name: "ThesisId",
            //    table: "Groups");

            //migrationBuilder.RenameTable(
            //    name: "EvaluationSubmissionAnswer",
            //    newName: "EvaluationSubmissionAnswers");

            //migrationBuilder.RenameIndex(
            //    name: "IX_EvaluationSubmissionAnswer_EvaluationSubmissionId",
            //    table: "EvaluationSubmissionAnswers",
            //    newName: "IX_EvaluationSubmissionAnswers_EvaluationSubmissionId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_EvaluationSubmissionAnswer_EvaluationFieldId",
            //    table: "EvaluationSubmissionAnswers",
            //    newName: "IX_EvaluationSubmissionAnswers_EvaluationFieldId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_EvaluationSubmissionAnswers",
            //    table: "EvaluationSubmissionAnswers",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EvaluationSubmissionAnswers_EvaluationFields_EvaluationFieldId",
            //    table: "EvaluationSubmissionAnswers",
            //    column: "EvaluationFieldId",
            //    principalTable: "EvaluationFields",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EvaluationSubmissionAnswers_EvaluationSubmissions_EvaluationSubmissionId",
            //    table: "EvaluationSubmissionAnswers",
            //    column: "EvaluationSubmissionId",
            //    principalTable: "EvaluationSubmissions",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EvaluationSubmissions_EvaluationForms_EvaluationFormId",
            //    table: "EvaluationSubmissions",
            //    column: "EvaluationFormId",
            //    principalTable: "EvaluationForms",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TaskSubmissionComments_Users_UserId",
            //    table: "TaskSubmissionComments",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TaskSubmissions_Students_StudentId",
            //    table: "TaskSubmissions",
            //    column: "StudentId",
            //    principalTable: "Students",
            //    principalColumn: "UserId",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TaskSubmissions_Tasks_TaskId",
            //    table: "TaskSubmissions",
            //    column: "TaskId",
            //    principalTable: "Tasks",
            //    principalColumn: "TaskId",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
