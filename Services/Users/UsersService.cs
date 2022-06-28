using Microsoft.IdentityModel.Tokens;
using recipe_shuffler.DTO.Users;
using recipe_shuffler.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using recipe_shuffler.Data;

namespace recipe_shuffler.Services
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentUserId() 
        {
            Guid result = Guid.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                var value = _httpContextAccessor.HttpContext.User.FindFirst("userId").Value;
                result = Guid.Parse(value);
            }

            return result;
        }

        public async Task<Guid> Insert(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public string UserAuth(string email, string password)
        {
            User user = _context.Users
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
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("userId", user.Id.ToString())
            };

            SymmetricSecurityKey key = new(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken token = new(claims: claims, expires: DateTime.Now.AddDays(25), signingCredentials: credentials);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        // private RefreshToken GetRefreshToken()
        // {
        //     RefreshToken token = new()
        //     {
        //         Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        //         CreateDate = DateTime.Now,
        //         ExpireDate = DateTime.Now.AddDays(7)
        //     };
        //
        //     return token;
        // }

        // private void SetRefreshToken(User user, RefreshToken token)
        // {
        //     CookieOptions cookieOptions = new CookieOptions()
        //     {
        //         HttpOnly = true,
        //         Expires = token.ExpireDate
        //     };
        //     
        //     IResponseCookies
        //
        //     user.RefreshToken = token;
        // }
    }
}
