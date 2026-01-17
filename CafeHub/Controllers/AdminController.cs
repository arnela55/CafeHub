using CafeHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            return View(); // Views/Admin/Index.cshtml
        }

        public IActionResult Users()
        {
            var users = DatabaseModel.Users; // hardkodirana lista korisnika
            return View(users);
        }

        // GET: Admin/EditUser/5
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = DatabaseModel.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Admin/EditUser
        [HttpPost]
        public IActionResult EditUser(User updatedUser)
        {
            var user = DatabaseModel.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user != null)
            {
                user.Role = updatedUser.Role; // update role
            }
            return RedirectToAction("Users");
        }

        public IActionResult Products()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            var products = DatabaseModel.Products;
            return View(products);
        }

        public IActionResult AddProduct()
        {
            // Hardkodirane kategorije
            ViewBag.Categories = new List<string>
    {
        "Kafa",
        "Topli napici",
        "Hladni napici",
        "Hrana"
    };
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(string Name, decimal Price, string Category)
        {
            var newProduct = new Product
            {
                Id = DatabaseModel.Products.Count + 1,
                Name = Name,
                Price = Price,
                Category = Category
            };

            DatabaseModel.Products.Add(newProduct);

            return RedirectToAction("Products");
        }

        public IActionResult EditProduct(int id)
        {
            var product = DatabaseModel.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            ViewBag.Categories = new List<string>
    {
        "Kafa",
        "Topli napici",
        "Hladni napici",
        "Hrana"
    };

            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(int Id, string Name, decimal Price, string Category)
        {
            var product = DatabaseModel.Products.FirstOrDefault(p => p.Id == Id);
            if (product != null)
            {
                product.Name = Name;
                product.Price = Price;
                product.Category = Category;
            }

            return RedirectToAction("Products");
        }

        public IActionResult Statistics()
        {
            var model = new AdminStatisticsViewModel
            {
                TopProducts = DatabaseModel.Products
                    .Select(p => new TopProductViewModel
                    {
                        Name = p.Name,
                        OrderCount = DatabaseModel.Orders
                            .Count(o => o.Items.Any(i => i.Product.Id == p.Id))
                    })
                    .OrderByDescending(p => p.OrderCount)
                    .Take(5)
                    .ToList(),

                ProductReviews = DatabaseModel.Products
                    .Select(p => new ProductReviewViewModel
                    {
                        ProductName = p.Name,
                        AverageRating = p.Ratings.Any()
                            ? p.Ratings.Average()
                            : 0,
                        ReviewCount = p.Ratings.Count
                    })
                    .ToList()
            };

            return View(model);
        }

        public IActionResult Feedback()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            var feedbacks = DatabaseModel.ServiceFeedbacks
                .OrderByDescending(f => f.CreatedAt)
                .ToList();

            return View(feedbacks);
        }


    }



}
