using recipe_shuffler.DataTransferObjects;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IRecipesService
    {
        IQueryable<RecipeList> GetList(RecipeCustomFilter customFilter);

        Task<Recipe> Insert(RecipeInsert recipe);

        Task<Recipe> Update(RecipeEdit recipe);

        Task<bool> Delete(Guid id);

        IEnumerable<RecipeList> GetRandom(RecipeCustomFilter customFilter);

        Task<Guid> Copy(Guid id);

        Task<bool> LikeOrDislike(Guid id);
    }
}