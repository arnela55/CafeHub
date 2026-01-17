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

        // ✅ NOVO: Prikaz stranice za Narudžbe
        public IActionResult Orders()
        {
            if (!IsEmployee())
                return RedirectToAction("Login", "Account");

            var orders = DatabaseModel.Orders;
            return View(orders); // Views/Employee/Orders.cshtml
        }

        // ✅ NOVO: Prikaz stranice za Rezervacije
        public IActionResult Reservations()
        {
            if (!IsEmployee())
                return RedirectToAction("Login", "Account");

            var reservations = DatabaseModel.Reservations;
            return View(reservations); // Views/Employee/Reservations.cshtml
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(int orderId, string status)
        {
            if (!IsEmployee())
                return RedirectToAction("Login", "Account");

            var order = DatabaseModel.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
                order.Status = status;

            return RedirectToAction("Index");
        }

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
    }
}
