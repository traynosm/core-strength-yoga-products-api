using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Model;
using core_strength_yoga_products_api.Models;
using core_strength_yoga_products_api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_api.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CoreStrengthYogaProductsApiDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<CoreStrengthYogaProductsApiDbContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Products.Any())
                {
                    return;
                }

                var image = new Image(
                    id: 1,
                    imageName: "yoga-mat-1",
                    alt: "some alt",
                    path: "~/images/banner-1.jpg"
                );

                var productType = new Models.ProductType(
                    id: 1,
                    productTypeName: "Equipment",
                    description: "Our Selection of Equipment"
                );
                productType.Image = image;

                var productCategory = new Models.ProductCategory(
                    id: 1,
                    productCategoryName: "Mats",
                    description: "Our Selection of Mats"
                );
                productCategory.Image = image;

                var productAttribute = new ProductAttributes(
                    id: 1,
                    stockLevel: 10,
                    priceAdjustment: 0,
                    colour: Colour.Red,
                    size: Size.M,
                    gender: Gender.Unisex
                );
                //context.ProductAttributes.Add(productAttribute);
                //context.SaveChanges();

                var product = new Product(
                    id: 1,
                    name: "Bog Standard Yoga Mat",
                    description: "The worst yoga mat you have ever seen",
                    fullPrice: 30m
                );
                product.ProductType = productType;
                product.ProductCategory = productCategory;
                product.Image = image;
                product.ProductAttributes = new List<ProductAttributes> { productAttribute };

                context.Products.Add(product);

                context.SaveChanges();
            }
        }
    }
}
