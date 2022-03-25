
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore; // Include
using recipe_shuffler.Data;
using recipe_shuffler.DTO;
using recipe_shuffler.DTO.Recipes;
using recipe_shuffler.DTO.Tags;
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
        public IQueryable<RecipeList> GetList(Guid userId)
        {
            IQueryable<RecipeList> list = _context.Recipes
                .Where(x => x.UserId == userId)
                .Select(x => new RecipeList()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image,
                    Ingredients = x.Ingredients,
                    Instructions = x.Instructions,
                    HasPork = x.HasPork,
                    HasPoultry = x.HasPoultry,
                    Tags = x.Tags
                    .Select(x => new TagList()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Color = x.Color
                    })
                });

            return list;
        }

        public async Task<Recipe> Insert(RecipeInsert model)
        {
            Recipe recipe = ConvertToModel(model);

            await _context.Recipes.AddAsync(recipe);
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
                .Include(x => x.Tags)
                .Skip(offset)
                .FirstOrDefault();

            return recipe;
        }

        public async Task<Recipe> InsertTag(TagInsertIntoRecipe model)
        {
            Recipe recipe = _context.Recipes
                .Where(x => x.Id == model.RecipeId)
                .Include(x => x.Tags)
                .FirstOrDefault();

            Tag tag = await _context.Tags.FindAsync(model.TagId);

            recipe.Tags.Add(tag);

            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> RemoveTag(TagInsertIntoRecipe model)
        {
            Recipe recipe = _context.Recipes
                .Where(x => x.Id == model.RecipeId)
                .Include(x => x.Tags)
                .FirstOrDefault();

            Tag tag = await _context.Tags.FindAsync(model.TagId);

            recipe.Tags.Remove(tag);

            await _context.SaveChangesAsync();

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

        //public RecipeList ConvertFromModelToRecipeList(Recipe recipe)
        //{
        //    RecipeList list = new();

        //    list.Id = recipe.Id;
        //    list.Title = recipe.Title;
        //    list.Instructions = recipe.Instructions;
        //    list.Image = recipe.Image;
        //    list.Ingredients = recipe.Ingredients;
        //    list.HasPork = recipe.HasPork;
        //    list.HasPoultry = recipe.HasPoultry;
        
        //    return list;
        //}
    }
}
