using recipe_shuffler.DTO;
using recipe_shuffler.DTO.Recipes;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IRecipesService
    {
        IQueryable<RecipeList> GetList(Guid userId);

        Task<Recipe> Insert(RecipeInsert recipe);

        Task<Recipe> Update(RecipeEdit recipe);

        Task<Recipe> Delete(Guid id);

        Recipe GetRandom(Guid userId);

        Task<Recipe> InsertTag(TagInsertIntoRecipe model);

        Task<Recipe> RemoveTag(TagInsertIntoRecipe model);
    }
}