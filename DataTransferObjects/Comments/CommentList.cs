namespace recipe_shuffler.DataTransferObjects
{
    public class CommentList
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public GenericComboBoxImage User { get; set; }
        public Guid ParentCommentId { get; set; }
    }
}
