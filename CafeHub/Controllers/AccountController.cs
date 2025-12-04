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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();   // obriši podatke o prijavi
            return RedirectToAction("Index", "Home"); // vrati na početnu
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string FullName, string Email, string PhoneNumber, string Password)
        {
            var exists = _users.FirstOrDefault(u => u.Email == Email);

            if (exists != null)
            {
                ViewBag.ErrorMessage = "Korisnik sa ovim emailom već postoji.";
                return View();
            }

            var newUser = new User
            {
                Name = FullName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Password = Password,
                Role = "User"
            };

            _users.Add(newUser);

            HttpContext.Session.SetString("UserEmail", Email);
            HttpContext.Session.SetString("UserName", FullName);

            return RedirectToAction("Index", "Home");
        }



        //public class User
        //{
        //    public int Id { get; set; }
        //    public string Email { get; set; }
        //    public string Password { get; set; }
        //    public string 
        //    public string Name { get; set; }
        //}
    }
}
