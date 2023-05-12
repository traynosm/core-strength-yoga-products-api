using System.ComponentModel.DataAnnotations;

namespace core_strength_yoga_products_api.Models
{
    public class CustomerDetail
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }

        public CustomerDetail(int id, int customerId, string firstName, string surname, 
            string phoneNo, string email)
        {
            Id = id;
            FirstName = firstName;
            Surname = surname;
            PhoneNo = phoneNo;
            Email = email;
            CustomerId = customerId;
        }
    }
}
