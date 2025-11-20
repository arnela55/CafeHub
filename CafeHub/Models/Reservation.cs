namespace CafeHub.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int TableNumber { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NumberOfPeople { get; set; }
        public string Status { get; set; } = "Aktivna";
    }
}
