using recipe_shuffler.DTO.Users;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IUsersService
    {
        IQueryable<UserReturn> Get(Guid id);

        Task<User> Insert(User user);

        Task<User> Update(UserUpdate model);

        User ChangeActive(Guid id);

        bool UserAuth(String email, String password);
    }
}
