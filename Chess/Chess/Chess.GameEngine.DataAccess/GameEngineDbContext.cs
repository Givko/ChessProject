using Chess.GameEngine.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Chess.GameEngine.DataAccess
{
    public class GameEngineDbContext : DbContext
    {
        public GameEngineDbContext(DbContextOptions<GameEngineDbContext> options)
            : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Username)
                .IsUnique();

            modelBuilder.Entity<Player>()
                .Property(p => p.Username)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(p => p.PlayerOneId)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .HasOne(p => p.PlayerOne)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasOne(p => p.PlayerTwo)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}