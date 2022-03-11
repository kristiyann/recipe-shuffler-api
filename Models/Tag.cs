using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace recipe_shuffler.Models
{
    public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        
        // Hex Code Regex

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Invalid Format")]
        public string? Color { get; set; } = string.Empty;

        [JsonIgnore]
        public HashSet<Recipe>? Recipes { get; set; }

        public Guid? UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
