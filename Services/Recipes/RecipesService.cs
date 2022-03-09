
using Microsoft.AspNetCore.OData.Query;
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
        public IQueryable GetList(ODataQueryOptions<Recipe> queryOptions, Guid userId)
        {
            IQueryable list = _context.Recipes
                .Where(x => x.UserId == userId);

            list = queryOptions.ApplyTo(list);

            return list;
        }

        public async Task<Recipe> Insert(RecipeInsert model)
        {
            Recipe recipe = ConvertToModel(model);
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> Update(RecipeInsert model)
        {
            Recipe recipe = ConvertToModel(model);
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> Delete(Guid id)
        {
            Recipe? recipe = _context.Recipes.FirstOrDefault(x => x.Id == id);

            _context.Remove(recipe);

            await _context.SaveChangesAsync();

            return recipe;
        }

        public Recipe GetRandom(Guid userId)
        {
            List<Recipe> list = _context.Recipes
                .Where(x => x.UserId == userId).ToList();

            int totalRecipes = list.Count;

            Random random = new();
            int offset = random.Next(0, totalRecipes);

            Recipe? recipe = _context.Recipes
                .Where(x => x.UserId == userId)
                .Skip(offset)
                .FirstOrDefault();

            return recipe;
        }

        public Recipe ConvertToModel(RecipeInsert model)
        {
            Recipe recipe = new();

            recipe.Id = model.Id;
            recipe.Title = model.Title;
            recipe.Image = model.Image;
            recipe.HasPork = model.HasPork;
            recipe.HasPoultry = model.HasPoultry;
            recipe.Instructions = model.Instructions;
            recipe.Ingredients = model.Ingredients;
            recipe.User = _context.Users.Find(model.UserId);

            return recipe;
        }
    }
}
