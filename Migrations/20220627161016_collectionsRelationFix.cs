using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace recipe_shuffler.Migrations
{
    public partial class collectionsRelationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Collections_CollectionId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_CollectionId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "CollectionRecipe",
                columns: table => new
                {
                    CollectionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionRecipe", x => new { x.CollectionsId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_CollectionRecipe_Collections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollectionRecipe_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionRecipe_RecipesId",
                table: "CollectionRecipe",
                column: "RecipesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionRecipe");

            migrationBuilder.AddColumn<Guid>(
                name: "CollectionId",
                table: "Recipes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CollectionId",
                table: "Recipes",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Collections_CollectionId",
                table: "Recipes",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id");
        }
    }
}
