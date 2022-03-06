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

        public IQueryable<UserReturnModel> Get(Guid id)
        {
            IQueryable<UserReturnModel> user = _context.Users
                .Where(x => x.Id == id)
                .Where(x => x.Active == true)
                .Include(x => x.Recipes)
                .Select(x => new UserReturnModel()
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    Recipes = x.Recipes.Select(z => new Recipes()
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

        public async Task<Users> Insert(Users user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Users> Update(UserUpdateModel model)
        {
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            Users user = ConvertToModel(model);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public Users ChangeActive(Guid id)
        {
            Users? user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                user.Active = !user.Active;
            }

            return user;
        }

        public bool UserAuth(String email, String password)
        {
             Users? user = _context.Users
            .Where(x => x.Email == email)
            .FirstOrDefault();

            if (user != null)
            {
                return BCrypt.Net.BCrypt.Verify(password, user.Password);
            }
            else return false;
        }

        public Users ConvertToModel(UserUpdateModel model)
        {
            Users user = new();
            
            user.Id = model.Id;
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;

            return user;
        }
    }
}
