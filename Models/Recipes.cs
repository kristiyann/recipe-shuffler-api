using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace recipe_shuffler.Models
{
    public class Recipes
    {
        [Key]
        public Guid Id { get; set; }

        public string? Title { get; set; } = string.Empty;

        public string? Image { get; set; } = string.Empty;

        public string? Ingredients { get; set; } = string.Empty;

        public string? Instructions { get; set; } = string.Empty;

        public bool HasPoultry { get; set; }

        public bool HasPork { get; set; }

        public Guid? UserId { get; set; }

        [JsonIgnore]
        public Users? User { get; set; }
    }
}
