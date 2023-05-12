using core_strength_yoga_products_api.Models;

namespace core_strength_yoga_products_api.Model.Dtos
{
    public class ProductTypeDto
    {
        public int Id { get; set; }
        public string ProductTypeName { get; set; }
        public string Description { get; set; }
        public ImageDto Image { get; set; }

        public static ProductTypeDto Resolve(ProductType productType)
        {
            return new ProductTypeDto
            {
                Id = productType.Id,
                ProductTypeName = productType.ProductTypeName,
                Description = productType.Description,
                Image = ImageDto.Resolve(productType.Image),
            };
        }

        public static ProductType Resolve(ProductTypeDto productTypeDto)
        {
            return new ProductType
            {
                Id = productTypeDto.Id,
                ProductTypeName = productTypeDto.ProductTypeName,
                Description = productTypeDto.Description,
                Image = ImageDto.Resolve(productTypeDto.Image),
                ImageId = productTypeDto.Image.Id
            };
        }
    }
}
