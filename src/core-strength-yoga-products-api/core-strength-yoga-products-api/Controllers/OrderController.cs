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

        //UPDATE AN EXISTING ORDER

    }
}
