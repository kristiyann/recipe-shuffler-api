using Microsoft.EntityFrameworkCore;
using recipe_shuffler.Data;
using recipe_shuffler.DTO.Users;
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

        public IQueryable<UserList> Get(Guid id)
        {
            IQueryable<UserList> user = _context.Users
                .Where(x => x.Id == id)
                .Where(x => x.Active)
                .Include(x => x.Recipes)
                .Select(x => new UserList()
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    Recipes = x.Recipes.Select(z => new Recipe()
                    {
                        Id = z.Id,
                        Title = z.Title,
                        Image = z.Image,
                        Ingredients = z.Ingredients,
                        Instructions = z.Instructions,
                        HasPork = z.HasPork,
                        HasPoultry = z.HasPoultry,
                    }),
                });

            return user;
        }

        public async Task<User> Insert(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Update(UserEdit model)
        {
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            User user = ConvertToModel(model);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Guid> UpdatePassword(UserPasswordEdit model)
        {
            User user = _context.Users
                .FirstOrDefault(x => x.Id == model.Id);

            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.Password = model.Password;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public Guid UserAuth(string email, string password)
        {
             User? user = _context.Users
                .Where(x => x.Email == email)
                .Where(x => x.Active)
                .FirstOrDefault();

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return user.Id;
                }
                else return Guid.Empty;
            }
            else return Guid.Empty;
        }

        public User ConvertToModel(UserEdit model)
        {
            User user = new();
            
            user.Id = model.Id;
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;

            return user;
        }
    }
}
