namespace recipe_shuffler.DTO
{
    public class RecipeInsertModel
    {
        public Guid Id { get; set; }

        public string? Title { get; set; } = string.Empty;

        public string? Image { get; set; } = string.Empty;

        public string? Ingredients { get; set; } = string.Empty;

        public string? Instructions { get; set; } = string.Empty;

        public bool HasPoultry { get; set; }

        public bool HasPork { get; set; }

        public Guid? UserId { get; set; }
    }
}
