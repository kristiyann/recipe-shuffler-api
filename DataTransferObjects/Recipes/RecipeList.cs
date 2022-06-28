using recipe_shuffler.DataTransferObjects;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.DTO.Users;

namespace recipe_shuffler.DTO.Recipes
{
    public class RecipeList
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public IEnumerable<TagList> Tags { get; set; }

        public GenericComboBoxImage User { get; set; }

        public bool IsPublic { get; set; }

        public GenericComboBox CopiedFrom { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public bool LikedByMe { get; set; }

        public int LikeCount { get; set; }
    }
}
