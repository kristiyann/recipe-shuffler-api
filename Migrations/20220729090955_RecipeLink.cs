using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace recipe_shuffler.Migrations
{
    public partial class RecipeLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Recipes");
        }
    }
}
