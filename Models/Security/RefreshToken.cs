using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipe_shuffler.Models;

public class RefreshToken
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    
    public string Token { get; set; }
    
    public DateTime CreateDate { get; set; } = DateTime.Now;
    
    public DateTime ExpireDate { get; set; }

    //public Guid UserId { get; set; }
    
    //public User User { get; set; }
}