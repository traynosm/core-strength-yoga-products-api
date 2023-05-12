using System.ComponentModel.DataAnnotations;

namespace core_strength_yoga_products_api.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime? DateOfSale { get; set; }
        public decimal OrderTotal { get; set; }

        public Order (int id, DateTime? dateOfSale, decimal orderTotal, int productId, int customerId)
        {
            Id = id;
            DateOfSale = dateOfSale;
            OrderTotal = orderTotal;
            ProductId = productId;
            CustomerId = customerId;  
        }
    }
}
