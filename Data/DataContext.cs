using Microsoft.EntityFrameworkCore;
using recipe_shuffler.Models;

namespace recipe_shuffler.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Recipes> Recipes { get; set; }
    }
}
