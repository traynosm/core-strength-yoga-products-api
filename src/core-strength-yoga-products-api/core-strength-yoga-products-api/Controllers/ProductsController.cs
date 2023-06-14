using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Extensions;
using core_strength_yoga_products_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;

namespace core_strength_yoga_products_api.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly CoreStrengthYogaProductsApiDbContext _context;

        public ProductsController(ILogger<ProductsController> logger, CoreStrengthYogaProductsApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _context.Products
                .IncludeAllRelated()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound(); 
            
            return product;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet()]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _context.Products
                .IncludeAllRelated()
                .ToListAsync();

            if (products == null) return NotFound();

            return products;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("ByCategory/{id}")]
        public ActionResult<IEnumerable<Product>> ByProductCategory(int id)
        {
            var products = _context.Products.SelectOnCategory(id);

            if (products == null) return NotFound();

            return products.ToList();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("ByType/{id}")]
        public ActionResult<IEnumerable<Product>> ByProductType(int id)
        {
            var products = _context.Products.SelectOnType(id);

            if (products == null) return NotFound();

            return products.ToList();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet(
            "FilterOnAttribute/ProductCategory={categoryId}/ProductType={productTypeId}/Colour={colourId}/Size={sizeId}/Gender={genderId}")]
        public ActionResult<IEnumerable<Product>> FilterOnAttribute(
            [FromUri] int categoryId = 0, int productTypeId = 0, int colourId = 0, int sizeId = 0, int genderId = 0)
        {
            var products = _context.Products.SelectOnCategory(categoryId);


            if (products == null) return NotFound();

            products = products
                .SelectOnType(_context.Products, productTypeId)
                .SelectOnColourAttribute(_context.Products, colourId)
                .SelectOnSizeAttribute(_context.Products, sizeId)
                .SelectOnGenderAttribute(_context.Products, genderId);

            return products.Any() ? products.ToList() : new List<Product>();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("Search/{query}")]
        public ActionResult<IEnumerable<Product>> Search([FromUri] string query)
        {
            var products = _context.Products
                .IncludeAllRelated()
                .Where(p => 
                    p.Name.ToLower().Contains(query) ||
                    p.ProductCategory.ProductCategoryName.ToLower().Contains(query.ToLower()) ||
                    p.ProductType.ProductTypeName.ToLower().Contains(query.ToLower()));

            if (products == null) return NotFound();

            return products.Any() ? products.ToList() : new List<Product>();
        }

        [Microsoft.AspNetCore.Mvc.HttpPut()]
        public async Task<ActionResult<Product>> Put(Product productToUpdate)
        {
            if(productToUpdate.Id == 0)
            {
                return NotFound();
            }
            
            var savedProduct = _context.Products
                .IncludeAllRelated()
                .FirstOrDefault(p => p.Id == productToUpdate.Id);

            if(savedProduct != null)
            {
                RedirectToAction("Post", new { productToUpdate });
            }

            UpdateProductAttributes(productToUpdate, savedProduct!); 
            UpdateProductCategory(productToUpdate, savedProduct!);
            UpdateProductType(productToUpdate, savedProduct!);   
            UpdateImage(productToUpdate, savedProduct!);

            _context.Entry(savedProduct!).CurrentValues.SetValues(productToUpdate);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(p => p.Id == productToUpdate.Id))
                {
                    return NotFound();
                }
            }
            catch(Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            
            return productToUpdate;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'DbContext.Products' is null.");
            }

            if(_context.Products.Any(p => p.Name == product.Name))
            {
                return Problem($"Product with name='{product.Name}' already exists!");
            }

            if (product.ProductTypeId > 0)
            {
                _context.ProductTypes.Attach(product.ProductType);
            }

            if (product.ProductCategoryId > 0)
            {
                _context.ProductCategories.Attach(product.ProductCategory);
            }

            if (product.ImageId > 0)
            {
                _context.Images.Attach(product.Image);
            }

            foreach (var productAttribute in product.ProductAttributes)
            {
                if(productAttribute.Id > 0)
                {
                    _context.ProductAttributes.Attach(productAttribute);
                }
            }

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return RedirectToAction($"Get", new { product.Id });
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var movie = await _context.Products.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Products.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private void UpdateProductAttributes(Product productToUpdate, Product savedProduct)
        {
            foreach (var productAttributeToUpdate in productToUpdate.ProductAttributes)
            {
                var savedProductAttribute = savedProduct.ProductAttributes.SingleOrDefault(
                    pa => pa.Id == productAttributeToUpdate.Id);

                if (savedProductAttribute != null)
                {
                    _context.Entry(savedProductAttribute).CurrentValues.SetValues(productAttributeToUpdate);
                }
                else
                {
                    _context.ProductAttributes.Add(productAttributeToUpdate);
                }
            }
        }

        private void UpdateProductCategory(Product productToUpdate, Product savedProduct)
        {
             _context.Entry(savedProduct.ProductCategory).CurrentValues
                .SetValues(productToUpdate.ProductCategory);       
        }

        private void UpdateProductType(Product productToUpdate, Product savedProduct)
        {
            _context.Entry(savedProduct.ProductType).CurrentValues
               .SetValues(productToUpdate.ProductType);
        }

        private void UpdateImage(Product productToUpdate, Product savedProduct)
        {
            _context.Entry(savedProduct.Image).CurrentValues
               .SetValues(productToUpdate.Image);
        }
    }
}