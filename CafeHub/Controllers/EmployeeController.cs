using CafeHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeHub.Controllers
{
    public class EmployeeController : Controller
    {
        private bool IsEmployee()
        {
            return HttpContext.Session.GetString("UserRole") == "Employee";
        }

        public IActionResult Index()
        {
            if (!IsEmployee())
                return RedirectToAction("Login", "Account");

            var model = new EmployeeDashboardView
            {
                Orders = DatabaseModel.Orders,
                Reservations = DatabaseModel.Reservations
            };

            return View(model);
        }

        // ✅ Prikaz stranice za Narudžbe sa ViewModel-om
        public IActionResult Orders()
        {
            if (!IsEmployee())
                return RedirectToAction("Login", "Account");

            var orders = DatabaseModel.Orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                Customer = o.Customer,
                UserName = o.User?.Name ?? "Nepoznato",
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                PaymentMethod = DatabaseModel.Payments.FirstOrDefault(p => p.Order.Id == o.Id)?.PaymentMethod,
                IsPreparationStarted = o.IsPreparationStarted, // OVO JE KLJUČNO

                Items = o.Items.Select(i => new OrderItemViewModel
                {
                    ProductName = i.Product?.Name ?? "Nepoznato",
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }).ToList();

            return View(orders); // Views/Employee/Orders.cshtml
        }

        // ✅ Prikaz stranice za Rezervacije
        public IActionResult Reservations()
        {
            if (!IsEmployee())
                return RedirectToAction("Login", "Account");

            var reservations = DatabaseModel.Reservations;
            return View(reservations); // Views/Employee/Reservations.cshtml
        }

        //[HttpPost]
        //public IActionResult UpdateOrderStatus(int orderId, string status)
        //{
        //    if (!IsEmployee())
        //        return RedirectToAction("Login", "Account");

        //    var order = DatabaseModel.Orders.FirstOrDefault(o => o.Id == orderId);
        //    if (order != null)
        //        order.Status = status;

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public IActionResult UpdateReservationStatus(int reservationId, string status)
        {
            if (!IsEmployee())
                return RedirectToAction("Login", "Account");

            var reservation = DatabaseModel.Reservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservation != null)
                reservation.Status = status;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            if (!IsEmployee())
                return RedirectToAction("Login", "Account");

            var order = DatabaseModel.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            // Ako je status 'Nova narudzba', automatski postavi 'U pripremi'
            //if (order.Status == "U pripremi")
            //    order.Status = "Nova narudzba";

            var model = new OrderViewModel
            {
                Id = order.Id,
                UserName = order.User?.Name ?? "Nepoznato",
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                PaymentMethod = DatabaseModel.Payments.FirstOrDefault(p => p.Order.Id == order.Id)?.PaymentMethod,

                Items = order.Items.Select(i => new OrderItemViewModel
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(int orderId, string actionType)
        {
            if (HttpContext.Session.GetString("UserRole") != "Employee")
                return RedirectToAction("Login", "Account");

            var order = DatabaseModel.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                if (actionType == "StartPreparation")
                {
                    order.IsPreparationStarted = true;
                    // Status ostaje "U pripremi"
                }
                else if (actionType == "MarkReady")
                {
                    order.Status = "Zavrseno"; // kad employee klikne "Spremno"
                    order.IsPreparationStarted = false;
                }
            }

            return RedirectToAction("Orders"); // reload view da se dugme promijeni
        }



        [HttpPost]
        public IActionResult StartPreparation(int orderId)
        {
            if (!IsEmployee()) return RedirectToAction("Login", "Account");

            var order = DatabaseModel.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.IsPreparationStarted = true; // employee je kliknuo dugme
                                                   // status ostaje "U pripremi"
            }

            return RedirectToAction("Orders");
        }

        [HttpPost]
        public IActionResult MarkReady(int orderId)
        {
            if (!IsEmployee()) return RedirectToAction("Login", "Account");

            var order = DatabaseModel.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = "Završeno"; // kupac vidi da je gotovo
            }

            return RedirectToAction("Orders");
        }


    }
}