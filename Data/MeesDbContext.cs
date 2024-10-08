using Blok1.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Blok1.Data
{
    public class MeesDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Orderline> OrderProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orderline>()
                .HasKey(ol => new { ol.OrderId, ol.ProductId });

            modelBuilder.Entity<Orderline>()
                .HasOne(ol => ol.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(ol => ol.OrderId);

            modelBuilder.Entity<Orderline>()
                .HasOne(ol => ol.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(ol => ol.ProductId);

            Product product1 = new Product
            {
                Id = 1,
                Name = "bloody",
                Comment = "Blood effect logo",
                Price = 10,
                ColorChange = true,
                CategoryId = 1
            };

            modelBuilder.Entity<Product>()
                .HasData(product1);


            Category category1 = new Category
            {
                Id = 1,
                Name = "Anime",
            };

            modelBuilder.Entity<Category>()
                .HasData(category1);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=.;Initial Catalog=Mees;Integrated Security=true;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
