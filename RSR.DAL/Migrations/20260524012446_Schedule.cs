using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "EvaluationForms",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AssignTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EvaluationForms", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoordinatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_Coordinators_CoordinatorId",
                        column: x => x.CoordinatorId,
                        principalTable: "Coordinators",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "EvaluationFields",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        MinValue = table.Column<int>(type: "int", nullable: false),
            //        MaxValue = table.Column<int>(type: "int", nullable: false),
            //        IsRequired = table.Column<bool>(type: "bit", nullable: false),
            //        EvaluationFormId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EvaluationFields", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_EvaluationFields_EvaluationForms_EvaluationFormId",
            //            column: x => x.EvaluationFormId,
            //            principalTable: "EvaluationForms",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EvaluationSubmissions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        EvaluationFormId = table.Column<int>(type: "int", nullable: false),
            //        SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        SubmittedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EvaluationSubmissions", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_EvaluationSubmissions_EvaluationForms_EvaluationFormId",
            //            column: x => x.EvaluationFormId,
            //            principalTable: "EvaluationForms",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_EvaluationSubmissions_Users_SubmittedByUserId",
            //            column: x => x.SubmittedByUserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            migrationBuilder.CreateTable(
                name: "DefenseExaminers",
                columns: table => new
                {
                    DefenseExaminerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExaminerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefenseExaminers", x => x.DefenseExaminerId);
                    table.ForeignKey(
                        name: "FK_DefenseExaminers_Examiners_ExaminerId",
                        column: x => x.ExaminerId,
                        principalTable: "Examiners",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DefenseExaminers_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "EvaluationSubmissionAnswers",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        EvaluationSubmissionId = table.Column<int>(type: "int", nullable: false),
            //        EvaluationFieldId = table.Column<int>(type: "int", nullable: false),
            //        Value = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EvaluationSubmissionAnswers", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_EvaluationSubmissionAnswers_EvaluationFields_EvaluationFieldId",
            //            column: x => x.EvaluationFieldId,
            //            principalTable: "EvaluationFields",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_EvaluationSubmissionAnswers_EvaluationSubmissions_EvaluationSubmissionId",
            //            column: x => x.EvaluationSubmissionId,
            //            principalTable: "EvaluationSubmissions",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_DefenseExaminers_ExaminerId",
                table: "DefenseExaminers",
                column: "ExaminerId");

            migrationBuilder.CreateIndex(
                name: "IX_DefenseExaminers_ScheduleId",
                table: "DefenseExaminers",
                column: "ScheduleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EvaluationFields_EvaluationFormId",
            //    table: "EvaluationFields",
            //    column: "EvaluationFormId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EvaluationSubmissionAnswers_EvaluationFieldId",
            //    table: "EvaluationSubmissionAnswers",
            //    column: "EvaluationFieldId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EvaluationSubmissionAnswers_EvaluationSubmissionId",
            //    table: "EvaluationSubmissionAnswers",
            //    column: "EvaluationSubmissionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EvaluationSubmissions_EvaluationFormId",
            //    table: "EvaluationSubmissions",
            //    column: "EvaluationFormId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EvaluationSubmissions_SubmittedByUserId",
            //    table: "EvaluationSubmissions",
            //    column: "SubmittedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CoordinatorId",
                table: "Schedules",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_GroupId",
                table: "Schedules",
                column: "GroupId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefenseExaminers");

            //migrationBuilder.DropTable(
            //    name: "EvaluationSubmissionAnswers");

            migrationBuilder.DropTable(
                name: "Schedules");

            //migrationBuilder.DropTable(
            //    name: "EvaluationFields");

            //migrationBuilder.DropTable(
            //    name: "EvaluationSubmissions");

            //migrationBuilder.DropTable(
            //    name: "EvaluationForms");
        }
    }
}
