using recipe_shuffler.DTO;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IRecipesService
    {
        List<Recipes> GetList(Guid userId);

        Task<Recipes> Insert(RecipeInsertModel recipe);

        Task<Recipes> Update(RecipeInsertModel recipe);

        Task<Recipes> Delete(Guid id);

        Recipes GetRandom(Guid userId);
    }
}