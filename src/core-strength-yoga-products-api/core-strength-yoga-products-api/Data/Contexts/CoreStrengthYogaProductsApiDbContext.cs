using core_strength_yoga_products_api.Model;
using core_strength_yoga_products_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_api.Data.Contexts
{
    public class CoreStrengthYogaProductsApiDbContext : IdentityDbContext<IdentityUser>
    {
        public CoreStrengthYogaProductsApiDbContext(DbContextOptions<CoreStrengthYogaProductsApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<ProductAttributes> ProductAttributes { get; set; }
        public DbSet<Models.ProductCategory> ProductCategories { get; set; }
        public DbSet<Models.ProductType> ProductTypes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
             .HasMany(e => e.ProductAttributes)
             .WithOne(e => e.Product);
        }
    }
}
