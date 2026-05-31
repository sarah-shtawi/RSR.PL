using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<float>(
            //    name: "TotalScore",
            //    table: "EvaluationSubmissions",
            //    type: "real",
            //    nullable: false,
            //    oldClrType: typeof(double),
            //    oldType: "float");

            migrationBuilder.AddColumn<float>(
              name: "TotalScore",
              table: "EvaluationSubmissions",
              type: "real",
              nullable: false,
              defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<double>(
            //    name: "TotalScore",
            //    table: "EvaluationSubmissions",
            //    type: "float",
            //    nullable: false,
            //    oldClrType: typeof(float),
            //    oldType: "real");

            migrationBuilder.DropColumn(
            name: "TotalScore",
            table: "EvaluationSubmissions");
        }
    }
}
