using core_strength_yoga_products_api.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core_strength_yoga_products_api.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public virtual IEnumerable<BasketItem> Items { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime? DateOfSale { get; set; }
        public decimal OrderTotal { get; set; }
        public bool IsPaid { get; set; }
        public int ShippingAddressId { get; set; }

        public Order() { }

        public Order (int id, DateTime? dateOfSale, decimal orderTotal, int customerId, bool isPaid)
        {
            Id = id;
            DateOfSale = dateOfSale;
            OrderTotal = orderTotal;
            CustomerId = customerId;  
            IsPaid = isPaid;    
        }
    }
}
