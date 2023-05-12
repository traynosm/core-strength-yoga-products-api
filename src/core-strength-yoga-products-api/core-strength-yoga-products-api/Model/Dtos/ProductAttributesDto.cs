using core_strength_yoga_products_api.Interfaces;
using core_strength_yoga_products_api.Models;

namespace core_strength_yoga_products_api.Model.Dtos
{
    public class ProductAttributesDto 
    {
        public int Id { get; set; }
        public int StockLevel { get; set; }
        public decimal PriceAdjustment { get; set; }
        public Models.Enums.Colour Colour { get; set; }
        public Models.Enums.Size Size { get; set; }
        public Models.Enums.Gender Gender { get; set; }

        public static IEnumerable<ProductAttributesDto> Resolve(IEnumerable<ProductAttributes> src)
        {
            var attributes = new List<ProductAttributesDto>();

            foreach(var attribute in src)
            {
                attributes.Add(new ProductAttributesDto
                {
                    Id = attribute.Id,
                    StockLevel = attribute.StockLevel,
                    PriceAdjustment = attribute.PriceAdjustment,
                    Colour = attribute.Colour,
                    Size = attribute.Size,
                    Gender = attribute.Gender
                });
            }

            return attributes;
        }


        public static ICollection<ProductAttributes> Resolve(IEnumerable<ProductAttributesDto> src)
        {
            var attributes = new List<ProductAttributes>();

            foreach (var attribute in src)
            {
                attributes.Add(new ProductAttributes
                {
                    Id = attribute.Id,
                    StockLevel = attribute.StockLevel,
                    PriceAdjustment = attribute.PriceAdjustment,
                    Colour = attribute.Colour,
                    Size = attribute.Size,
                    Gender = attribute.Gender,  
                });
            }

            return attributes;
        }
    }
}
