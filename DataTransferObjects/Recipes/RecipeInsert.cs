namespace recipe_shuffler.DataTransferObjects
{
    public class RecipeInsert
    {
        public string? Title { get; set; } = string.Empty;

        public string? Image { get; set; } = string.Empty;

        public string? Ingredients { get; set; } = string.Empty;

        public string? Instructions { get; set; } = string.Empty;

        public bool IsPublic { get; set; }

        public IEnumerable<Guid>? TagIds { get; set; }
    }
}
