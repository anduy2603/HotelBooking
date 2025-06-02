using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models;

namespace HotelBooking.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyBookings()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            // TODO: Implement booking creation logic
            return RedirectToAction(nameof(MyBookings));
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            // TODO: Implement booking cancellation logic
            return RedirectToAction(nameof(MyBookings));
        }
    }
} 