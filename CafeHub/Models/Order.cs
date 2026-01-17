namespace CafeHub.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string Customer { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "U pripremi";
        public string Placanje { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public bool IsPreparationStarted { get; set; } = false;



    }
}
