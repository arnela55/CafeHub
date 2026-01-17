using CafeHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Controllers
{
    public class AccountController : Controller
    {
        /* =======================
           LOGIN
        ======================== */

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = DatabaseModel.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // SESSION PODACI
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserRole", user.Role);

                // REDIRECT PO ROLE
                if (user.Role == "Admin")
                    return RedirectToAction("Index", "Admin");

                if (user.Role == "Employee")
                    return RedirectToAction("Index", "Employee");

                return RedirectToAction("Index", "Home"); // Customer
            }

            ViewBag.ErrorMessage = "Pogrešan email ili lozinka.";
            return View();
        }

        /* =======================
           LOGOUT
        ======================== */

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        /* =======================
           REGISTRACIJA (CUSTOMER)
        ======================== */

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string FullName, string Email, string PhoneNumber, string Password)
        {
            var exists = DatabaseModel.Users.FirstOrDefault(u => u.Email == Email);

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
                Role = "Customer" // ⬅️ REGISTRACIJA SAMO KUPAC
            };

            DatabaseModel.Users.Add(newUser);

            // SESSION
            HttpContext.Session.SetString("UserEmail", Email);
            HttpContext.Session.SetString("UserName", FullName);
            HttpContext.Session.SetString("UserRole", "Customer");

            return RedirectToAction("Index", "Home");
        }
    }
}
