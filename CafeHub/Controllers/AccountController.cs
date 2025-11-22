using CafeHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> _users = new List<User>
        {
            new User { Email = "admin@cafehub.com", Password = "1234", Name="Admin" }
        };

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.Name);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Pogrešan email ili lozinka.";
            return View();
        }

        // Logout i Register po potrebi

        public class User
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
        }
    }
}
