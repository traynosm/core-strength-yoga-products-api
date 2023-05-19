﻿using core_strength_yoga_products_api.Data.Contexts;
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
        private static IEnumerable<Customer> _customers;
        private static IEnumerable<CustomerDetail> _customerDetails;
        private static IEnumerable<AddressDetail> _addressDetails;
        private static IEnumerable<Order> _orders;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _images = SeedImages();
            _productCategories = SeedProductCategories();
            _productTypes = SeedProductTypes();
            _products = SeedProducts();
            _addressDetails = SeedAddressDetails();
            _customerDetails = SeedCustomerDetails();
            _customers = SeedCustomers();
            _orders = SeedOrders();

            using (var context = new CoreStrengthYogaProductsApiDbContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<CoreStrengthYogaProductsApiDbContext>>()))
            {
                context.Database.EnsureCreated();

                if(context.Images.Any()){
                    Console.WriteLine("The Images database contains data and cannot be seeded");
                }else{
                    foreach (var image in _images)
                    {
                        context.Images.Add(image);
                    }
                }

                if(context.ProductCategories.Any()){
                    Console.WriteLine("The Product Catagories database contains data and cannot be seeded");
                }else{
                    foreach (var category in _productCategories)
                    {
                        context.ProductCategories.Add(category);
                    }
                }

                if (context.ProductTypes.Any())
                {
                    Console.WriteLine("The ProductTypes database contains data and cannot be seeded");
                }
                else
                {
                    foreach (var productType in _productTypes)
                    {
                        context.ProductTypes.Add(productType);
                    }
                }

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
                    alt: "Image of Blue Yoga mat",
                    path: "TBC"),
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
                    imageName: "Bottoms",
                    alt: "Image of Yoga Bottoms",
                    path: "/images/bottoms.png"),
                new Image(
                    id: 11,
                    imageName: "Blocks",
                    alt: "Image of Yoga Bottoms",
                    path: "/images/Blocks.jpg"),
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
                    productTypeName: "Yoga Tops",
                    description: "Our Selection of Yoga Tops",
                    image: _images!.ByName("Tops")),
               new Models.ProductType(
                    id: 5,
                    productTypeName: "Yoga Leggings",
                    description: "Our Selection of Yoga Leggings",
                    image: _images!.ByName("Bottoms")),
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
                    IdentityUserName = "my_username",
                    IsActive = true,
                    IsGdpr = true,
                    CustomerDetail = _customerDetails.ById(1)
                }
            };
        }

        private static IEnumerable<CustomerDetail> SeedCustomerDetails()
        {
            return new List<CustomerDetail>
            {
                new CustomerDetail
                {
                    Id = 1,
                    Email = "some_email@email.com",
                    FirstName = "John",
                    Surname = "Doe",
                    PhoneNo = "1234567890",
                    Addresses = new List<AddressDetail>
                    {
                        _addressDetails.ById(1)
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
                    IsPaid = true,
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
