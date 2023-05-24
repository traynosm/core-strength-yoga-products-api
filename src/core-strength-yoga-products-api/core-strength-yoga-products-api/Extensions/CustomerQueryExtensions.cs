using core_strength_yoga_products_api.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace core_strength_yoga_products_api.Extensions
{
    public static class CustomerQueryExtensions
    {
        public static IIncludableQueryable<Customer, IEnumerable<AddressDetail>> IncludeAllRelated(this DbSet<Customer> set)
        {
            return set
                .Include(p => p.CustomerDetail)
                .Include(p => p.CustomerDetail.Addresses);
        }
    }
}
