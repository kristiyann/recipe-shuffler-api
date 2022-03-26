using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace recipe_shuffler.Models
{
    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Image { get; set; } = string.Empty;

        public string? Ingredients { get; set; } = string.Empty;

        public string? Instructions { get; set; } = string.Empty;

        // TODO Deprecate
        public bool HasPoultry { get; set; }

        // TODO Deprecate
        public bool HasPork { get; set; }

        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public HashSet<Tag>? Tags { get; set; }
    }
}
