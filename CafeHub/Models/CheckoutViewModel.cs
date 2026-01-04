namespace CafeHub.Models
{
    public class CheckoutViewModel
    {
        public virtual Order Order { get; set; }

        public string PaymentMethod { get; set; } 

        // samo za online plaćanje
        public string? CardNumber { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CVV { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
