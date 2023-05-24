using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Extensions;
using core_strength_yoga_products_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly CoreStrengthYogaProductsApiDbContext _context;

        public ProductTypesController(ILogger<ProductsController> logger, CoreStrengthYogaProductsApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> Get()
        {
            return await _context.ProductTypes
                .Include(pt => pt.Image)
                .ToListAsync();
        }

        [HttpGet("ByCategoryId/{id}")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetByCategoryId(int id)
        {
            var products = _context.Products
                .IncludeAllRelated()
                .Where(p => p.ProductCategoryId == id);

            var productTypes = products.Select(p => p.ProductType).Distinct();

            return productTypes.ToList();
        }
    }
}