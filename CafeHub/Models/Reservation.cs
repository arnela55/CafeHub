namespace CafeHub.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int TableNumber { get; set; }
        public virtual Table Table { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NumberOfPeople { get; set; }
        public string Status { get; set; } = "Aktivna";
        public bool IsConfirmed { get; set; } = false; // NOVO

    }
}
