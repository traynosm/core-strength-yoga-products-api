using core_strength_yoga_products_api.Data.Contexts;
using core_strength_yoga_products_api.Model.Dtos;
using core_strength_yoga_products_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;

namespace core_strength_yoga_products_api.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
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
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null) return NotFound();

            var dto = ProductDto.Resolve(product);  
            
            return dto;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("ByCategory/{id}")]
        public ActionResult<IEnumerable<ProductDto>> ByProductCategory(int id)
        {
            var products = _context.Products.Where(p => p.ProductCategoryId == id);

            if (products == null) return NotFound();

           var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                productDtos.Add(ProductDto.Resolve(product));
            }

            return productDtos;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("ByType/{id}")]
        public ActionResult<IEnumerable<ProductDto>> ByProductType(int id)
        {
            var products = _context.Products.Where(p => p.ProductTypeId == id);

            if (products == null) return NotFound();

            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                productDtos.Add(ProductDto.Resolve(product));
            }

            return productDtos;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("FilterOnAttribute/ProductCategory={categoryId}/Colour={colourId}/Size={sizeId}/Gender={genderId}")]
        public ActionResult<IEnumerable<ProductDto>> FilterOnAttribute([FromUri] int categoryId = 0, int colourId = 0, int sizeId = 0, int genderId = 0)
        {
            var products = categoryId > 0 ? 
                _context.Products.Where(p => p.ProductCategoryId == categoryId).ToList() :
                _context.Products.ToList();

            if (products == null) return NotFound();

            products = colourId > 0 ?
                products.SelectMany(p => p.ProductAttributes)
                    .Where(p => (int)p.Colour == colourId)
                    .SelectMany(pa => pa.Products).ToList() :
                products;

            products = sizeId > 0 ?
                products.SelectMany(p => p.ProductAttributes)
                    .Where(p => (int)p.Size == sizeId)
                    .SelectMany(pa => pa.Products).ToList() :
                products;

            products = genderId > 0 ?
                products.SelectMany(p => p.ProductAttributes)
                    .Where(p => (int)p.Gender == genderId)
                    .SelectMany(pa => pa.Products).ToList() :
                products;

            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                productDtos.Add(ProductDto.Resolve(product));
            }

            return productDtos;
        }

        [Microsoft.AspNetCore.Mvc.HttpPut()]
        public async Task<IActionResult> Put(ProductDto productDto)
        {
            var productToUpdate = ProductDto.Resolve(productDto);

            var savedProduct = _context.Products.Find(productDto.Id);

            if(savedProduct != null)
            {
                RedirectToAction("Post", new { productDto = productDto});
            }

            UpdateProductAttributes(productToUpdate, savedProduct); 
            UpdateProductCategory(productToUpdate, savedProduct);
            UpdateProductType(productToUpdate, savedProduct);   
            UpdateImage(productToUpdate, savedProduct);

            _context.Entry(savedProduct).CurrentValues.SetValues(productToUpdate);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException concurrencyException)
            {
                if (!_context.Products.Any(p => p.Id == productDto.Id))
                {
                    return NotFound();
                }
            }
            catch(Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return NoContent();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult<ProductDto>> Post(ProductDto productDto)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'DbContext.Movies' is null.");
            }
            var product = ProductDto.Resolve(productDto);

            if(_context.Products.Any(p => p.Name == product.Name))
            {
                return Problem($"Product with name={product.Name} already exists!");
            }

            if(product.ProductTypeId > 0)
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

            foreach(var productAttribute in product.ProductAttributes)
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
                var savedProductAttribute = savedProduct.ProductAttributes.SingleOrDefault(pa => pa.Id == productAttributeToUpdate.Id);

                if (savedProductAttribute != null)
                {
                    _context.Entry(savedProductAttribute).CurrentValues.SetValues(productAttributeToUpdate);
                }
                else
                {
                    productAttributeToUpdate.Products = new List<Product> { savedProduct };
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