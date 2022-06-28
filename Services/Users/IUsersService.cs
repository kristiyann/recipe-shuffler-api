using recipe_shuffler.DTO.Users;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IUsersService
    {
        Task<Guid> Insert(User user);

        string UserAuth(string email, string password);

        Guid GetCurrentUserId();
    }
}
