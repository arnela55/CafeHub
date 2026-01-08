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


        // PRIKAZ PLAĆANJA
        public IActionResult Checkout()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail == null)
                return RedirectToAction("Login", "Account");

            var user = DatabaseModel.Users.First(u => u.Email == userEmail);
            var order = DatabaseModel.Orders
                .FirstOrDefault(o => o.User == user && o.Status == "U pripremi");

            if (order == null || !order.Items.Any())
                return RedirectToAction("Cart");

            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FinishPayment(string paymentMethod)
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (userEmail == null)
                return RedirectToAction("Login", "Account");

            var user = DatabaseModel.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null) return RedirectToAction("Login", "Account");

            var order = DatabaseModel.Orders
                .FirstOrDefault(o => o.User == user && o.Status == "U pripremi");

            if (order == null) return RedirectToAction("Cart");

            // Normalizacija inputa
            var pm = paymentMethod?.Trim().ToLower();

            // Postavljanje statusa narudžbe
            if (pm == "online")
            {
                order.Status = "Plaćeno";
            }
            else if (pm == "cod")
            {
                order.Status = "-"; // crtica za pouzeće
            }
            else
            {
                order.Status = "U pripremi"; // fallback
            }

            // Snimanje u Payments
            var payment = new Payment
            {
                Id = DatabaseModel.Payments.Count + 1,
                Order = order,
                PaymentMethod = pm == "online" ? "Online" : "Pouzeće",
                PaymentStatus = pm == "online" ? "Uspješno" : "Na čekanju",
                TransactionDate = DateTime.Now
            };
            DatabaseModel.Payments.Add(payment);

            // TotalAmount
            order.TotalAmount = order.Items.Sum(i => i.Price * i.Quantity);

            //loyalty status
            int earnedPoints = (int)Math.Floor(order.TotalAmount); // 1 KM = 1 bod
            user.LoyaltyPoints += earnedPoints;

      
            TempData["LoyaltyMessage"] =
                $"Osvojili ste {earnedPoints} loyalty bodova! Ukupno: {user.LoyaltyPoints}";

            // Redirect na Receipt
            return RedirectToAction("Receipt", new { id = order.Id });
        }


        public IActionResult Receipt(int id)
        {
            var order = DatabaseModel.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            var payment = DatabaseModel.Payments.FirstOrDefault(p => p.Order.Id == id);
            ViewBag.Payment = payment;

            return View(order);
        }





    }
}

