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

        public async Task<IActionResult> Search(
            DateTime? checkInDate,
            DateTime? checkOutDate,
            int? adults,
            int? children,
            RoomType? roomType,
            string bedType,
            decimal? minPrice,
            decimal? maxPrice,
            int? minSize,
            int? maxSize,
            string[] amenities,
            string status,
            string searchTerm)
        {
            // Store search parameters in ViewBag for form persistence
            ViewBag.CheckInDate = checkInDate?.ToString("yyyy-MM-dd");
            ViewBag.CheckOutDate = checkOutDate?.ToString("yyyy-MM-dd");
            ViewBag.Adults = adults;
            ViewBag.Children = children;
            ViewBag.RoomType = roomType;
            ViewBag.BedType = bedType;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.MinSize = minSize;
            ViewBag.MaxSize = maxSize;
            ViewBag.Amenities = amenities;
            ViewBag.Status = status;
            ViewBag.SearchTerm = searchTerm;

            var query = _context.Rooms.AsQueryable();

            // Apply filters
            if (adults.HasValue || children.HasValue)
            {
                int totalGuests = (adults ?? 0) + (children ?? 0);
                query = query.Where(r => r.MaxGuests >= totalGuests);
            }

            if (roomType.HasValue)
            {
                query = query.Where(r => r.Type == roomType.Value);
            }

            if (!string.IsNullOrEmpty(bedType))
            {
                query = query.Where(r => r.BedType == bedType);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(r => r.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(r => r.Price <= maxPrice.Value);
            }

            if (minSize.HasValue)
            {
                query = query.Where(r => r.Size >= minSize.Value);
            }

            if (maxSize.HasValue)
            {
                query = query.Where(r => r.Size <= maxSize.Value);
            }

            if (amenities != null && amenities.Any())
            {
                foreach (var amenity in amenities)
                {
                    query = query.Where(r => r.Amenities.Contains(amenity));
                }
            }

            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse<RoomStatus>(status, out var roomStatus))
                {
                    query = query.Where(r => r.Status == roomStatus);
                }
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(r =>
                    (r.Name != null && EF.Functions.Like(r.Name, $"%{searchTerm}%")) ||
                    (r.Description != null && EF.Functions.Like(r.Description, $"%{searchTerm}%")) ||
                    (r.Amenities != null && r.Amenities.Any(a => a.Contains(searchTerm)))
                );
            }

            // Check room availability for the selected dates
            if (checkInDate.HasValue && checkOutDate.HasValue)
            {
                query = query.Where(r => !r.Bookings.Any(b =>
                    (b.CheckInDate <= checkOutDate.Value && b.CheckOutDate >= checkInDate.Value) &&
                    (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.CheckedIn)
                ));
            }

            var rooms = await query.ToListAsync();
            return View(rooms);
        }
    }
} 