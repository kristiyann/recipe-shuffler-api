using Microsoft.EntityFrameworkCore; // Include
using recipe_shuffler.Data;
using recipe_shuffler.DTO;
using recipe_shuffler.DTO.Recipes;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Helpers;
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
        public IQueryable<RecipeList> GetList(bool shuffle)
        {
            IQueryable<Recipe> query = _context.Recipes
                .Where(x => x.UserId == _usersService.GetMyId())
                .Include(x => x.Tags)
                .Include(x => x.User);

            if (shuffle)
            {
                List<Recipe> tempList = query.ToList();
                tempList.ShuffleCollection();
                query = tempList.AsQueryable();
            }

            IQueryable<RecipeList> list = query
                .Select(x => new RecipeList()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image,
                    Ingredients = x.Ingredients,
                    Instructions = x.Instructions,
                    Calories = x.Calories,
                    Protein = x.Protein,
                    Link = x.Link ?? string.Empty,
                    Tags = x.Tags
                    .Select(y => new TagList()
                    {
                        Id = y.Id,
                        Name = y.Name,
                        Color = y.Color,
                        IsCategory = y.UserId == null ? true : false
                    })
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
                    .Where(x => x.UserId == _usersService.GetMyId() || x.UserId == null)
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
                .Where(x => x.UserId == _usersService.GetMyId())
                .Single(x => x.Id == model.Id);

                List<Tag> tags = await _context.Tags
                    .Where(x => model.TagIds.Any(f => f == x.Id))
                    .Where(x => x.UserId == _usersService.GetMyId() || x.UserId == null)
                    .ToListAsync();

                recipe = CustomUpdate(recipe, model, tags);
            }
            else
            {
                recipe = this.ConvertEditModelToDbObj(model);
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

        //public List<RecipeList> Shuffle(RecipeCustomFilter customFilter)
        //{
        //    IQueryable<Recipe> query = _context.Recipes
        //        .Where(x => x.UserId == _usersService.GetMyId());

        //    query = this.ApplyCustomFilter(query, customFilter);

        //    List<RecipeList> list = query
        //        .Select(x => new RecipeList()
        //        {
        //            Id = x.Id,
        //            Title = x.Title,
        //            Image = x.Image,
        //            Ingredients = x.Ingredients,
        //            Instructions = x.Instructions,
        //            Calories = x.Calories,
        //            Protein = x.Protein,
        //            Link = x.Link ?? string.Empty,
        //            Tags = x.Tags
        //            .Select(y => new TagList()
        //            {
        //                Id = y.Id,
        //                Name = y.Name,
        //                Color = y.Color
        //            })
        //        }).ToList();

        //    list.ShuffleCollection();

        //    return list;
        //}

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
            if (recipe.Link != model.Link)
            {
                recipe.Link = model.Link;
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
                UserId = _usersService.GetMyId(),
                Tags = new HashSet<Tag>(),
                Protein = model.Protein,
                Calories = model.Calories,
                Link = model.Link
            };

            return recipe;
        }

        private Recipe ConvertEditModelToDbObj(RecipeEdit model)
        {
            Recipe recipe = new()
            {
                Id = model.Id,
                Title = model.Title,
                Image = model.Image,
                Instructions = model.Instructions,
                Ingredients = model.Ingredients,
                UserId = _usersService.GetMyId(),
                Protein = model.Protein,
                Calories = model.Calories,
                Link = model.Link
            };

            return recipe;
        }

        #endregion

        #region customFilter

        private IQueryable<Recipe> ApplyCustomFilter(IQueryable<Recipe> query, RecipeCustomFilter customFilter)
        {
            if (customFilter != null)
            {
                if (customFilter.Ingredients != null)
                {
                    query = query.Where(x => x.Ingredients.ToLower().Contains(customFilter.Ingredients.ToLower()));
                }

                if (customFilter.TagIds != null)
                {
                    query = query.Where(x => customFilter.TagIds.Any(q => x.Tags.Any(z => z.Id == q)));
                }
            }

            return query;
        }

        #endregion
    }
}
