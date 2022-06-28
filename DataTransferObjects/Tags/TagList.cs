namespace recipe_shuffler.DataTransferObjects
{
    public class TagList
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Color { get; set; } = string.Empty;
    }
}
