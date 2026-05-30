using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeleteGroupId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "EvaluationForms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "EvaluationForms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
