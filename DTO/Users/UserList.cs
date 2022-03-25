using recipe_shuffler.Models;

namespace recipe_shuffler.DTO.Users
{
    public class UserList
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public IEnumerable<Recipe>? Recipes { get; set; }
    }
}
