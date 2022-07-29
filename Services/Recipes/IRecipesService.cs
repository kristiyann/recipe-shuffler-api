using recipe_shuffler.DTO;
using recipe_shuffler.DTO.Recipes;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IRecipesService
    {
        IQueryable<RecipeList> GetList();

        Task<Recipe> Insert(RecipeInsert recipe);

        Task<Recipe> Update(RecipeEdit recipe);

        Task<bool> Delete(Guid id);

        Recipe GetRandom();

        List<RecipeList> Shuffle(RecipeCustomFilter customFilter);
    }
}