using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models;
using HotelBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Controllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return View(rooms);
        }

        public async Task<IActionResult> Details(int id)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        public async Task<IActionResult> Search(DateTime? checkInDate, DateTime? checkOutDate, int? guests, string? roomType)
        {
            var query = _context.Rooms.AsQueryable();

            if (guests.HasValue)
            {
                query = query.Where(r => r.MaxGuests >= guests.Value);
            }

            if (!string.IsNullOrEmpty(roomType))
            {
                if (Enum.TryParse<RoomType>(roomType, true, out var type))
                {
                    query = query.Where(r => r.Type == type);
                }
            }

            var rooms = await query.ToListAsync();
            return View(rooms);
        }
    }
} 