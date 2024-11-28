using System.Numerics;
using System.Text.RegularExpressions;
using cluster.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace cluster.Api
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Shared.Entities.Match> Matches { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Existing configurations
            modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Team>().HasIndex(x => x.TeamName).IsUnique();
            modelBuilder.Entity<Tournament>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Player>()
                .HasOne(p => p.User)
                .WithOne(u => u.Player)
                .HasForeignKey<Player>(p => p.UserId);

            // New configurations for Match relationships
            modelBuilder.Entity<Shared.Entities.Match>()
                .HasOne(m => m.Team1)
                .WithMany()
                .HasForeignKey(m => m.Team1Id)
                .OnDelete(DeleteBehavior.Restrict);  // Changed from Cascade

            modelBuilder.Entity<Shared.Entities.Match>()
                .HasOne(m => m.Team2)
                .WithMany()
                .HasForeignKey(m => m.Team2Id)
                .OnDelete(DeleteBehavior.Restrict);  // Changed from Cascade

            modelBuilder.Entity<Shared.Entities.Match>()
                .HasOne(m => m.Tournament)
                .WithMany(t => t.Matches)
                .HasForeignKey(m => m.TournamentId);
        }
    }
}
