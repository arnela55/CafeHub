using CafeHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Controllers
{
    public class LoyaltyController : Controller
    {
        public IActionResult Index()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail == null)
                return RedirectToAction("Login", "Account");

            var user = DatabaseModel.Users.FirstOrDefault(u => u.Email == userEmail);
            return View(user); 
        }

    }
}
