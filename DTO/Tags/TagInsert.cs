using recipe_shuffler.Models;
using System.ComponentModel.DataAnnotations;

namespace recipe_shuffler.DTO.Tags
{
    public class TagInsert
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Invalid Format")]
        public string? Color { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}
