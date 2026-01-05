using CafeHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Controllers
{
    public class RatingController : Controller
    {
        [HttpPost]
        public IActionResult Add(int productId, int stars)
        {
            // Validacija
            if (stars < 1 || stars > 5)
                return RedirectToAction("Index", "Menu");

            // Pronađi proizvod
            var product = DatabaseModel.Products
                .FirstOrDefault(p => p.Id == productId);

            if (product == null)
                return RedirectToAction("Index", "Menu");

            // Dodaj rating (mock / demo)
            product.Ratings.Add(stars);

            // Izračunaj prosjek
            product.AverageRating = product.Ratings.Average();

            return RedirectToAction("Index", "Menu");
        }
    }
}
