namespace CafeHub.Models
{
    public class DatabaseModel
    {
        public static List<User> Users = new List<User>
        {
            new User{Email="admin@cafehub.com", Password="1234", Name="Admin", Role="Admin"},
            new User{Email="employee@cafehub.com", Password="1234", Name="Employee", Role="Employee"},
            new User{Email="arnela@cafehub.com", Password="arnelaICR", Name="Arnela", Role="Customer"}
        };

        public static List<Product> Products = new List<Product>
{
    // Kafa
    new Product { Id = 1, Name = "Espresso", Category = "Kafa", Price = 2.50M, Description="Jaka crna kafa", Image="/images/products/espresso.jpg.jpg" },
    new Product { Id = 2, Name = "Cappuccino", Category = "Kafa", Price = 3.00M, Description="Espresso sa pjenom od mlijeka", Image="/images/products/cappucino.jpg" },
    new Product { Id = 3, Name = "Latte", Category = "Kafa", Price = 3.50M, Description="Espresso sa puno mlijeka i pjenom", Image="/images/products/latte.jpg" },
    new Product { Id = 4, Name = "Americano", Category = "Kafa", Price = 2.80M, Description="Rjeđa, duža crna kafa", Image="/images/products/americano.jpg" },
    new Product { Id = 5, Name = "Mocha", Category = "Kafa", Price = 3.80M, Description="Espresso sa čokoladom i mlijekom", Image="/images/products/mocha.jpg" },
    
    // Topli napici
    new Product { Id = 6, Name = "Čaj – Zeleni", Category = "Topli napici", Price = 2.00M, Description="Zeleni čaj visokog kvaliteta", Image="/images/products/greenTea.jpg" },
    new Product { Id = 7, Name = "Čaj – Crni", Category = "Topli napici", Price = 2.00M, Description="Klasični crni čaj", Image="/images/products/blackTea.png" },
    new Product { Id = 8, Name = "Topla čokolada", Category = "Topli napici", Price = 3.00M, Description="Kremasta topla čokolada sa šlagom", Image="/images/products/hotChocolate.jpg" },

    // Hladni napici
    new Product { Id = 9, Name = "Iced Latte", Category = "Hladni napici", Price = 3.80M, Description="Hladna kafa sa mlijekom i ledom", Image="/images/products/icedLatte.png" },
    new Product { Id = 10, Name = "Smoothie – Jagoda", Category = "Hladni napici", Price = 4.50M, Description="Svježi jagodni smoothie", Image="/images/products/strawberrySmoothie.jpg" },

    // Hrana
    new Product { Id = 11, Name = "Sendvič – Curry piletina", Category = "Hrana", Price = 4.50M, Description="Topli sendvič sa sočnom piletinom u curry sosu", Image="/images/products/curry.jpg" },
    new Product { Id = 12, Name = "Sendvič – Đački", Category = "Hrana", Price = 4.00M, Description="Lagani sendvič sa šunkom, sirom i svježim povrćem", Image="/images/products/sandwich.jpg" },
    new Product { Id = 13, Name = "Croissant", Category = "Hrana", Price = 2.50M, Description="Svježi maslac croissant", Image="/images/products/croissant.jpg" },
    new Product { Id = 14, Name = "Kolač – Čokolada", Category = "Hrana", Price = 3.50M, Description="Sočni čokoladni kolač", Image="/images/products/chocolateCake.jpg" },
    new Product { Id = 15, Name = "Kolač – Cheesecake", Category = "Hrana", Price = 3.80M, Description="Kremasti cheesecake sa prelivom od jagode", Image="/images/products/cheesecakeStraw.jpg" },
};

        public static List<Reservation> Reservations = new List<Reservation>()
        {
            // primjer postojeće rezervacije (opcionalno)
            // new Reservation { Id = 1, User = Users[1], TableNumber = 2, ReservationTime = DateTime.Now.AddHours(2), NumberOfPeople = 2, Status = "Aktivna" }
        };

        // Hardkodirani stolovi (TableNumber, Name, Seats, row/col za prikaz)
        public static List<Table> Tables = new List<Table>
        {
            new Table { Number = 1, Name = "Sto 1", Seats = 2, Row = 1, Col = 1 },
            new Table { Number = 2, Name = "Sto 2", Seats = 2, Row = 1, Col = 2 },
            new Table { Number = 3, Name = "Sto 3", Seats = 4, Row = 1, Col = 3 },
            new Table { Number = 4, Name = "Sto 4", Seats = 4, Row = 1, Col = 4 },
            new Table { Number = 5, Name = "Sto 5", Seats = 2, Row = 2, Col = 1 },
            new Table { Number = 6, Name = "Sto 6", Seats = 2, Row = 2, Col = 2 },
            new Table { Number = 7, Name = "Sto 7", Seats = 4, Row = 2, Col = 3 },
            new Table { Number = 8, Name = "Sto 8", Seats = 4, Row = 2, Col = 4 },
            new Table { Number = 9, Name = "Sto 9", Seats = 6, Row = 3, Col = 1 },
            new Table { Number = 10, Name = "Sto 10", Seats = 6, Row = 3, Col = 2 },
            new Table { Number = 11, Name = "Sto 11", Seats = 4, Row = 3, Col = 3 },
            new Table { Number = 12, Name = "Sto 12", Seats = 2, Row = 3, Col = 4 }
        };
        public static List<Order> Orders = new List<Order>();
        public static List<OrderItem> OrderItems = new List<OrderItem>();
       // public static List<Reservation> Reservations = new List<Reservation>();
        public static List<Payment> Payments = new List<Payment>();

        public static List<ServiceFeedback> ServiceFeedbacks { get; set; } = new List<ServiceFeedback>();

    }
}
