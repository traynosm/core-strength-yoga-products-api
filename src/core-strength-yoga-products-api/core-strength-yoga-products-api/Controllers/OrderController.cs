using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Extensions;
using core_strength_yoga_products_api.Migrations;
using core_strength_yoga_products_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet("GetByUsername/{username}")]
        public ActionResult<IEnumerable<Order>> GetByUsername(String username)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.IdentityUserName == username);

            if (customer == null) return NotFound();


            var orders = _context.Orders.Where(o => o.CustomerId == customer.Id)
                .Include(p => p.Items)
                .Include(p => p.Customer)
                .Include(p => p.Customer.CustomerDetail)
                .Include(p => p.Customer.CustomerDetail.Addresses);
                //.FirstOrDefaultAsync(o => o.Id == id);*/

            if (orders == null)
            {
                return NotFound();
            }

            return orders.Any() ? orders.ToList() : new List<Order>();
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

            if (!order.Customer.CustomerDetail.Addresses.IsNullOrEmpty())
            {
                var firstAddress = order.Customer.CustomerDetail.Addresses.First();
                order.ShippingAddressId = firstAddress.Id; 
            }
            

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return order;     
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
        //UPDATE AN EXISTING ORDER
    }
}
