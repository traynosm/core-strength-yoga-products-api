using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Migrations;
using core_strength_yoga_products_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_api.Controllers
{
    [Route("api/v1/[controller]")]
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
                .Include(p => p.Customer)
                .Include(p => p.Customer.CustomerDetail)
                .Include(p => p.Customer.CustomerDetail.Addresses)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }


        //POST A NEW ORDER

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'DbContext.Orders' is null.");
            }
            if (order.CustomerId > 0)
            {
                _context.Customers.Attach(order.Customer);
            }
            order.IsPaid = false;

            var firstAddress = order.Customer.CustomerDetail.Addresses.First();
            order.ShippingAddressId = firstAddress.Id;

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return order;     
        }
        //UPDATE AN EXISTING ORDER
    }
}
