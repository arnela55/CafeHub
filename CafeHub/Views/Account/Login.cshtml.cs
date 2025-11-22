using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CafeHub.Views.Home.Login
{
    public class LoginModel : PageModel
    {
        // Hardkodirani korisnici
        private readonly List<User> _users = new List<User>
        {
            new User { Email = "admin@cafehub.com", Password = "1234" },
            new User { Email = "test@cafehub.com", Password = "abcd" }
        };

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            // samo prikaz forme
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Unesite email i lozinku.";
                return Page();
            }

            // Provjera korisnika u hardkodiranoj listi
            var user = _users.FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (user != null)
            {
                // Uspješan login ? redirect na Index
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Pogrešan email ili lozinka.";
                return Page();
            }
        }

        // Lokalna klasa User za hardkodirane korisnike
        public class User
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}

