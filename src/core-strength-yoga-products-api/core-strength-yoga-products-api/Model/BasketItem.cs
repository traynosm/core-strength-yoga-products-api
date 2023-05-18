using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace core_strength_yoga_products_api.Model
{
    [PrimaryKey(nameof(OrderId), nameof(CustomerId), nameof(ProductId), nameof(ProductAttributeId))]
    public class BasketItem
    {
        [ForeignKey(nameof(OrderId))]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }

        public BasketItem() { }

        public static decimal CalculateTotalItemCost(decimal fullPrice, decimal priceAdjustment, int qty)
        {
            return qty * (fullPrice + priceAdjustment);
        }
    }
}
