
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
            Recipes recipe = ConvertToModel(model);
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipes> Update(RecipeInsertModel model)
        {
            Recipes recipe = ConvertToModel(model);
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipes> Delete(Guid id)
        {
            Recipes? recipe = _context.Recipes.FirstOrDefault(x => x.Id == id);

            _context.Remove(recipe);

            await _context.SaveChangesAsync();

            return recipe;
        }

        public Recipes GetRandom(Guid userId)
        {
            List<Recipes> list = _context.Recipes
                .Where(x => x.UserId == userId).ToList<Recipes>();

            int totalRecipes = list.Count;

            Random random = new();
            int offset = random.Next(0, totalRecipes);

            Recipes? recipe = _context.Recipes
                .Where(x => x.UserId == userId)
                .Skip(offset)
                .FirstOrDefault();

            return recipe;
        }

        public Recipes ConvertToModel(RecipeInsertModel model)
        {
            Recipes recipe = new();

            recipe.Id = model.Id;
            recipe.Title = model.Title;
            recipe.Image = model.Image;
            recipe.HasPork = model.HasPork;
            recipe.HasPoultry = model.HasPoultry;
            recipe.Instructions = model.Instructions;
            recipe.Ingredients =  model.Ingredients;
            recipe.User = _context.Users.Find(model.UserId);

            return recipe;
        }
    }
}
