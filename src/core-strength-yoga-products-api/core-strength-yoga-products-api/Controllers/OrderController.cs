using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly CoreStrengthYogaProductsApiDbContext _context;
        public OrderController(ILogger<OrderController> logger, CoreStrengthYogaProductsApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]  
        public async Task<ActionResult<Order>> Get(int id)
        {
            var order = await _context.Orders
                .Include(p => p.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if(order == null)
            {
                return NotFound();
            }

            return order;
        }


        //POST A NEW ORDER
        //[Microsoft.AspNetCore.Mvc.HttpPost]
        //public async Task<ActionResult<Order>> Post(Order order)
        //{
        //    if (_context.Orders == null)
        //    {
        //        return Problem("Entity set 'DbContext.Orders' is null.");
        //    }

        //    if (_context.Orders.Any(p => p.Name == product.Name))
        //    {
        //        return Problem($"Product with name='{product.Name}' already exists!");
        //    }

        //    if (product.ProductTypeId > 0)
        //    {
        //        _context.ProductTypes.Attach(product.ProductType);
        //    }

        //    if (product.ProductCategoryId > 0)
        //    {
        //        _context.ProductCategories.Attach(product.ProductCategory);
        //    }

        //    if (product.ImageId > 0)
        //    {
        //        _context.Images.Attach(product.Image);
        //    }

        //    foreach (var productAttribute in product.ProductAttributes)
        //    {
        //        if (productAttribute.Id > 0)
        //        {
        //            _context.ProductAttributes.Attach(productAttribute);
        //        }
        //    }

        //    _context.Products.Add(product);

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction($"Get", new { product.Id });
        //}

        //UPDATE AN EXISTING ORDER

    }
}
