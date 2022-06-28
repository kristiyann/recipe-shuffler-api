using recipe_shuffler.Models.CrossReference;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipe_shuffler.Models
{
    public class Recipe
    {
        public Recipe()
        {
            Tags = new HashSet<Tag>();
            UserLikedRecipes = new HashSet<UserLikedRecipe>();
            Collections = new HashSet<Collection>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public bool IsPublic { get; set; } = true;

        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public Guid? CopiedFromId { get; set; }

        public Recipe? CopiedFrom { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<Collection> Collections { get; set; }

        public ICollection<UserLikedRecipe> UserLikedRecipes { get; set; }
    }
}
