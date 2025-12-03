using Microsoft.AspNetCore.Mvc;
using CafeHub.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CafeHub.Controllers
{
    public class ReservationController : Controller
    {
        private readonly TimeSpan ReservationDuration = TimeSpan.FromHours(3);

        // GET: /Reservation/Index
        public IActionResult Index(DateTime? selectedDateTime, int? selectedTableNumber)
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                TempData["LoginMessage"] =
                    "Morate biti ulogovani kako biste izvršili narudžbu ili rezervaciju stola.";
                return RedirectToAction("Login", "Account");
            }

            var user = DatabaseModel.Users.FirstOrDefault(u => u.Email == userEmail);

            // Resetuj sva zauzeća
            foreach (var t in DatabaseModel.Tables)
                t.IsReserved = false;

            // Oznaci zauzete stolove za sve aktivne rezervacije
            foreach (var r in DatabaseModel.Reservations.Where(r => r.Status == "Aktivna"))
            {
                var table = DatabaseModel.Tables.FirstOrDefault(t => t.Number == r.TableNumber);
                if (table != null)
                    table.IsReserved = true;
            }

            // Ako korisnik je unio datum/termin, označi zauzete stolove za taj termin
            if (selectedDateTime.HasValue)
            {
                var overlapping = DatabaseModel.Reservations
                    .Where(r => r.Status == "Aktivna" &&
                               r.ReservationTime < selectedDateTime.Value.Add(ReservationDuration) &&
                               r.ReservationTime.Add(ReservationDuration) > selectedDateTime.Value)
                    .Select(r => r.TableNumber)
                    .ToList();

                foreach (var t in DatabaseModel.Tables.Where(t => overlapping.Contains(t.Number)))
                {
                    t.IsReserved = true; // crvena boja
                }
            }

            var model = new ReservationView
            {
                NewReservation = new Reservation
                {
                    ReservationTime = selectedDateTime ?? DateTime.Now,
                    TableNumber = selectedTableNumber ?? 0
                },
                UserReservations = DatabaseModel.Reservations
                    .Where(r => r.User != null && r.User.Email == userEmail)
                    .OrderByDescending(r => r.ReservationTime)
                    .ToList(),
                AllTables = DatabaseModel.Tables
            };

            return View(model);
        }

        // POST: /Reservation/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Reservation newReservation)
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                TempData["LoginMessage"] =
                    "Morate biti ulogovani kako biste izvršili narudžbu ili rezervaciju stola.";
                return RedirectToAction("Login", "Account");
            }

            // Validacija: termin u prošlosti
            if (newReservation.ReservationTime < DateTime.Now)
            {
                TempData["ReservationMessage"] = "Ne možete rezervisati termin u prošlosti.";
                return RedirectToAction("Index", new { selectedDateTime = newReservation.ReservationTime, selectedTableNumber = newReservation.TableNumber });
            }

            // Sto postoji?
            var exists = DatabaseModel.Tables.Any(t => t.Number == newReservation.TableNumber);
            if (!exists)
            {
                TempData["ReservationMessage"] = "Odabrani sto ne postoji.";
                return RedirectToAction("Index", new { selectedDateTime = newReservation.ReservationTime });
            }

            // Provjera preklapanja termina (3h)
            var overlappingReservations = DatabaseModel.Reservations
                .Where(r => r.TableNumber == newReservation.TableNumber && r.Status == "Aktivna")
                .ToList();

            foreach (var r in overlappingReservations)
            {
                var start = r.ReservationTime;
                var end = r.ReservationTime.Add(ReservationDuration);

                var newStart = newReservation.ReservationTime;
                var newEnd = newReservation.ReservationTime.Add(ReservationDuration);

                bool overlap = newStart < end && start < newEnd;
                if (overlap)
                {
                    TempData["ReservationMessage"] =
                        $"Sto {r.TableNumber} je zauzet u tom terminu ({start:g} - {end:g}).";
                    return RedirectToAction("Index", new { selectedDateTime = newReservation.ReservationTime, selectedTableNumber = newReservation.TableNumber });
                }
            }

            // USER
            var user = DatabaseModel.Users.FirstOrDefault(u => u.Email == userEmail)
                       ?? new User
                       {
                           Email = userEmail,
                           Name = "Korisnik",
                           Id = DatabaseModel.Users.Count + 1
                       };

            if (!DatabaseModel.Users.Contains(user))
                DatabaseModel.Users.Add(user);

            // Označi sto kao zauzet
            var reservedTable = DatabaseModel.Tables.FirstOrDefault(t => t.Number == newReservation.TableNumber);
            if (reservedTable != null)
                reservedTable.IsReserved = true;

            // Spremi rezervaciju
            newReservation.User = user;
            newReservation.Id = DatabaseModel.Reservations.Count + 1;
            newReservation.Status = "Aktivna";

            DatabaseModel.Reservations.Add(newReservation);

            // Zelena boja: označi upravo potvrđenu rezervaciju
            TempData["ReservationMessage"] = "Rezervacija uspješno kreirana!";
            TempData["ConfirmedTable"] = newReservation.TableNumber;

            return RedirectToAction("Index", new { selectedDateTime = newReservation.ReservationTime });
        }
    }
}
