using CafeHub.Helpers;
using CafeHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail == null)
                return RedirectToAction("Login", "Account");

            var user = DatabaseModel.Users.FirstOrDefault(u => u.Email == userEmail);
            var product = DatabaseModel.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null) return NotFound();

            // Pronađi postojeću "U pripremi" narudžbu
            var order = DatabaseModel.Orders
                .FirstOrDefault(o => o.User == user && o.Status == "U pripremi");

            if (order == null)
            {
                order = new Order
                {
                    Id = DatabaseModel.Orders.Count + 1,
                    User = user,
                    Status = "U pripremi",
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                };
                DatabaseModel.Orders.Add(order);
            }

            var existingItem = order.Items.FirstOrDefault(i => i.Product.Id == productId);
            if (existingItem != null)
                existingItem.Quantity += quantity;
            else
                order.Items.Add(new OrderItem
                {
                    Id = DatabaseModel.OrderItems.Count + 1,
                    Order = order,
                    Product = product,
                    Quantity = quantity,
                    Price = product.Price
                });
            TempData["CartMessage"] = $"{product.Name} je dodan u korpu!";

            // Vratimo korisnika na stranicu s koje je došao
            return Redirect(Request.Headers["Referer"].ToString());
        }




        // 2) PRIKAZ KORPE
        public IActionResult Cart()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");

            if (userEmail == null)
                return RedirectToAction("Login", "Account");

            var user = DatabaseModel.Users.FirstOrDefault(u => u.Email == userEmail);

            var order = DatabaseModel.Orders
                .FirstOrDefault(o => o.User == user && o.Status == "U pripremi");

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int id)
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail == null)
                return RedirectToAction("Login", "Account");

            var user = DatabaseModel.Users.FirstOrDefault(u => u.Email == userEmail);
            var order = DatabaseModel.Orders
                .FirstOrDefault(o => o.User == user && o.Status == "U pripremi");

            if (order != null)
            {
                var item = order.Items.FirstOrDefault(i => i.Id == id);
                if (item != null)
                    order.Items.Remove(item);
            }

            return RedirectToAction("Cart");
        }

    }
}

