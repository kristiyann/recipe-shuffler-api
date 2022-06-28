using Microsoft.EntityFrameworkCore;
using recipe_shuffler.Models;
using recipe_shuffler.Models.CrossReference;

namespace recipe_shuffler.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Collection> Collections { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserLikedRecipe> UserLikedRecipes { get; set; }


        // public DbSet<RefreshToken> RefreshTokens { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Recipe>().HasKey(r => new { r.UserId, r.CopiedFromId });
        //    modelBuilder.Entity<Recipe>()
        //           .HasOne(r => r.User)
        //           .WithMany(u => u.Recipes)
        //           .HasForeignKey(r => r.UserId)
        //           .OnDelete(DeleteBehavior.Restrict);
        //    modelBuilder.Entity<Recipe>()
        //                .HasOne(r => r.CopiedFrom)
        //                .WithMany(u => u.RecipesCopies)
        //                .HasForeignKey(r => r.CopiedFromId)
        //                .OnDelete(DeleteBehavior.Restrict);
        //}
    }
}
