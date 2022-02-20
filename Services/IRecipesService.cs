using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IRecipesService
    {
        Task<List<Recipes>> GetListByUser();

        Task<Recipes> GetAsync();

        Task<Recipes> AddAsync();

        Task<Recipes> UpdateAsync();

        Task<Recipes> Delete();
    }
}