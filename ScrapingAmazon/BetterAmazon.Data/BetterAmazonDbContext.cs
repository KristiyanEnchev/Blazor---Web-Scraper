namespace BetterAmazon.Data
{
    using BetterAmazon.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BetterAmazonDbContext : DbContext
    {
        public BetterAmazonDbContext(DbContextOptions<BetterAmazonDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Books", Url = "books", Icon = "book" },
                    new Category { Id = 2, Name = "Software", Url = "software", Icon = "camera-slr" },
                    new Category { Id = 3, Name = "Video Games", Url = "video-games", Icon = "aperture" }
                );
        }
    }
}
