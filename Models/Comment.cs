using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipe_shuffler.Models
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }
    }
}
