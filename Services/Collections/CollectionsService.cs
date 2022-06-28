using Microsoft.EntityFrameworkCore;
using recipe_shuffler.Data;
using recipe_shuffler.DataTransferObjects;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public class CollectionsService : ICollectionsService
    {
        private readonly DataContext _context;
        private readonly IUsersService _usersService;

        public CollectionsService(DataContext context, IUsersService usersService)
        {
            _context = context;
            _usersService = usersService;
        }

        public IQueryable<CollectionList> GetList()
        {
            IQueryable<Collection> query = _context.Collections;

            IQueryable<CollectionList> list = query
                .Select(x => new CollectionList()
                {
                    Id = x.Id,
                    CreateDate = x.CreateDate,
                    IsPublic = x.IsPublic,
                    Name = x.Name,
                    User = new GenericComboBoxImage() 
                    {
                        Value = x.User.Id,
                        Text = x.User.Username
                    },
                    Recipes = x.UserId == _usersService.GetCurrentUserId() ? x.Recipes.Select(r => new GenericComboBox()
                    {
                        Value = r.Id,
                        Text = r.Title
                    }) : x.Recipes.Where(r => r.IsPublic).Select(r => new GenericComboBox()
                    {
                        Value = r.Id,
                        Text = r.Title
                    })
                });

            return list;
        }

        public async Task<Guid> Insert(CollectionEdit toInsert)
        {
            Collection collection = new()
            {
                UserId = _usersService.GetCurrentUserId(),
                Name = toInsert.Name,
                IsPublic = toInsert.IsPublic,
                CreateDate = DateTime.Now
            };

            await _context.Collections.AddAsync(collection);
            await _context.SaveChangesAsync();

            return collection.Id;
        }

        public async Task<bool> UpdateInline(CollectionList toUpdate)
        {
            bool result = false;

            Collection collection = _context.Collections
                .Where(x => x.UserId == _usersService.GetCurrentUserId())
                .Include(collection => collection.Recipes)
                .FirstOrDefault();

            if (collection != null)
            {
                ConvertToDbObj(collection, toUpdate);

                foreach (Recipe recipe in collection.Recipes.ToList())
                {
                    if (!toUpdate.Recipes.Any(p => p.Value == recipe.Id))
                    {
                        collection.Recipes.Remove(recipe);
                    }
                }

                List<Guid> dbObjRecipesIds = collection.Recipes.Select(x => x.Id).ToList();
                IEnumerable<Guid> toUpdateRecipesId = toUpdate.Recipes.Select(x => x.Value);

                List<Recipe> recipes = await _context.Recipes
                    .Where(y => !dbObjRecipesIds.Contains(y.Id))
                    .Where(x => toUpdateRecipesId.Any(c => c == x.Id))
                    .ToListAsync();

                foreach (Recipe recipe in recipes)
                {
                    collection.Recipes.Add(recipe);
                }

                await _context.SaveChangesAsync();

                result = true;
            }

            return result;
        }

        public async Task<bool> Delete(Guid id)
        {
            bool result = false;

            Collection collection = _context.Collections
                .Where(x => x.UserId == _usersService.GetCurrentUserId())
                .FirstOrDefault();

            if (collection != null)
            {
                _context.Collections.Remove(collection);
                await _context.SaveChangesAsync();

                result = true;
            }

            return result;
        }

        #region Converters

        private static void ConvertToDbObj(Collection dbObj, CollectionList toUpdate)
        {
            dbObj.IsPublic = toUpdate.IsPublic;
            dbObj.Name = toUpdate.Name;
        }

        #endregion
    }
}
