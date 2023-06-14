using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Model;
using core_strength_yoga_products_api.Models;
using core_strength_yoga_products_api.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_api.Data
{
    public static class SeedData
    {
        private static IEnumerable<Image> _images;
        private static IEnumerable<Models.ProductCategory> _productCategories;
        private static IEnumerable<Models.ProductType> _productTypes;
        private static IEnumerable<Product> _products;
        private static IEnumerable<Customer> _customers;
        private static IEnumerable<IdentityUser> _users;
        private static IEnumerable<CustomerDetail> _customerDetails;
        private static IEnumerable<AddressDetail> _addressDetails;
        private static IEnumerable<Order> _orders;
        private static RoleManager<IdentityRole> _roleManager;
        private static UserManager<IdentityUser> _userManager;


        public static void Initialize(IServiceProvider serviceProvider)
        {
            _images = SeedImages();
            _productCategories = SeedProductCategories();
            _productTypes = SeedProductTypes();
            _products = SeedProducts();
            _addressDetails = SeedAddressDetails();
            _customerDetails = SeedCustomerDetails();
            _customers = SeedCustomers();
            _users = SeedUsers();
            _orders = SeedOrders();
            _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            SeedRoles();
            
            using (var context = new CoreStrengthYogaProductsApiDbContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<CoreStrengthYogaProductsApiDbContext>>()))
            {
                context.Database.EnsureCreated();

                context.Database.ExecuteSqlRaw
                    ("DELETE FROM [Images];" +
                    "DELETE FROM [ProductCategories];" +
                    "DELETE FROM [ProductTypes];" +
                    "DELETE FROM [ProductAttributes];" +
                    "DELETE FROM [Products];" +
                    "DELETE FROM [AddressDetail];" +
                    "DELETE FROM [CustomerDetail];" +
                    "DELETE FROM [Customers];" +
                    "DELETE FROM [BasketItem];" +
                    "DELETE FROM [AspNetUsers];" +
                    "DELETE FROM [Orders];");

                if (context.Products.Any())
                {
                    Console.WriteLine("The Products database contains data and cannot be seeded");
                }
                else
                {
                    foreach (var product in _products)
                    {
                        context.Products.Add(product);
                    }
                }

                if (context.Customers.Any())
                {
                    Console.WriteLine("The Customers database contains data and cannot be seeded");
                }
                else
                {
                    foreach (var customer in _customers)
                    {
                        context.Customers.Add(customer);
                    }
                }

                if (context.Users.Any())
                {
                    Console.WriteLine("Identity Users already present");
                }
                else
                {
                    CreateUsers();
                }

                if(context.Orders.Any())
                {
                    Console.WriteLine("The Orders database contains data and cannot be seeded");
                }
                else
                {
                    foreach (var order in _orders)
                    {
                        context.Orders.Add(order);
                    }
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
                    name: "Standard Yoga Mat",
                    description: "Get started with this classic yoga mat",
                    fullPrice: 30m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Mats),
                    image: _images.ByName("mat-1"),
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
                    name: "Standard Yoga Block",
                    description: "The worst yoga block you have ever seen",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Blocks),
                    image: _images.ByName("Blocks"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 4,
                            stockLevel: 100,
                            priceAdjustment: -0.5m,
                            colour: Colour.Red,
                            size: Size.M,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 3,
                    name: "Luxury Yoga Bolster",
                    description: "The worst yoga block you have ever seen",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Bolsters),
                    image: _images.ByName("bolster-2"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 5,
                            stockLevel: 100,
                            priceAdjustment: -0.5m,
                            colour: Colour.Purple,
                            size: Size.M,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 4,
                    name: "Standard Yoga Bolster",
                    description: "Aid your yoga practice with this bolseter",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Bolsters),
                    image: _images.ByName("bolster-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 6,
                            stockLevel: 100,
                            priceAdjustment: -0.5m,
                            colour: Colour.Red,
                            size: Size.M,
                            gender: Gender.Unisex),
                        new ProductAttributes(
                            id: 7,
                            stockLevel: 10,
                            priceAdjustment: -0.5m,
                            colour: Colour.Green,
                            size: Size.M,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 5,
                    name: "Womens Yoga halter top",
                    description: "Move with ease on your yoga practice with this bright,confortable top. 1OO% Cotton.",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Clothing),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Tops),
                    image: _images.ByName("halter-top"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 8,
                            stockLevel: 100,
                            priceAdjustment: -0.5m,
                            colour: Colour.Red,
                            size: Size.M,
                            gender: Gender.Womens),
                        new ProductAttributes(
                            id: 9,
                            stockLevel: 100,
                            priceAdjustment: 0M,
                            colour: Colour.Pink,
                            size: Size.M,
                            gender: Gender.Womens),
                        new ProductAttributes(
                            id: 10,
                            stockLevel: 100,
                            priceAdjustment: -0.10m,
                            colour: Colour.Purple,
                            size: Size.M,
                            gender: Gender.Womens),
                    }),
                new Product(
                    id: 6,
                    name: "Men's Leggings",
                    description: "Move with ease on your yoga practice with these comfortable leggings. 1OO% Cotton.",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Clothing),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Bottoms),
                    image: _images.ByName("man-leggings-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 11,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Blue,
                            size: Size.M,
                            gender: Gender.Mens),
                        new ProductAttributes(
                            id: 12,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Black,
                            size: Size.M,
                            gender: Gender.Mens),
                    }),
                new Product(
                    id: 7,
                    name: "Men's Shorts",
                    description: "Move with ease on your yoga practice with these comfortable shorts. 1OO% Cotton.",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Clothing),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Bottoms),
                    image: _images.ByName("man-shorts-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 13,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Blue,
                            size: Size.M,
                            gender: Gender.Mens),
                        new ProductAttributes(
                            id: 14,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Black,
                            size: Size.M,
                            gender: Gender.Mens),
                    }),
                new Product(
                    id: 8,
                    name: "Women's Shorts",
                    description: "Move with ease on your yoga practice with these comfortable shorts. 1OO% Cotton.",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Clothing),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Bottoms),
                    image: _images.ByName("woman-shorts-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 15,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.White,
                            size: Size.M,
                            gender: Gender.Womens),
                        new ProductAttributes(
                            id: 16,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Purple,
                            size: Size.L,
                            gender: Gender.Womens),
                        new ProductAttributes(
                            id: 17,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Red,
                            size: Size.XL,
                            gender: Gender.Womens),
                    }),
                new Product(
                    id: 9,
                    name: "Women's long sleeve top",
                    description: "Move with ease on your yoga practice with these comfortable,cosy top. 1OO% Cotton.",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Clothing),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Tops),
                    image: _images.ByName("woman-top-long-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 18,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.White,
                            size: Size.M,
                            gender: Gender.Womens),
                        new ProductAttributes(
                            id: 19,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Purple,
                            size: Size.L,
                            gender: Gender.Womens),
                        new ProductAttributes(
                            id: 20,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Red,
                            size: Size.XL,
                            gender: Gender.Womens),
                    }),
                new Product(
                    id: 10,
                    name: "Women's Wrap Top",
                    description: "Move with ease on your yoga practice with these comfortable,cosy top. 1OO% Cotton.",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Clothing),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Tops),
                    image: _images.ByName("women-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 21,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.White,
                            size: Size.M,
                            gender: Gender.Womens),
                        new ProductAttributes(
                            id: 22,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Yellow,
                            size: Size.L,
                            gender: Gender.Womens),
                        new ProductAttributes(
                            id: 23,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Orange,
                            size: Size.XL,
                            gender: Gender.Womens),
                    }),
                new Product(
                    id: 11,
                    name: "Basic Yoga Bolster",
                    description: "Aid your yoga practice with this bolster",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Bolsters),
                    image: _images.ByName("bolster-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 24,
                            stockLevel: 100,
                            priceAdjustment: -0.5m,
                            colour: Colour.Red,
                            size: Size.M,
                            gender: Gender.Unisex),
                        new ProductAttributes(
                            id: 25,
                            stockLevel: 10,
                            priceAdjustment: -0.5m,
                            colour: Colour.Green,
                            size: Size.M,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 12,
                    name: "Luxury Yoga Mat",
                    description: "Take your yoga practice to the next level. This mat will make you feel like your practice is on a cloud.",
                    fullPrice: 30m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Mats),
                    image: _images.ByName("mat-2"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 26,
                            stockLevel: 10,
                            priceAdjustment: 0,
                            colour: Colour.Red,
                            size: Size.M,
                            gender: Gender.Unisex),
                        new ProductAttributes(
                            id: 27,
                            stockLevel: 15,
                            priceAdjustment: 0,
                            colour: Colour.Blue,
                            size: Size.M,
                            gender: Gender.Unisex),
                        new ProductAttributes(
                            id: 28,
                            stockLevel: 15,
                            priceAdjustment: 0,
                            colour: Colour.Green,
                            size: Size.M,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 13,
                    name: "Yoga Mat Bag",
                    description: "Keep your amy dry and clean",
                    fullPrice: 30m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Accessories),
                    image: _images.ByName("mat-bag-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 29,
                            stockLevel: 10,
                            priceAdjustment: 0,
                            colour: Colour.Black,
                            size: Size.M,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 14,
                    name: "Straps",
                    description: "Deepen your stretch and enhance your yoga practice",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Accessories),
                    image: _images.ByName("straps-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 30,
                            stockLevel: 5,
                            priceAdjustment: 0,
                            colour: Colour.Pink,
                            size: Size.M,
                            gender: Gender.Unisex),
                        new ProductAttributes(
                            id: 31,
                            stockLevel: 10,
                            priceAdjustment: 0,
                            colour: Colour.Blue,
                            size: Size.M,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 15,
                    name: "Blanket",
                    description: "Keep warm during Shavasana. The blanket can also be placed under your knee while practicing",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Accessories),
                    image: _images.ByName("yoga-blanket-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 32,
                            stockLevel: 15,
                            priceAdjustment: -0.5m,
                            colour: Colour.Black,
                            size: Size.M,
                            gender: Gender.Unisex),
                        new ProductAttributes(
                            id: 33,
                            stockLevel: 10,
                            priceAdjustment: 0,
                            colour: Colour.Green,
                            size: Size.M,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 16,
                    name: "Standard Yoga Meditiation Cushion",
                    description: "Get more confortable for your meditation",
                    fullPrice: 15m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Accessories),
                    image: _images.ByName("yoga-cushion-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 34,
                            stockLevel: 15,
                            priceAdjustment: -0.5m,
                            colour: Colour.Black,
                            size: Size.M,
                            gender: Gender.Unisex),
                        new ProductAttributes(
                            id: 35,
                            stockLevel: 10,
                            priceAdjustment: 0,
                            colour: Colour.Purple,
                            size: Size.M,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 17,
                    name: "Luxury Yoga Meditiation Cushion",
                    description: "Take your meditation to the next level",
                    fullPrice: 15m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Accessories),
                    image: _images.ByName("yoga-cushion-2"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 36,
                            stockLevel: 15,
                            priceAdjustment: -0.5m,
                            colour: Colour.Black,
                            size: Size.M,
                            gender: Gender.Unisex),
                        new ProductAttributes(
                            id: 37,
                            stockLevel: 10,
                            priceAdjustment: 0,
                            colour: Colour.Purple,
                            size: Size.M,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 18,
                    name: "Yoga Slippers",
                    description: "Keep your feet warm without losing the connection to the mat",
                    fullPrice: 15m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Clothing),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Footwear),
                    image: _images.ByName("yoga-slippers"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 38,
                            stockLevel: 15,
                            priceAdjustment: -0.5m,
                            colour: Colour.Black,
                            size: Size.M,
                            gender: Gender.Unisex),
                        new ProductAttributes(
                            id: 39,
                            stockLevel: 10,
                            priceAdjustment: 0,
                            colour: Colour.Black,
                            size: Size.L,
                            gender: Gender.Unisex),
                    }),
                new Product(
                    id: 19,
                    name: "Men's YogaTop",
                    description: "Move with ease on your yoga practice with this comfortable sleevelesstop. 1OO% Cotton.",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Clothing),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Tops),
                    image: _images.ByName("men-top-1"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 40,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Blue,
                            size: Size.M,
                            gender: Gender.Mens),
                        new ProductAttributes(
                            id: 41,
                            stockLevel: 30,
                            priceAdjustment: -0.6m,
                            colour: Colour.Black,
                            size: Size.XL,
                            gender: Gender.Mens),
                    }),
                new Product(
                    id: 20,
                    name: "Yoga Wheel",
                    description: "The Yoga Wheel is used to improve flexibility and strength in the back, chest, shoulders, abdomen and hip flexor.",
                    fullPrice: 20m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Accessories),
                    image: _images.ByName("yoga-wheel"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 42,
                            stockLevel: 15,
                            priceAdjustment: 0m,
                            colour: Colour.Black,
                            size: Size.M,
                            gender: Gender.Unisex),      
                    }),
                new Product(
                    id: 21,
                    name: "1kg dumbells",
                    description: "1kg dumbells for light resistance training.",
                    fullPrice: 10m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Accessories),
                    image: _images.ByName("1kg-dumbells"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 43,
                            stockLevel: 20,
                            priceAdjustment: 0m,
                            colour: Colour.Yellow,
                            size: Size.M,
                            gender: Gender.Unisex),      
                    }),
                new Product(
                    id: 22,
                    name: "Eye Pillow",
                    description: "Helps to aid relaxation, meditation and mindfulness.",
                    fullPrice: 15m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Accessories),
                    image: _images.ByName("eye-pillow"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 44,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Blue,
                            size: Size.M,
                            gender: Gender.Unisex),                             
                    }),
                new Product(
                    id: 23,
                    name: "Yoga Sports Bra",
                    description: "Technical fabric that keeps you dry",
                    fullPrice: 15m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Clothing),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Tops),
                    image: _images.ByName("yoga-sportsbra"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 45,
                            stockLevel: 10,
                            priceAdjustment: 0m,
                            colour: Colour.Black,
                            size: Size.M,
                            gender: Gender.Womens),                             
                    }),
                new Product(
                    id: 24,
                    name: "Essential Oil Yoga Mat Spray",
                    description: "A 100 ml spray you can slip in your bag to bring some nature to your practice.",
                    fullPrice: 5m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Accessories),
                    image: _images.ByName("yoga-mat-spray"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 46,
                            stockLevel: 30,
                            priceAdjustment: 0m,
                            colour: Colour.Blue,
                            size: Size.M,
                            gender: Gender.Unisex),                             
                    }),
                new Product(
                    id: 25,
                    name: "Yoga knee & wrist pad",
                    description: "This pad is for support and comfort for sensitive knees and wrists when practising yoga.",
                    fullPrice: 5m,
                    productCategory: _productCategories.ByName(Models.Enums.ProductCategory.Equipment),
                    productType: _productTypes.ByName(Models.Enums.ProductType.Accessories),
                    image: _images.ByName("yoga-knee-wrist-pad"),
                    productAttributes: new List<ProductAttributes>()
                    {
                        new ProductAttributes(
                            id: 47,
                            stockLevel: 20,
                            priceAdjustment: 0m,
                            colour: Colour.Green,
                            size: Size.M,
                            gender: Gender.Unisex),                             
                    }),
            };
        }

        private static IEnumerable<Image> SeedImages()
        {
            return new List<Image>
            {
                new Image(
                    id: 1,
                    imageName: "yoga-mat-1",
                    alt: "Image of Blue Yoga mat",
                    path: "/images/mat-1.jpg"),
                new Image(
                    id: 2,
                    imageName: "yoga-mat-2",
                    alt: "some alt",
                    path: "/images/banner-2.jpg"),
                new Image(
                    id: 3,
                    imageName: "Equipment",
                    alt: "Image of Equipment",
                    path: "/images/Equipment.jpg"),
                new Image(
                    id: 4,
                    imageName: "yoga-mat-3",
                    alt: "some alt",
                    path: "/images/banner-3.jpg"),
                new Image(
                    id: 5,
                    imageName: "Clothing",
                    alt: "Image of Clothing",
                    path: "/images/YogaClothing.jpg"),
                new Image(
                    id: 6,
                    imageName: "Multi color Mat",
                    alt: "Image of Multi Colour Yoga mat",
                    path: "TBC"),
                new Image(
                    id: 7,
                    imageName: "Bolsters",
                    alt: "Image of Bolsters",
                    path: "/images/Bolsters.jpg"),
                new Image(
                    id: 8,
                    imageName: "Mats",
                    alt: "Image of Yoga Mats",
                    path: "/images/Mats.jpg"),
                new Image(
                    id: 9,
                    imageName: "Tops",
                    alt: "Image of Yoga Tops",
                    path: "/images/tops.jpg"),
                new Image(
                    id: 10,
                    imageName: "bottoms",
                    alt: "Image of Yoga Bottoms",
                    path: "/images/bottoms.png"),
                new Image(
                    id: 11,
                    imageName: "Blocks",
                    alt: "Image of Yoga Blocks",
                    path: "/images/Blocks.jpg"),
                new Image(
                    id: 12,
                    imageName: "bolster-1",
                    alt: "Image of yoga bolster",
                    path: "/images/bolster-1.jpg"),
                new Image(
                    id: 13,
                    imageName: "halter-top",
                    alt: "Image of halter top",
                    path: "/images/halter-top.jpg"),
                new Image(
                    id: 14,
                    imageName: "mat-2",
                    alt: "Image of Yoga mat",
                    path: "/images/mat-2.jpg"),
                new Image(
                    id: 15,
                    imageName: "mat-bag-1",
                    alt: "Image of Yoga mat bag",
                    path: "/images/mat-bag-1.jpg"),
                new Image(
                    id: 16,
                    imageName: "men-top-1",
                    alt: "Image of Yoga Blocks",
                    path: "/images/men-top-1.png"),
                new Image(
                    id: 17,
                    imageName: "straps-1",
                    alt: "Image of Yoga strap",
                    path: "/images/straps-1.jpg"),
                new Image(
                    id: 18,
                    imageName: "woman-shorts-1",
                    alt: "Image of Yoga Blocks",
                    path: "/images/woman-shorts-1.jpg"),
                new Image(
                    id: 19,
                    imageName: "woman-top-long-1",
                    alt: "Image of Yoga Blocks",
                    path: "/images/woman-top-long-1.jpg"),
                new Image(
                    id: 20,
                    imageName: "yoga-accessories",
                    alt: "Image of Yoga Blocks",
                    path: "/images/yoga-accessories.jpg"),
                new Image(
                    id: 21,
                    imageName: "yoga-blanket-1",
                    alt: "Image of Yoga Blocks",
                    path: "/images/yoga-blanket-1.jpg"),
                new Image(
                    id: 22,
                    imageName: "yoga-cushion-1",
                    alt: "Image of Yoga Blocks",
                    path: "/images/yoga-cushion-1.jpg"),
                new Image(
                    id: 23,
                    imageName: "yoga-cushion-2",
                    alt: "Image of Yoga Blocks",
                    path: "/images/yoga-cushion-2.jpg"),
                new Image(
                    id: 24,
                    imageName: "yoga-slippers",
                    alt: "Image of Yoga Blocks",
                    path: "/images/yoga-slippers.jpg"),
                new Image(
                    id: 25,
                    imageName: "women-1",
                    alt: "Image of Yoga Blocks",
                    path: "/images/women-1.jpg"),
                new Image(
                    id: 26,
                    imageName: "mat-1",
                    alt: "Image of Yoga Blocks",
                    path: "/images/mat-1.jpg"),
                new Image(
                    id: 27,
                    imageName: "mat-2",
                    alt: "Image of Yoga Blocks",
                    path: "/images/mat-2.jpg"),
                new Image(
                    id: 28,
                    imageName: "man-leggings-1",
                    alt: "Image of Yoga Blocks",
                    path: "/images/man-leggings-1.jpg"),
                new Image(
                    id: 29,
                    imageName: "man-shorts-1",
                    alt: "Image of Yoga Blocks",
                    path: "/images/man-shorts-1.jpg"),
                new Image(
                    id: 30,
                    imageName: "bolster-2",
                    alt: "Image of Yoga Bolster",
                    path: "/images/bolster-2.jpg"),
                new Image(
                    id: 31,
                    imageName: "yoga-wheel",
                    alt: "Image of Yoga Wheel",
                    path: "/images/yoga-wheel.jpg"),
                new Image(
                    id: 32,
                    imageName: "1kg-dumbells",
                    alt: "Image of 1kg dumbells",
                    path: "/images/1kg-dumbells.jpg"),
                new Image(
                    id: 33,
                    imageName: "eye-pillow",
                    alt: "Image of eye pillow",
                    path: "/images/eye-pillow.jpg"),
                new Image(
                    id: 34,
                    imageName: "yoga-sportsbra",
                    alt: "Image of Yoga Sports Bra",
                    path: "/images/yoga-sportsbra.jpg"),
                new Image(
                    id: 35,
                    imageName: "yoga-mat-spray",
                    alt: "Image of Yoga Mat Spray",
                    path: "/images/yoga-mat-spray.jpg"),
                new Image(
                    id: 36,
                    imageName: "yoga-knee-wrist-pad",
                    alt: "Image of Yoga knee and wirst pad",
                    path: "/images/yoga-knee-wrist-pad.jpg"),
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
                    image: _images!.ByName("Equipment")),
                new Models.ProductCategory(
                    id: 2,
                    productCategoryName: "Clothing",
                    description: "Our Selection of Clothing",
                    image: _images!.ByName("Clothing")),
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
                    image: _images!.ByName("Mats")),
                new Models.ProductType(
                    id: 2,
                    productTypeName: "Blocks",
                    description: "Our Selection of Blocks",
                    image: _images!.ByName("Blocks")),
                new Models.ProductType(
                    id: 3,
                    productTypeName: "Bolsters",
                    description: "Our Selection of Bolsters",
                    image: _images!.ByName("Bolsters")),
               new Models.ProductType(
                    id: 4,
                    productTypeName: "Tops",
                    description: "Our Selection of Yoga Tops",
                    image: _images!.ByName("Tops")),
               new Models.ProductType(
                    id: 5,
                    productTypeName: "Bottoms",
                    description: "Our Selection of Yoga Leggings",
                    image: _images!.ByName("bottoms")),
               new Models.ProductType(
                    id: 6,
                    productTypeName: "Accessories",
                    description: "Our Selection of Yoga Accessories",
                    image: _images!.ByName("yoga-accessories")),
               new Models.ProductType(
                    id: 7,
                    productTypeName: "Footwear",
                    description: "Our Selection of Yoga Footwear",
                    image: _images!.ByName("yoga-slippers")),

            };
        }

        private static IEnumerable<Customer> SeedCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    IdentityUserName = "user",
                    IsActive = true,
                    IsGdpr = true,
                    CustomerDetail = _customerDetails.ById(1)
                }
            };
        }
        
        private static IEnumerable<IdentityUser> SeedUsers()
        {
            
            var passwordHasher = new PasswordHasher<object>();
            var hashedPassword = passwordHasher.HashPassword(null, "Testing123!");
            
            return new List<IdentityUser>
            {
                
                new IdentityUser()
                {
                    Email = "admin@email.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    PasswordHash = hashedPassword
                },
                new IdentityUser()
                {
                    Email = "user@email.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user",
                    PasswordHash = hashedPassword
                }
            };
        }
        
        public static async Task SeedRoles()
        {
            // Create the "Admin" role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                await _roleManager.CreateAsync(adminRole);
            }

            // Create the "User" role if it doesn't exist
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                var userRole = new IdentityRole("User");
                await _roleManager.CreateAsync(userRole);
            }
        }
        
        
        public static async Task CreateUsers()
        {
            foreach (var user in _users)
            {
                var result  = await _userManager.CreateAsync(user, "Testing123!");

                if (result.Succeeded)
                {
                    // User created successfully

                    if (user.UserName == "admin")
                    {
                        await _userManager.AddToRolesAsync(user, new[] { "User", "Admin" });
                    }
                    else
                    {
                        await _userManager.AddToRolesAsync(user, new[] { "User" });  
                    }
                    
                    // Add roles to the user
                }
                else
                {
                    // Failed to create the user
                    // Handle the error or log it
                }
            }
        }

        private static IEnumerable<CustomerDetail> SeedCustomerDetails()
        {
            return new List<CustomerDetail>
            {
                new CustomerDetail
                {
                    Id = 1,
                    Email = "user@email.com",
                    FirstName = "John",
                    Surname = "Doe",
                    PhoneNo = "1234567890",
                    Addresses = new List<AddressDetail>
                    {
                        _addressDetails.ById(1)
                    }
                },
                new CustomerDetail
                {
                    Id = 2,
                    Email = "admin@email.com",
                    FirstName = "Admin",
                    Surname = "User",
                    PhoneNo = "1234567890",
                    Addresses = new List<AddressDetail>
                    {
                        _addressDetails.ById(2)
                    }
                }
            };
        }

        private static IEnumerable<AddressDetail> SeedAddressDetails()
        {
            return new List<AddressDetail>
            {
                new AddressDetail
                {
                    Id = 1,
                    StreetAddr = "10 Some Street",
                    City = "Some City",
                    AddrLine2 = "",
                    County = "Dublin",
                    Country = "Ireland",
                    PostCode = "DA13 44F",
                },
                new AddressDetail
                {
                    Id = 2,
                    StreetAddr = "12 Some Street",
                    City = "Other city",
                    AddrLine2 = "",
                    County = "Dublin",
                    Country = "Ireland",
                    PostCode = "DA13 44F",
                }
            };
        }

        private static IEnumerable<Order> SeedOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    ShippingAddressId = 1,
                    DateOfSale = DateTime.Now,
                    IsPaid = false,
                    OrderTotal = 35.50m,
                    Items = new List<BasketItem>
                    {
                        new BasketItem
                        {
                            CustomerId = 1,
                            ProductId = 1,  
                            ProductAttributeId = 1,
                            Quantity = 2,
                            TotalCost = 35.50m,
                        }
                    }
                }
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
        private static CustomerDetail ById(this IEnumerable<CustomerDetail> customerDetails, int id)
        {
            return customerDetails.FirstOrDefault(i => i.Id == id) ??
                throw new NullReferenceException($"Invalid CustomerDetail Id");
        }

        private static AddressDetail ById(this IEnumerable<AddressDetail> addresses, int id)
        {
            return addresses.FirstOrDefault(i => i.Id == id) ??
                throw new NullReferenceException($"Invalid Address Id");
        }

        private static Customer ById(this IEnumerable<Customer> customers, int id) 
        {
            return customers.FirstOrDefault(i => i.Id == id) ??
                 throw new NullReferenceException($"Invalid Customer Id");
        }
    }
}
