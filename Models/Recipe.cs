using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace recipe_shuffler.Models
{
    public class Recipe
    {
        public Recipe()
        {
            Tags = new HashSet<Tag>();
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
    }
}
