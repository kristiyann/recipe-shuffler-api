using Microsoft.EntityFrameworkCore; // Include
using recipe_shuffler.Data;
using recipe_shuffler.DataTransferObjects.Recipes;
using recipe_shuffler.DTO;
using recipe_shuffler.DTO.Recipes;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.DataTransferObjects;
using recipe_shuffler.Models;
using recipe_shuffler.Models.CrossReference;
using recipe_shuffler.Services.Base;

namespace recipe_shuffler.Services
{
    public class RecipesService : RSBaseService, IRecipesService
    {
        private readonly DataContext _context;
        private readonly IUsersService _usersService;

        public RecipesService(DataContext context, IUsersService usersService) : base(context, usersService)
        {
        }

        public IQueryable<RecipeList> GetList(RecipeCustomFilter customFilter)
        {
            List<Guid> userLikesIds = this.GetCurrentUserLikesIds();

            IQueryable<Recipe> query = this.GenerateInitialQuery();

            query = this.ApplyCustomFilter(query, customFilter);

            IQueryable<RecipeList> list = this.ConvertToListModel(query, userLikesIds);

            return list;
        }

        public async Task<Recipe> Insert(RecipeInsert model)
        {
            Recipe recipe = ConvertToDbObj(model);
            recipe.CreateDate = DateTime.Now;

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
                recipe = await ConvertToDbObj(model);
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

        public IEnumerable<RecipeList> GetRandom(RecipeCustomFilter customFilter)
        {
            IQueryable<Recipe> query = this.GenerateInitialQuery()
                .Include(x => x.Tags)
                .Include(x => x.User);

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

            List<Guid> userLikesIds = this.GetCurrentUserLikesIds();

            IEnumerable<RecipeList> list = this.ConvertToListModel(recipeCollection, userLikesIds);

            return list;
        }

        public async Task<Guid> Copy(Guid id)
        {
            Recipe originalRecipe = this.GenerateInitialQuery(id)
                .Include(r => r.Tags)
                .FirstOrDefault();

            if (originalRecipe is not null)
            {
                Recipe newRecipe = new()
                {
                    Title = originalRecipe.Title,
                    Ingredients = originalRecipe.Ingredients,
                    Image = originalRecipe.Image,
                    IsPublic = true,
                    Instructions = originalRecipe.Instructions,
                    CopiedFromId = id,
                    UserId = _usersService.GetCurrentUserId(),
                    CreateDate = DateTime.Now
                };

                await _context.Recipes.AddAsync(newRecipe);

                foreach (Tag tag in originalRecipe.Tags)
                {
                    Tag newTag = new()
                    {
                        Name = tag.Name,
                        Color = tag.Color,
                        UserId = _usersService.GetCurrentUserId(),
                    };

                    await _context.Tags.AddAsync(newTag);

                    newRecipe.Tags.Add(newTag);
                }

                await _context.SaveChangesAsync();

                return newRecipe.Id;
            }

            return Guid.Empty;
        }

        public async Task<bool> LikeOrDislike(Guid id)
        {
            bool result = false;

            Recipe recipe = this.GenerateInitialQuery(id)
                .FirstOrDefault();

            if (recipe != null)
            { 
                UserLikedRecipe query = _context.UserLikedRecipes.Where(x => x.RecipeId == recipe.Id)
                    .Where(x => x.UserId == _usersService.GetCurrentUserId())
                    .FirstOrDefault();

                if (query == null)
                {
                    UserLikedRecipe userLike = new()
                    {
                        RecipeId = recipe.Id,
                        UserId = _usersService.GetCurrentUserId(),
                        CreateDate = DateTime.Now
                    };

                    await _context.UserLikedRecipes.AddAsync(userLike);
                }
                else {
                    _context.UserLikedRecipes.Remove(query);
                }

                await _context.SaveChangesAsync();
                result = true;
            }

            return result;
        }
        
        #region InitialQuery

        private IQueryable<Recipe> GenerateInitialQuery(Guid? id = null, Guid? userId = null)
        {
            IQueryable<Recipe> query = _context.Recipes.Where(x => x.UserId == _usersService.GetCurrentUserId());

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
        private Recipe ConvertToDbObj(RecipeInsert model)
        {
            Recipe recipe = new()
            {
                Title = model.Title,
                Image = model.Image,
                Instructions = model.Instructions,
                Ingredients = model.Ingredients,
                User = _context.Users.Find(_usersService.GetCurrentUserId()),
                Tags = new HashSet<Tag>(),
                IsPublic = model.IsPublic
            };

            return recipe;
        }

        private async Task<Recipe> ConvertToDbObj(RecipeEdit model)
        {
            Recipe recipe = new()
            {
                Id = model.Id,
                Title = model.Title,
                Image = model.Image,
                Instructions = model.Instructions,
                Ingredients = model.Ingredients,
                // UserId = model.UserId,
                User = await _context.Users.FindAsync(_usersService.GetCurrentUserId()),
                IsPublic = model.IsPublic
            };

            return recipe;
        }

        private IQueryable<RecipeList> ConvertToListModel(IQueryable<Recipe> query, List<Guid> userLikesIds)
        {
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
                    User = new GenericComboBoxImage()
                    {
                        Value = x.UserId,
                        Text = x.User.Username
                    },
                    CopiedFrom = x.CopiedFrom != null ? new GenericComboBox()
                    {
                        Text = x.CopiedFrom.Title + " by @" + x.CopiedFrom.User.Username,
                        Value = x.Id
                    } : null,
                    CreateDate = x.CreateDate,
                    LikedByMe = userLikesIds.Any(z => z == x.Id),
                    LikeCount = x.UserLikedRecipes.Count
                });

            return list;
        }

        private IEnumerable<RecipeList> ConvertToListModel(List<Recipe> query, List<Guid> userLikesIds)
        {
            IEnumerable<RecipeList> list = query
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
                    User = new GenericComboBoxImage()
                    {
                        Value = x.UserId,
                        Text = x.User.Username
                    },
                    CopiedFrom = x.CopiedFrom != null ? new GenericComboBox()
                    {
                        Text = x.CopiedFrom.Title + " by @" + x.CopiedFrom.User.Username,
                        Value = x.Id
                    } : null,
                    CreateDate = x.CreateDate == DateTime.MinValue ? DateTimeOffset.MinValue : x.CreateDate,
                    LikedByMe = userLikesIds.Any(z => z == x.Id),
                    LikeCount = x.UserLikedRecipes.Count
                });

            return list;
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

            if (customFilter == null || customFilter.UserId != this._usersService.GetCurrentUserId())
            {
                query = query.Where(x => x.IsPublic);
            }

            return query;
        }

        #endregion

        #region Helpers

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

        

        #endregion
    }
}
