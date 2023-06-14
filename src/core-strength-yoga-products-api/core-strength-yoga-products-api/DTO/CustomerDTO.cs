using core_strength_yoga_products_api.Models;

namespace core_strength_yoga_products_api.DTO;

public class CustomerDTO
{
    public string IdentityUserName { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsGdpr { get; set; }
    public CustomerDetailDTO CustomerDetail { get; set; }
}