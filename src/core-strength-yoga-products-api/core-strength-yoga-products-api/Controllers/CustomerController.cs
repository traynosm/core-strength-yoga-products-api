using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Extensions;
using core_strength_yoga_products_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CoreStrengthYogaProductsApiDbContext _context;

        public CustomerController(ILogger<CustomerController> logger, CoreStrengthYogaProductsApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await _context.Customers
                .IncludeAllRelated()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (customer == null) return NotFound();

            return customer;
        }

        [HttpGet("GetByUserName/{identityUserName}")]
        public async Task<ActionResult<Customer>> GetByUserName(string identityUserName)
        {
            var customer = await _context.Customers
                .IncludeAllRelated()
                .FirstOrDefaultAsync(p => p.IdentityUserName == identityUserName);

            if (customer == null) return NotFound();

            return customer;
        }

        //POST NEW CUSTOMER ON REGISTRATION

        //UPDATE CUSTOMER DETAILS
    }
}
