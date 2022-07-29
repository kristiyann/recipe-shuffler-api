namespace recipe_shuffler.DTO.Recipes
{
    public class RecipeCustomFilter
    {
        public string? Ingredients { get; set; }

        public IEnumerable<Guid> TagIds;
    }
}
