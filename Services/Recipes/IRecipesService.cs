using Microsoft.AspNetCore.OData.Query;
using recipe_shuffler.DTO;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IRecipesService
    {
        IQueryable GetList(ODataQueryOptions<Recipe> queryOptions, Guid userId);

        Task<Recipe> Insert(RecipeInsert recipe);

        Task<Recipe> Update(RecipeInsert recipe);

        Task<Recipe> Delete(Guid id);

        Recipe GetRandom(Guid userId);

        Task<Recipe> InsertTag(TagInsertIntoRecipe model);
    }
}