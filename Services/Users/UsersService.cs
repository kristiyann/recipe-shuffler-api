using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using recipe_shuffler.Data;
using recipe_shuffler.DTO.Users;
using recipe_shuffler.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace recipe_shuffler.Services
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UsersService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
                    }),
                });

            return user;
        }

        public async Task<Guid> Insert(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<Guid> Update(UserEdit model)
        {
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            User user = ConvertEditToDbObj(model);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user.Id;
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

        public string UserAuth(string email, string password)
        {
            User? user = _context.Users
                .Where(x => x.Email == email)
                .FirstOrDefault(x => x.Active);

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return CreateJwtToken(user);
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private static User ConvertEditToDbObj(UserEdit model)
        {
            User user = new()
            {
                Id = model.Id,
                Username = model.Username,
                Password = model.Password,
                Email = model.Email
            };

            return user;
        }

        private string CreateJwtToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            SymmetricSecurityKey key = new(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken token = new(claims: claims, expires: DateTime.Now.AddDays(2), signingCredentials: credentials);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
