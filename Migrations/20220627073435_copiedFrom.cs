using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace recipe_shuffler.Migrations
{
    public partial class copiedFrom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CopiedFromId",
                table: "Recipes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CopiedFromId",
                table: "Recipes",
                column: "CopiedFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Recipes_CopiedFromId",
                table: "Recipes",
                column: "CopiedFromId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Recipes_CopiedFromId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CopiedFromId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CopiedFromId",
                table: "Recipes");
        }
    }
}
