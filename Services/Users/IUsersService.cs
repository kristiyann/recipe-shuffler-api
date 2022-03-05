using recipe_shuffler.Models;

namespace recipe_shuffler.Services
{
    public interface IUsersService
    {
        Users Get(Guid id);
    }
}
