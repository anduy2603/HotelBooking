using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models;

namespace HotelBooking.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckInOut()
        {
            return View();
        }

        public IActionResult Invoice()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcessCheckIn(int bookingId)
        {
            // TODO: Implement check-in logic
            return RedirectToAction(nameof(CheckInOut));
        }

        [HttpPost]
        public IActionResult ProcessCheckOut(int bookingId)
        {
            // TODO: Implement check-out logic
            return RedirectToAction(nameof(CheckInOut));
        }

        [HttpPost]
        public IActionResult GenerateInvoice(int bookingId)
        {
            // TODO: Implement invoice generation logic
            return RedirectToAction(nameof(Invoice));
        }
    }
} 