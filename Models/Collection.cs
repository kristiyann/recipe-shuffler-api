using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipe_shuffler.Models
{
    public class Collection
    {
        public Collection()
        {
            Recipes = new HashSet<Recipe>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsPublic { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
