namespace CafeHub.Models
{
    public class ServiceFeedback
    {
        public int Id { get; set; }
        public int Rating { get; set; } 
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
