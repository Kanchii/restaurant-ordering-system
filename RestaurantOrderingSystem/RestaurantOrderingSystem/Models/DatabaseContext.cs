using Microsoft.EntityFrameworkCore;

namespace RestaurantOrderingSystem.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();

            base.OnModelCreating(builder);
        }
    }
}
