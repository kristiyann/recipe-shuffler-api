using recipe_shuffler.Data;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _context;

        public UsersService(DataContext context)
        {
            _context = context;
        }

        public Users Get(Guid id)
        {
            Users user = _context.Users.FirstOrDefault(x => x.Id == id);

            return user;
        }
    }
}
