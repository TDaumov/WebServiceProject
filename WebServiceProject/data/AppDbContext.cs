using Microsoft.EntityFrameworkCore;
using WebServiceProject.Models;

namespace WebServiceProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Таблица Products
        public DbSet<Product> Products => Set<Product>();

        // ✅ Настройки таблицы (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Price)
                      .HasPrecision(10, 2)
                      .IsRequired();

                entity.Property(p => p.Description)
                      .HasMaxLength(500);
            });
        }
    }
}
