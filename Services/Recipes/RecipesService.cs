
using recipe_shuffler.Data;
using recipe_shuffler.DTO;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly DataContext _context;

        public RecipesService(DataContext context)
        {
            _context = context;
        }
        public List<Recipes> GetList(Guid userId)
        {
            List<Recipes> list = _context.Recipes
                .Where(x => x.UserId == userId).ToList();
          
            return list;
        }

        public async Task<Recipes> Insert(RecipeInsertModel model)
        {
            Recipes recipe = convertToModel(model);
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        public Task<Recipes> Update(Recipes recipe)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Recipes convertToModel(RecipeInsertModel model)
        {
            Recipes recipe = new Recipes();
            recipe.Id = model.Id;
            recipe.Title = model.Title;
            recipe.HasPork = model.HasPork;
            recipe.HasPoultry = model.HasPoultry;
            recipe.Instructions = model.Instructions;
            recipe.Ingredients =  model.Ingredients;

            return recipe;
        }
    }
}
