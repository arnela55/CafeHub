namespace CafeHub.Models
{
    public class DatabaseModel
    {
        public static List<User> Users = new List<User>();

        public static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Espresso", Category = "Kafa", Price = 2.50M, Description="Jaka crna kafa", Image="/images/products/espresso.jpg.jpg" },
            new Product { Id = 2, Name = "Cappuccino", Category = "Kafa", Price = 3.00M, Description="Espresso sa pjenom od mlijeka", Image=null },
            new Product { Id = 3, Name = "Sendvič", Category = "Hrana", Price = 4.00M, Description="Razni topli sendvici", Image=null }
        };

        public static List<Order> Orders = new List<Order>();
        public static List<OrderItem> OrderItems = new List<OrderItem>();
        public static List<Reservation> Reservations = new List<Reservation>();
        public static List<Payment> Payments = new List<Payment>();
    }
}
