using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Model;
using core_strength_yoga_products_api.Models;
using core_strength_yoga_products_api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_api.Data
{
    public static class SeedData
    {
        private static IEnumerable<Image> _images;
        private static IEnumerable<Models.ProductCategory> _productCategories;
        private static IEnumerable<Models.ProductType> _productTypes;
        private static IEnumerable<Product> _products;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _images = SeedImages();
            _productCategories = SeedProductCategories();
            _productTypes = SeedProductTypes();
            _products = SeedProducts();

            using (var context = new CoreStrengthYogaProductsApiDbContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<CoreStrengthYogaProductsApiDbContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Products.Any())
                {
                    Console.WriteLine("The database contains data and cannot be seeded");
                 
                    return;
                }

                foreach(var product in _products)
                {
                    context.Products.Add(product);
                }

                context.SaveChanges();
            }
        }

        private static IEnumerable<Product> SeedProducts()
        {
            return new List<Product> 
            { 
                new Product(
                    id: 1,
                    name: "Bog Standard Yoga Mat",
                    description: "The worst yoga mat you have ever seen",
                    fullPrice: 30m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Mats),
                    image: _images.ByName("yoga-mat-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 1,
                            stockLevel: 10,
                            priceAdjustment: 0,
                            colour: Colour.Red,
                            size: Size.M,
                            gender: Gender.Unisex),
                        new ProductAttributes(
                            id: 2,
                            stockLevel: 15,
                            priceAdjustment: 0,
                            colour: Colour.Blue,
                            size: Size.M,
                            gender: Gender.Unisex),
                        new ProductAttributes(
                            id: 3,
                            stockLevel: 15,
                            priceAdjustment: 0,
                            colour: Colour.Green,
                            size: Size.M,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 2,
                    name: "Bog Standard Yoga Block",
                    description: "The worst yoga block you have ever seen",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Blocks),
                    image: _images.ByName("yoga-mat-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 4,
                            stockLevel: 100,
                            priceAdjustment: -0.5m,
                            colour: Colour.Red,
                            size: Size.M,
                            gender: Gender.Unisex),
                    })
            };
        }

        private static IEnumerable<Image> SeedImages()
        {
            return new List<Image> 
            {
                new Image(
                    id: 1,
                    imageName: "yoga-mat-1",
                    alt: "some alt",
                    path: "~/images/banner-1.jpg"),
                new Image(
                    id: 2,
                    imageName: "yoga-mat-2",
                    alt: "some alt",
                    path: "~/images/banner-2.jpg"),
                new Image(
                    id: 3,
                    imageName: "yoga-mat-3",
                    alt: "some alt",
                    path: "~/images/banner-3.jpg"),
            };
        }

        private static IEnumerable<Models.ProductCategory> SeedProductCategories() 
        {
            return new List<Models.ProductCategory>
            {
                new Models.ProductCategory(
                    id: 1,
                    productCategoryName: "Equipment",
                    description: "Our Selection of Equipment",
                    image: _images!.ByName("yoga-mat-1")),
                new Models.ProductCategory(
                    id: 2,
                    productCategoryName: "Clothing",
                    description: "Our Selection of Clothing",
                    image: _images!.ByName("yoga-mat-2")),
            };
        }

        private static IEnumerable<Models.ProductType> SeedProductTypes()
        {
            return new List<Models.ProductType>
            {
                new Models.ProductType(
                    id: 1,
                    productTypeName: "Mats",
                    description: "Our Selection of Mats",
                    image: _images!.ByName("yoga-mat-1")),
                new Models.ProductType(
                    id: 2,
                    productTypeName: "Blocks",
                    description: "Our Selection of Blocks",
                    image: _images!.ByName("yoga-mat-2")),
                new Models.ProductType(
                    id: 3,
                    productTypeName: "Bolsters",
                    description: "Our Selection of Bolsters",
                    image: _images!.ByName("yoga-mat-3")),
               new Models.ProductType(
                    id: 4,
                    productTypeName: "Yoga Tops",
                    description: "Our Selection of Yoga Tops",
                    image: _images!.ByName("yoga-mat-3")),
               new Models.ProductType(
                    id: 5,
                    productTypeName: "Yoga Leggings",
                    description: "Our Selection of Yoga Leggings",
                    image: _images!.ByName("yoga-mat-3")),
            };
        }

        private static Image ByName(this IEnumerable<Image> images, string name)
        {
            return images.FirstOrDefault(i => i.ImageName == name) ?? 
                throw new NullReferenceException($"Invalid Image Name");
        }

        private static Models.ProductCategory ByName(this IEnumerable<Models.ProductCategory> productCategories, Models.Enums.ProductCategory name)
        {
            return productCategories.FirstOrDefault(i => i.ProductCategoryName == name.ToString()) ??
                throw new NullReferenceException($"Invalid Product Category Name");
        }

        private static Models.ProductType ByName(this IEnumerable<Models.ProductType> productTypes, Models.Enums.ProductType name)
        {
            return productTypes.FirstOrDefault(i => i.ProductTypeName == name.ToString()) ??
                throw new NullReferenceException($"Invalid Product Type Name");
        }
    }
}
