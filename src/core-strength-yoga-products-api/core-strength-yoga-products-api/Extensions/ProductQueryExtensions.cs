using core_strength_yoga_products_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace core_strength_yoga_products_api.Extensions
{
    public static class ProductQueryExtensions
    {
        public static IIncludableQueryable<Product, ICollection<ProductAttributes>> IncludeAllRelated(this DbSet<Product> set)
        {
            return set
                .Include(p => p.Image)
                .Include(p => p.ProductType)
                .Include(p => p.ProductType.Image)
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductCategory.Image)
                .Include(p => p.ProductAttributes);
        }

        public static List<Product> SelectOnCategory(this DbSet<Product> set, int id)
        {
            return id > 0 ?
                set.IncludeAllRelated()
                   .Where(p => p.ProductCategoryId == id)
                   .ToList() :
                set.IncludeAllRelated()
                .ToList();
        }

        public static List<Product> SelectOnType(this DbSet<Product> set, int id)
        {
            return id > 0 ?
                set.IncludeAllRelated()
                   .Where(p => p.ProductTypeId == id)
                   .ToList() :
                set.IncludeAllRelated()
                .ToList();
        }

        public static List<Product> SelectOnColourAttribute(this List<Product> products, DbSet<Product> set, int id) 
        {
            var ids = products
                 .SelectMany(p => p.ProductAttributes)
                 .Where(p => (int)p.Colour == id)
                 .Select(pa => pa.ProductId).ToList();

            return id > 0 && products.Any() ? set
             .IncludeAllRelated()
             .Where(p => ids.Contains(p.Id))
                 .ToList():
             products;
        }

        public static List<Product> SelectOnSizeAttribute(this List<Product> products, DbSet<Product> set, int id)
        {
            var ids = products
                  .SelectMany(p => p.ProductAttributes)
                  .Where(p => (int)p.Size == id)
                  .Select(pa => pa.ProductId).ToList();

            return id > 0 && products.Any() ? set
             .IncludeAllRelated()
             .Where(p => ids.Contains(p.Id))
                 .ToList() :
             products;
        }

        public static List<Product> SelectOnGenderAttribute(this List<Product> products, DbSet<Product> set, int id)
        {
            var ids = products
               .SelectMany(p => p.ProductAttributes)
               .Where(p => (int)p.Gender == id)
               .Select(pa => pa.ProductId).ToList();

            return id > 0 && products.Any() ? set
             .IncludeAllRelated()
             .Where(p => ids.Contains(p.Id))
                 .ToList() :
             products;
        }
    }
}
