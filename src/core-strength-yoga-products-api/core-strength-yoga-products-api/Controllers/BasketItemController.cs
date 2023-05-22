using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Extensions;
using core_strength_yoga_products_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace core_strength_yoga_products_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private readonly ILogger<BasketItemController> _logger;
        private readonly CoreStrengthYogaProductsApiDbContext _context;
        public BasketItemController(ILogger<BasketItemController> logger, CoreStrengthYogaProductsApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }
       
        [HttpPost("CalculateItemTotalCost")]
        public ActionResult<BasketItem> CalculateItemTotalCost(BasketItem item)
        {
            var product = _context.Products
                .IncludeAllRelated()
                .FirstOrDefault(p => p.Id == item.ProductId);

            if(product == null)
            {
                return Problem($"Invalid Product Id={item.ProductId}");
            }

            var selectedAttribute = product.ProductAttributes
                .FirstOrDefault(pa => pa.Id == item.ProductAttributeId);

            if (selectedAttribute == null)
            {
                return Problem($"Invalid Product Attribute Id={item.ProductAttributeId}");
            }

            var totalCost = BasketItem.CalculateTotalItemCost(
                product.FullPrice,
                selectedAttribute.PriceAdjustment,
                item.Quantity);

            item.TotalCost = totalCost;

            return item;
        }

        [HttpPost("CalculateTotalBasketCost")]
        public ActionResult<decimal> CalculateTotalBasketCost(IEnumerable<BasketItem> basketItems) 
        {
            decimal totalBasketCost = 0;
            foreach (var basketItem in basketItems)
            {
                if (basketItem.Quantity > 0) 
                {
                    if (basketItem.TotalCost == 0)
                    {

                    }
                }
                totalBasketCost += basketItem.TotalCost;
            }
            return totalBasketCost;
        }
    }
}
