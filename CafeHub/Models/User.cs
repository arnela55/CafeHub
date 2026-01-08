namespace CafeHub.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get;set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int LoyaltyPoints { get; set; } = 0;
        public int FreeCoffees { get; set; } = 0;


    }
}
