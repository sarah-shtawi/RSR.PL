using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditImageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureProfileURL",
                table: "Supervisors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureProfileURL",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureProfileURL",
                table: "Examiners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureProfileURL",
                table: "Coordinators",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureProfileURL",
                table: "Supervisors");

            migrationBuilder.DropColumn(
                name: "PictureProfileURL",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PictureProfileURL",
                table: "Examiners");

            migrationBuilder.DropColumn(
                name: "PictureProfileURL",
                table: "Coordinators");
        }
    }
}
