using Microsoft.EntityFrameworkCore; // Include
using recipe_shuffler.Data;
using recipe_shuffler.DataTransferObjects.Recipes;
using recipe_shuffler.DTO;
using recipe_shuffler.DTO.Recipes;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.DTO.Users;
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
        public IQueryable<RecipeList> GetList(RecipeCustomFilter customFilter)
        {
            IQueryable<Recipe> query = this.GenerateInitialQuery();

            query = this.ApplyCustomFilter(query, customFilter);

            IQueryable<RecipeList> list = query
                .Select(x => new RecipeList()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image,
                    Ingredients = x.Ingredients,
                    Instructions = x.Instructions,
                    IsPublic = x.IsPublic,
                    Tags = x.Tags
                    .Select(y => new TagList()
                    {
                        Id = y.Id,
                        Name = y.Name,
                        Color = y.Color
                    }),
                    User = new GenericComboBoxUser()
                    {
                        Value = x.UserId,
                        Text = x.User.Username
                    }
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
                recipe = this.GenerateInitialQuery(model.Id)
                .Include(x => x.Tags)
                .Include(x => x.User)
                .FirstOrDefault();

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

            Recipe recipe = this.GenerateInitialQuery(id).FirstOrDefault();

            if (recipe != null)
            {
                _context.Remove(recipe);
                await _context.SaveChangesAsync();

                result = true;
            }

            return result;
        }

        public List<Recipe> GetRandom(RecipeCustomFilter customFilter)
        {
            IQueryable<Recipe> query = this.GenerateInitialQuery().Include(x => x.Tags);

            query = ApplyCustomFilter(query, customFilter);

            int totalRecipes = query.Count();

            Random random = new();
            int offset = random.Next(0, totalRecipes);

            Recipe recipe = query.Skip(offset).FirstOrDefault();

            List<Recipe> recipeCollection = new();

            if (recipe != null)
            {
                recipeCollection.Add(recipe);
            }

            return recipeCollection;
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

            return recipe;
        }

        #region InitialQuery

        private IQueryable<Recipe> GenerateInitialQuery(Guid? id = null, Guid? userId = null)
        {
            IQueryable<Recipe> query = _context.Recipes.Where(x => x.UserId == _usersService.GetMyId());

            if (userId != null)
            {
                query = query.Where(x => x.UserId == userId);
            }

            if (id != null)
            {
                query = query.Where(x => x.Id == id.Value);
            }

            return query;
        }

        #endregion

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
                Tags = new HashSet<Tag>()
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
                User = await _context.Users.FindAsync(_usersService.GetMyId())
            };

            //if (model.TagIds != null)
            //{
            //    recipe.Tags = new HashSet<Tag>();
            //}

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
                    query = query.Where(x => x.Ingredients.Contains(customFilter.Ingredients));
                }

                if (customFilter.UserId != null)
                {
                    query = query.Where(x => x.UserId == customFilter.UserId);
                }

                if (customFilter.TagIds != null)
                {
                    query = query.Where(x => customFilter.TagIds.Any(q => x.Tags.Any(z => z.Id == q)));
                }
            }

            if (customFilter == null || customFilter.UserId != this._usersService.GetMyId())
            {
                query = query.Where(x => x.IsPublic);
            }

            return query;
        }

        #endregion
    }
}
