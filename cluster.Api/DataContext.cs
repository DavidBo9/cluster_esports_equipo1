using cluster.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace cluster.Api
{
    public class DataContext : DbContext
    {
        public DbSet <City> Cities { get; set; }
        public DbSet <Country> Countries { get; set; }

        public DataContext(DbContextOptions<DataContext>dbContext):base(dbContext) { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<City>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
