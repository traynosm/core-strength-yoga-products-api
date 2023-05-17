using core_strength_yoga_products_api.Model;
using core_strength_yoga_products_api.Models;
using core_strength_yoga_products_api.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

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
             .WithMany(e => e.Products);

            //SeedImages(builder);
            //SeedProductTypes(builder);
            //SeedProductCategories(builder);
            //SeedProducts(builder);
            //SeedProductAttributes(builder);
        }

        //public static void SeedProductTypes(ModelBuilder builder)
        //{
        //    builder.Entity<Models.ProductType>().HasData(
        //        new Models.ProductType(
        //            id: 1,
        //            productTypeName: "Equipment",
        //            description: "Our Selection of Equipment",
        //            imageId: 1));

        //    builder.Entity<Models.ProductType>().HasData(
        //       new Models.ProductType(
        //            id: 2,
        //            productTypeName: "Clothing",
        //            description: "Our Selection of Clothing",
        //            imageId: 1));
        //}

        //public static void SeedProductCategories(ModelBuilder builder)
        //{
        //    builder.Entity<Models.ProductCategory>().HasData(
        //        new Models.ProductCategory(
        //            id: 1,
        //            productCategoryName: "Mats",
        //            description: "Our Selection of Mats",
        //            imageId: 1));
        //}

        //public static void SeedProductAttributes(ModelBuilder builder)
        //{
        //    builder.Entity<ProductAttributes>().HasData(new ProductAttributes(
        //            id: 1,
        //            stockLevel: 10,
        //            priceAdjustment: 0,
        //            colour: Colour.Red,
        //            size: Size.M,
        //            gender: Gender.Unisex,
        //            productId: 1
        //        ));
        //}

        //public static void SeedImages(ModelBuilder builder)
        //{
        //    builder.Entity<Image>().HasData(new Image(
        //            id: 1,
        //            imageName: "yoga-mat-1",
        //            alt: "some alt",
        //            path: "~/images/banner-1.jpg",
        //            productId: 1
        //        ));
        //}

        //public static void SeedProducts(ModelBuilder builder)
        //{
        //    var product = new Product(
        //        id: 1,
        //        name: "Bog Standard Yoga Mat",
        //        description: "The worst yoga mat you have ever seen",
        //        fullPrice: 30m,
        //        productCategoryId: 1,
        //        productTypeId: 1
        //    );

        //    builder.Entity<Product>().HasData(product);
        //}
    }
}
