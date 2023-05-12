using core_strength_yoga_products_api.Models;

namespace core_strength_yoga_products_api.Model.Dtos
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string ProductCategoryName { get; set; }
        public string Description { get; set; }
        public ImageDto Image { get; set; }

        public static ProductCategoryDto Resolve(ProductCategory productCategory)
        {
            return new ProductCategoryDto
            {
                Id = productCategory.Id,
                ProductCategoryName = productCategory.ProductCategoryName,
                Description = productCategory.Description,
                Image = ImageDto.Resolve(productCategory.Image),
            };
        }

        public static ProductCategory Resolve(ProductCategoryDto productCategoryDto)
        {
            return new ProductCategory
            {
                Id = productCategoryDto.Id,
                ProductCategoryName = productCategoryDto.ProductCategoryName,
                Description = productCategoryDto.Description,
                Image = ImageDto.Resolve(productCategoryDto.Image),
                ImageId = productCategoryDto.Image.Id
            };
        }
    }
}
