using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            List<string> images = new List<string>
            {
                "/images/gallery/interior.jpg"
            };

            return View(images);
        }
    }
}
