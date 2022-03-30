using recipe_shuffler.DTO.Users;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IUsersService
    {
        IQueryable<UserList> Get(Guid id);

        Task<Guid> Insert(User user);

        Task<Guid> Update(UserEdit model);

        //Task<User> ChangeActive(Guid id);

        Task<Guid> UpdatePassword(UserPasswordEdit model);

        Guid UserAuth(String email, String password);
    }
}
