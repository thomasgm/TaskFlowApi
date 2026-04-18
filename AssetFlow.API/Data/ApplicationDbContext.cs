using AssetFlow.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetFlow.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Popula tabelas ao rodar primeira migration (Seeding)
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Hardware", Description = "Computadores" },
                new Category { Id = 2, Name = "Software", Description = "Licenças" }
            );
        }
    }
}