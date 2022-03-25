using System.ComponentModel.DataAnnotations;

namespace recipe_shuffler.DTO.Tags
{
    public class TagInsertIntoRecipe
    {
        public Guid TagId { get; set; }

        public Guid RecipeId { get; set; }
    }
}
