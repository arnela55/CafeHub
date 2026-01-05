namespace CafeHub.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } 
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<int> Ratings { get; set; } = new();
        public double AverageRating { get; set; }

    }
}
