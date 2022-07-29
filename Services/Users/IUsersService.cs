using recipe_shuffler.DTO.Security;
using recipe_shuffler.DTO.Users;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IUsersService
    {
        Task<Guid> Insert(User user);

        TokenResponse UserAuth(string email, string password);

        Guid GetMyId();
    }
}
