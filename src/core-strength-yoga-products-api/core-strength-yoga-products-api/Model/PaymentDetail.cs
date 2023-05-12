using System.ComponentModel.DataAnnotations;

namespace core_strength_yoga_products_api.Models
{
    public class PaymentDetail
    {
        [Key]
        public int Id { get; set; }
        public int CustomerDetailId { get; set; }
        public virtual CustomerDetail CustomerDetail { get; set; }
        public string CardType { get; set; }
        public bool IsDebit { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string Expiry { get; set; }
        public bool IsDefault { get; set; }

        public PaymentDetail(int id, string cardType, bool isDebit, string cardNumber, string cardName, string expiry, bool isDefault,
            int customerDetailId)
        {
            Id = id;
            CardType = cardType;
            IsDebit = isDebit;
            CardNumber = cardNumber;
            CardName = cardName;
            Expiry = expiry;
            IsDefault = isDefault;
            CustomerDetailId = customerDetailId;  
        }
    }
}
