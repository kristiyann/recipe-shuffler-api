using recipe_shuffler.DTO;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IRecipesService
    {
        List<Recipe> GetList(Guid userId);

        Task<Recipe> Insert(RecipeInsertModel recipe);

        Task<Recipe> Update(RecipeInsertModel recipe);

        Task<Recipe> Delete(Guid id);

        Recipe GetRandom(Guid userId);
    }
}