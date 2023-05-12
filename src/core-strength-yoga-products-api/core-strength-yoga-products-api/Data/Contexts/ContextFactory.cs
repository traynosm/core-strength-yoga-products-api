using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_api.Data.Contexts
{
    public class CoreStrengthYogaProductsApiDbContextFactory : IDesignTimeDbContextFactory<CoreStrengthYogaProductsApiDbContext>
    {  
        public CoreStrengthYogaProductsApiDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CoreStrengthYogaProductsApiDbContext>();
                optionsBuilder.UseSqlite("Data Source=core-strength-yoga-products-api.db")
                .UseLazyLoadingProxies();
           
            return new CoreStrengthYogaProductsApiDbContext(optionsBuilder.Options);
        }
    }
}
