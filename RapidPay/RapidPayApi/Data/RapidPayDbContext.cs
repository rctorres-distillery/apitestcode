using Microsoft.EntityFrameworkCore;
using RapidPayApi.Data.Entities;

namespace RapidPayApi.Data
{
    public class RapidPayDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("RapidPayDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasMany(c => c.Payments)
                .WithOne();
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
