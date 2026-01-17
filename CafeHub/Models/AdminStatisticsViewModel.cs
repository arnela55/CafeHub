namespace CafeHub.Models
{
    public class AdminStatisticsViewModel
    {
        public List<TopProductViewModel> TopProducts { get; set; }
        public List<ProductReviewViewModel> ProductReviews { get; set; }
    }

    public class TopProductViewModel
    {
        public string Name { get; set; }
        public int OrderCount { get; set; }
    }

    public class ProductReviewViewModel
    {
        public string ProductName { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }

}
