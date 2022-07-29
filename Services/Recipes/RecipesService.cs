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
        private readonly IUsersService _usersService;

        public RecipesService(DataContext context, IUsersService usersService)
        {
            _context = context;
            _usersService = usersService;
        }
        public IQueryable<RecipeList> GetList()
        {
            IQueryable<RecipeList> list = _context.Recipes
                .Where(x => x.UserId == _usersService.GetMyId())
                .Select(x => new RecipeList()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image,
                    Ingredients = x.Ingredients,
                    Instructions = x.Instructions,
                    Tags = x.Tags
                    .Select(y => new TagList()
                    {
                        Id = y.Id,
                        Name = y.Name,
                        Color = y.Color
                    }),
                    Calories = x.Calories,
                    Protein = x.Protein,
                    Category = x.Category
                });

            return list;
        }

        public async Task<Recipe> Insert(RecipeInsert model)
        {
            Recipe recipe = ConvertInsertModelToDbObj(model);

            if (model != null && model.TagIds != null)
            {
                List<Tag> tags = await _context.Tags
                    .Where(x => model.TagIds.Any(f => f == x.Id))
                    .ToListAsync();

                foreach (Tag tagObject in tags)
                {
                    recipe.Tags.Add(tagObject);
                }
            }

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> Update(RecipeEdit model)
        {
            Recipe recipe = new();

            if (model != null && model.TagIds != null)
            {
                recipe = _context.Recipes
                .Include(x => x.Tags)
                .Include(x => x.User)
                .Single(x => x.Id == model.Id);

                List<Tag> tags = await _context.Tags
                    .Where(x => model.TagIds.Any(f => f == x.Id))
                    .ToListAsync();

                recipe = CustomUpdate(recipe, model, tags);
            }
            else
            {
                recipe = await ConvertEditModelToDbObj(model);
            }

            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task<bool> Delete(Guid id)
        {
            bool result = false;

            Recipe recipe = _context.Recipes
                .Where(x => x.UserId == _usersService.GetMyId())
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (recipe != null)
            {
                _context.Remove(recipe);
                await _context.SaveChangesAsync();

                result = true;
            }

            return result;
        }

        public Recipe GetRandom()
        {
            List<Recipe> list = _context.Recipes
                .Where(x => x.UserId == _usersService.GetMyId()).ToList();

            int totalRecipes = list.Count;

            Random random = new();
            int offset = random.Next(0, totalRecipes);

            Recipe? recipe = _context.Recipes
                .Where(x => x.UserId == _usersService.GetMyId())
                .Include(x => x.Tags)
                .Skip(offset)
                .FirstOrDefault();

            return recipe;
        }

        private static Recipe CustomUpdate(Recipe recipe, RecipeEdit model, List<Tag> tags)
        {
            foreach (Tag tag in recipe.Tags.ToList())
            {
                if (!tags.Contains(tag))
                {
                    recipe.Tags.Remove(tag);
                }
            }

            foreach (Tag tag in tags)
            {
                if (!recipe.Tags.Any(t => t.Id == tag.Id))
                {
                    recipe.Tags.Add(tag);
                }
            }

            if (recipe.Title != model.Title)
            {
                recipe.Title = model.Title;
            }
            if (recipe.Instructions != model.Instructions)
            {
                recipe.Instructions = model.Instructions;
            }
            if (recipe.Image != model.Image)
            {
                recipe.Image = model.Image;
            }
            if (recipe.Ingredients != model.Ingredients)
            {
                recipe.Ingredients = model.Ingredients;
            }
            if (recipe.Calories != model.Calories)
            {
                recipe.Calories = model.Calories;
            }
            if (recipe.Protein != model.Protein)
            {
                recipe.Protein = model.Protein;
            }
            if (recipe.Category != model.Category)
            {
                recipe.Category = model.Category;
            }

            return recipe;
        }

        #region Converters
        private Recipe ConvertInsertModelToDbObj(RecipeInsert model)
        {
            Recipe recipe = new()
            {
                Title = model.Title,
                Image = model.Image,
                Instructions = model.Instructions,
                Ingredients = model.Ingredients,
                User = _context.Users.Find(_usersService.GetMyId()),
                Tags = new HashSet<Tag>(),
                Protein = model.Protein,
                Calories = model.Calories,
                Category = model.Category
            };

            return recipe;
        }

        private async Task<Recipe> ConvertEditModelToDbObj(RecipeEdit model)
        {
            Recipe recipe = new()
            {
                Id = model.Id,
                Title = model.Title,
                Image = model.Image,
                Instructions = model.Instructions,
                Ingredients = model.Ingredients,
                // UserId = model.UserId,
                User = await _context.Users.FindAsync(_usersService.GetMyId()),
                Protein = model.Protein,
                Calories = model.Calories,
                Category = model.Category
            };

            //if (model.TagIds != null)
            //{
            //    recipe.Tags = new HashSet<Tag>();
            //}

            return recipe;
        }

        #endregion
    }
}
