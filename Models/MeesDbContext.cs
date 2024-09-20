using Microsoft.EntityFrameworkCore;

namespace Blok1.Models
{
    public class MeesDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=.;Initial Catalog=Mees;Integrated Security=true;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Product product1 = new Product
            {
                Id = 1,
                Name = "bloody",
                Comment = "Blood effect logo",
                Price = 10,
                ColorChange = true
            };

            modelBuilder.Entity<Product>()
                .HasData(product1);

        }
    }
}
