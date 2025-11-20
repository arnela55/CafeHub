using CafeHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            var products = DatabaseModel.Products;
            return View(products);
        }
    }
}
