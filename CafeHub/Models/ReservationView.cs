namespace CafeHub.Models
{
    public class ReservationView
    {
        public Reservation NewReservation { get; set; }
        public List<Reservation> UserReservations { get; set; }
        public List<Table> AllTables { get; set; } // za prikaz mape stolova
    }
}
