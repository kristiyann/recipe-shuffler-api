using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipe_shuffler.Models.CrossReference
{
    public class UserLikedRecipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
