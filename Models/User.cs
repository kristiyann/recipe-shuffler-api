using recipe_shuffler.Models.CrossReference;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipe_shuffler.Models
{
    public class User
    {
        public User() 
        {
            Recipes = new HashSet<Recipe>();
            UserLikedRecipes = new HashSet<UserLikedRecipe>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public ICollection<Recipe> Recipes { get; set; }

        public bool Active { get; set; } = true;

        public ICollection<UserLikedRecipe> UserLikedRecipes { get; set; }

        //public Guid? RefreshTokenId { get; set; }

        //public RefreshToken? RefreshToken { get; set; }
    }
}
