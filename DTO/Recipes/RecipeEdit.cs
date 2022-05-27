namespace recipe_shuffler.DTO.Recipes
{
    public class RecipeEdit
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public string? Ingredients { get; set; } = string.Empty;
        public string? Instructions { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public IEnumerable<Guid>? TagIds { get; set; }
    }
}
