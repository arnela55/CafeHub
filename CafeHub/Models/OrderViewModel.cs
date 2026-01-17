namespace CafeHub.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Na cekanju";
        public List<OrderItemViewModel> Items { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsPreparationStarted { get; set; } = false;

    }

    public class OrderItemViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
