namespace recipe_shuffler.DataTransferObjects
{
    public class RecipeCustomFilter
    {
        public string? Ingredients { get; set; }
        public Guid? UserId { get; set; }

        public IEnumerable<Guid> TagIds;
    }
}
