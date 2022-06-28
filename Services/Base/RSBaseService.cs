using Microsoft.AspNetCore.Mvc;
using recipe_shuffler.Data;

namespace recipe_shuffler.Services.Base
{
    public class RSBaseService : Controller
    {
        private readonly DataContext _context;
        private readonly IUsersService _usersService;
        public RSBaseService(DataContext context, IUsersService usersService)
        {
            _context = context;
            _usersService = usersService;
        }

        protected List<Guid> GetCurrentUserLikesIds()
        {
            List<Guid> userLikesIds = _context.UserLikedRecipes
                .Where(u => u.UserId == _usersService.GetCurrentUserId())
                .Select(z => z.RecipeId)
                .ToList();

            return userLikesIds;
        }
    }
}
