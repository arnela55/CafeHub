namespace CafeHub.Models
{
    public class EmployeeDashboardView
    {
        public List<Order> Orders { get; set; }
        public List<Reservation> Reservations { get; set; }
        public int NewOrdersCount => Orders.Count(o =>
     o.Status == "U pripremi" ||
     o.Status == "-" ||
     o.Status == "Plaćeno"
 );

        public int ActiveReservationsCount => Reservations.Count(r => r.Status == "Aktivna");

    }
}
