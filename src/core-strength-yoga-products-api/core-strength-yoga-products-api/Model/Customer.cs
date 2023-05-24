using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core_strength_yoga_products_api.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string IdentityUserName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsGdpr { get; set; }

        public int CustomerDetailId { get; set; }
        public virtual CustomerDetail CustomerDetail { get; set; }

        public Customer() { }

        public Customer(int id, string identityUserName, DateTime createdAt, bool isActive, bool isGdpr)
        {
            Id = id;
            IdentityUserName = identityUserName;
            CreatedAt = createdAt;
            IsActive = isActive;
            IsGdpr = isGdpr;
        }
    }
}
