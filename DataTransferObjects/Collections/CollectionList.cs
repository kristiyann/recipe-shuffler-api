namespace recipe_shuffler.DataTransferObjects
{
    public class CollectionList
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public GenericComboBox User { get; set; }
        public IEnumerable<GenericComboBox> Recipes { get; set; }
    }
}
