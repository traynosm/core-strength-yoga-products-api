using core_strength_yoga_products_api.Model;
using System.ComponentModel.DataAnnotations;

namespace core_strength_yoga_products_api.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        public string ProductCategoryName { get; set; }
        public string Description { get; set; }
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }

        public ProductCategory() { }    
        public ProductCategory(int id, string productCategoryName, string description)
        {
            Id = id;
            ProductCategoryName = productCategoryName;
            Description = description;
        }
        public ProductCategory(int id, string productCategoryName, string description, Image image)
        {
            Id = id;
            ProductCategoryName = productCategoryName;
            Description = description;
            Image = image;
        }
    }
    
}
