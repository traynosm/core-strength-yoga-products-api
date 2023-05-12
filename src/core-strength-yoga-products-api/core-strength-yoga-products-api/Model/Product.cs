using core_strength_yoga_products_api.Model;
using System.ComponentModel.DataAnnotations;

namespace core_strength_yoga_products_api.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal FullPrice { get; set; }
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
        public virtual ICollection<ProductAttributes> ProductAttributes { get; set; }

        public Product() { }
        public Product(int id, string name, string description, decimal fullPrice) 
        {
            Id = id;
            Name = name;
            Description = description;
            FullPrice = fullPrice;
        }
    }
}
