namespace CafeHub.Models
{
    public class Table
    {
        public int Number { get; set; }    // jedinstveni broj stola
        public string Name { get; set; }   // "Sto 1"
        public int Seats { get; set; }     // koliko sjedi
        public int Row { get; set; }       // pozicija za grid prikaz
        public int Col { get; set; }       // pozicija za grid prikaz
        public bool IsReserved { get; set; }
    }
}
