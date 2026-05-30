using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DefenseExaminers_Examiners_ExaminerId",
                table: "DefenseExaminers");


    

            migrationBuilder.AddForeignKey(
                name: "FK_DefenseExaminers_Examiners_ExaminerId",
                table: "DefenseExaminers",
                column: "ExaminerId",
                principalTable: "Examiners",
                principalColumn: "UserId");

        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DefenseExaminers_Examiners_ExaminerId",
                table: "DefenseExaminers");

  

            migrationBuilder.AddForeignKey(
                name: "FK_DefenseExaminers_Examiners_ExaminerId",
                table: "DefenseExaminers",
                column: "ExaminerId",
                principalTable: "Examiners",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
