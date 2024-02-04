using GameReviewApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // primary keys
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Game>()
                .HasKey(g => g.Id);
            modelBuilder.Entity<Company>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Profile>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Review>()
                .HasKey(r => r.Id);
            modelBuilder.Entity<Reviewer>()
                .HasKey(r => r.Id);

            // one to one ( Reviewer <-> Profile )

            modelBuilder.Entity<Reviewer>()
                .HasOne(r => r.Profile)
                .WithOne(p => p.Reviewer)
                .HasForeignKey<Profile>(p => p.ReviewerId);

            // one to many ( Company <-> Game )

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Games)
                .WithOne(g => g.Company)
                .HasForeignKey(g => g.CompanyId);

            // one to many ( Game <-> Review )

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Reviews)
                .WithOne(r => r.Game)
                .HasForeignKey(r => r.GameId);

            // one to many ( Reviewer <-> Review )

            modelBuilder.Entity<Reviewer>()
                .HasMany(r => r.Reviews)
                .WithOne(rr => rr.Reviewer)
                .HasForeignKey(rr => rr.ReviewerId);

            // many to many ( Game <-> Category )

            modelBuilder.Entity<GameCategory>()
                .HasKey(gc => new { gc.GameId, gc.CategoryId });
            modelBuilder.Entity<GameCategory>()
                .HasOne(g => g.Game)
                .WithMany(gc => gc.GameCategories)
                .HasForeignKey(c => c.GameId);
            modelBuilder.Entity<GameCategory>()
                .HasOne(g => g.Category)
                .WithMany(gc => gc.GameCategories)
                .HasForeignKey(c => c.CategoryId);
        }

    }
}
