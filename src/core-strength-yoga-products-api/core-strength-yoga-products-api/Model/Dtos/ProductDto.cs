using core_strength_yoga_products_api.Models;

namespace core_strength_yoga_products_api.Model.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public ProductCategoryDto ProductCategory { get; set; }
        public ProductTypeDto ProductType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal FullPrice { get; set; }
        public ImageDto Image { get; set; }
        public IEnumerable<ProductAttributesDto> ProductAttributes { get; set; }

        public static ProductDto Resolve(Product product)
        {
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                ProductCategory = ProductCategoryDto.Resolve(product.ProductCategory),
                ProductType = ProductTypeDto.Resolve(product.ProductType),
                ProductAttributes = ProductAttributesDto.Resolve(product.ProductAttributes),
                FullPrice = product.FullPrice,
                Description = product.Description,
                Image = ImageDto.Resolve(product.Image)
            };

            return productDto;
        }

        public static Product Resolve(ProductDto productDto)
        {            
            var product = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                ProductCategoryId = productDto.ProductCategory.Id,
                ProductCategory = ProductCategoryDto.Resolve(productDto.ProductCategory),
                ProductTypeId = productDto.ProductType.Id,
                ProductType = ProductTypeDto.Resolve(productDto.ProductType),
                ProductAttributes = ProductAttributesDto.Resolve(productDto.ProductAttributes),
                FullPrice = productDto.FullPrice,
                Description = productDto.Description,
                Image = ImageDto.Resolve(productDto.Image),
                ImageId = productDto.Image.Id,
            };

            return product;
        }
    }
}
