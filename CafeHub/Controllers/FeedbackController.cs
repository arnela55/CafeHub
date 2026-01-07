using CafeHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Controllers
{
    public class FeedbackController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Feedback");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Add(int rating, string comment)
        {
            // Provjera prijave (isto kao kod AddToCart)
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail == null)
            {
                ViewData["FeedbackMessage"] = "Morate biti prijavljeni da pošaljete feedback.";
                ViewData["FeedbackClass"] = "warning";
                return View("Feedback");
            }

            if (rating < 1 || rating > 5 || string.IsNullOrWhiteSpace(comment))
            {
                ViewData["FeedbackMessage"] = "Molimo unesite ocjenu od 1 do 5 i komentar.";
                ViewData["FeedbackClass"] = "warning";
                return View("Feedback");
            }

            var feedback = new ServiceFeedback
            {
                Rating = rating,
                Comment = comment
            };

            DatabaseModel.ServiceFeedbacks.Add(feedback);

            ViewData["FeedbackMessage"] = "Hvala na vašem komentaru! ☕";
            ViewData["FeedbackClass"] = "success";

            return View("Feedback");
        }

    }
}
