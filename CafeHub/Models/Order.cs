namespace CafeHub.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "U pripremi";

    }
}
