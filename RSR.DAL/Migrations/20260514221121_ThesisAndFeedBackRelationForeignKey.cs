using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ThesisAndFeedBackRelationForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThesisFeedbacks_ThesisVersions_FeedbackId",
                table: "ThesisFeedbacks");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisFeedbacks_VersionId",
                table: "ThesisFeedbacks",
                column: "VersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThesisFeedbacks_ThesisVersions_VersionId",
                table: "ThesisFeedbacks",
                column: "VersionId",
                principalTable: "ThesisVersions",
                principalColumn: "VersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThesisFeedbacks_ThesisVersions_VersionId",
                table: "ThesisFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_ThesisFeedbacks_VersionId",
                table: "ThesisFeedbacks");

            migrationBuilder.AddForeignKey(
                name: "FK_ThesisFeedbacks_ThesisVersions_FeedbackId",
                table: "ThesisFeedbacks",
                column: "FeedbackId",
                principalTable: "ThesisVersions",
                principalColumn: "VersionId");
        }
    }
}
