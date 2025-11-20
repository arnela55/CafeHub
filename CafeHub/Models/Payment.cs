namespace CafeHub.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public virtual Order Order { get; set; }
        public string PaymentMethod { get; set; } = "Online"; // ili "Gotovina"
        public string PaymentStatus { get; set; } = "Uspješno";
        public DateTime TransactionDate { get; set; } = DateTime.Now;

    }
}
