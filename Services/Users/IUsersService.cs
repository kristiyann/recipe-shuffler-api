using recipe_shuffler.DTO.Users;
using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IUsersService
    {
        IQueryable<UserReturnModel> Get(Guid id);

        Task<Users> Insert(Users user);

        Task<Users> Update(UserUpdateModel model);

        Users ChangeActive(Guid id);
    }
}
