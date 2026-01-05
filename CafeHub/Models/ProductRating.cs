namespace CafeHub.Models
{
    public class ProductRating
    {
        public int Id { get; set; }
        public virtual Product Product { get; set; }
        public int Stars { get; set; } // 1–5
        public DateTime CreatedAt { get; set; }
    }
}
