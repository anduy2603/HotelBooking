using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Data;
using HotelBooking.Models;
using HotelBooking.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HotelBooking.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Booking/MyBookings
        [HttpGet]
        public async Task<IActionResult> MyBookings(string? status = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var userId = GetCurrentUserId();
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var query = _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.CheckInOut)
                .Include(b => b.Rating)
                .Where(b => b.UserId == userId);

            // Lọc theo trạng thái
            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse<BookingStatus>(status, out var bookingStatus))
                {
                    query = query.Where(b => b.Status == bookingStatus);
                }
            }

            // Lọc theo khoảng thời gian
            if (fromDate.HasValue)
            {
                query = query.Where(b => b.CheckInDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(b => b.CheckOutDate <= toDate.Value);
            }

            var bookings = await query
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            // Chuyển đổi sang ViewModel
            var viewModels = bookings.Select(b => new BookingViewModel
            {
                Id = b.Id,
                BookingNumber = b.BookingNumber,
                RoomId = b.RoomId,
                Room = b.Room,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                TotalPrice = b.TotalPrice,
                DepositAmount = b.DepositAmount,
                Status = b.Status,
                PaymentStatus = b.PaymentStatus,
                CreatedAt = b.CreatedAt,
                ModifiedAt = b.ModifiedAt,
                CheckInOut = b.CheckInOut,
                Rating = b.Rating,
                SpecialRequests = b.SpecialRequests,
                CancellationReason = b.CancellationReason,
                CancelledAt = b.Status == BookingStatus.Cancelled ? b.ModifiedAt : null
            });

            ViewBag.StatusFilter = status;
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return View(viewModels);
        }

        public async Task<IActionResult> Create(int roomId)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null)
                return NotFound();

            var model = new BookingViewModel
            {
                RoomId = roomId,
                Room = room,
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = GetCurrentUserId();
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var isRoomAvailable = await IsRoomAvailable(model.RoomId, model.CheckInDate, model.CheckOutDate);
            if (!isRoomAvailable)
            {
                ModelState.AddModelError("", "Phòng đã được đặt trong khoảng thời gian này.");
                return View(model);
            }

            var room = await _context.Rooms.FindAsync(model.RoomId);
            var totalDays = (model.CheckOutDate - model.CheckInDate).Days;
            var totalPrice = room.Price * totalDays;
            var depositAmount = totalPrice * 0.3m;

            var booking = new Booking
            {
                RoomId = model.RoomId,
                UserId = userId,
                GuestId = userId, // Assuming GuestId is same as UserId for now
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                TotalPrice = totalPrice,
                DepositAmount = depositAmount,
                SpecialRequests = model.SpecialRequests,
                Status = BookingStatus.Pending,
                PaymentStatus = PaymentStatus.Pending,
                CreatedAt = DateTime.Now,
                BookingNumber = $"BK{DateTime.Now:yyyyMMddHHmmss}",
                PaymentMethod = model.PaymentMethod
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Confirmation), new { id = booking.Id });
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null || booking.UserId != GetCurrentUserId())
                return NotFound();

            var model = new BookingViewModel
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                Room = booking.Room,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                SpecialRequests = booking.SpecialRequests
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookingViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null || booking.UserId != GetCurrentUserId())
                return NotFound();

            var isRoomAvailable = await IsRoomAvailable(model.RoomId, model.CheckInDate, model.CheckOutDate, id);
            if (!isRoomAvailable)
            {
                ModelState.AddModelError("", "Phòng đã được đặt trong khoảng thời gian này.");
                return View(model);
            }

            var room = await _context.Rooms.FindAsync(model.RoomId);
            var totalDays = (model.CheckOutDate - model.CheckInDate).Days;
            var totalPrice = room.Price * totalDays;

            var modification = new BookingModification
            {
                BookingId = booking.Id,
                ModifiedAt = DateTime.Now,
                ModifiedBy = GetCurrentUserId().ToString(),
                Changes = $"Check-in: {booking.CheckInDate} -> {model.CheckInDate}, " +
                         $"Check-out: {booking.CheckOutDate} -> {model.CheckOutDate}, " +
                         $"Total price: {booking.TotalPrice} -> {totalPrice}"
            };

            booking.CheckInDate = model.CheckInDate;
            booking.CheckOutDate = model.CheckOutDate;
            booking.TotalPrice = totalPrice;
            booking.SpecialRequests = model.SpecialRequests;
            booking.ModifiedAt = DateTime.Now;

            _context.BookingModifications.Add(modification);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = booking.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, string cancellationReason)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null || booking.UserId != GetCurrentUserId())
                return NotFound();

            if (booking.Status != BookingStatus.Pending && booking.Status != BookingStatus.Confirmed)
            {
                return BadRequest("Không thể hủy đặt phòng ở trạng thái này.");
            }

            // Kiểm tra thời gian hủy phòng (ví dụ: không cho phép hủy trong vòng 24h trước check-in)
            if (booking.CheckInDate <= DateTime.Now.AddHours(24))
            {
                return BadRequest("Không thể hủy đặt phòng trong vòng 24 giờ trước ngày nhận phòng.");
            }

            booking.Status = BookingStatus.Cancelled;
            booking.ModifiedAt = DateTime.Now;
            booking.CancellationReason = cancellationReason;

            // Thêm lịch sử chỉnh sửa
            var modificationHistory = new BookingModification
            {
                BookingId = booking.Id,
                ModifiedAt = DateTime.Now,
                ModifiedBy = GetCurrentUserId().ToString(),
                Changes = $"Hủy đặt phòng. Lý do: {cancellationReason}"
            };

            _context.BookingModifications.Add(modificationHistory);
            await _context.SaveChangesAsync();

            // TODO: Gửi email thông báo hủy phòng

            return RedirectToAction(nameof(Details), new { id = booking.Id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Guest)
                .Include(b => b.Modifications)
                .Include(b => b.CheckInOut)
                .Include(b => b.Rating)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null || booking.UserId != GetCurrentUserId())
                return NotFound();

            var viewModel = new BookingViewModel
            {
                Id = booking.Id,
                BookingNumber = booking.BookingNumber,
                RoomId = booking.RoomId,
                Room = booking.Room,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalPrice = booking.TotalPrice,
                DepositAmount = booking.DepositAmount,
                Status = booking.Status,
                PaymentStatus = booking.PaymentStatus,
                CreatedAt = booking.CreatedAt,
                ModifiedAt = booking.ModifiedAt,
                Modifications = booking.Modifications.ToList(),
                CheckInOut = booking.CheckInOut,
                Rating = booking.Rating,
                SpecialRequests = booking.SpecialRequests,
                CancellationReason = booking.CancellationReason,
                CancelledAt = booking.Status == BookingStatus.Cancelled ? booking.ModifiedAt : null
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Invoice(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Guest)
                .Include(b => b.CheckInOut)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null || booking.UserId != GetCurrentUserId())
                return NotFound();

            var viewModel = new BookingViewModel
            {
                Id = booking.Id,
                BookingNumber = booking.BookingNumber,
                RoomId = booking.RoomId,
                Room = booking.Room,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalPrice = booking.TotalPrice,
                DepositAmount = booking.DepositAmount,
                Status = booking.Status,
                PaymentStatus = booking.PaymentStatus,
                CreatedAt = booking.CreatedAt,
                CheckInOut = booking.CheckInOut,
                SpecialRequests = booking.SpecialRequests
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Rate(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Rating)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null || booking.UserId != GetCurrentUserId())
                return NotFound();

            if (booking.Status != BookingStatus.CheckedOut)
            {
                return BadRequest("Chỉ có thể đánh giá sau khi đã check-out.");
            }

            if (booking.Rating != null)
            {
                return RedirectToAction(nameof(Details), new { id = booking.Id });
            }

            return View(new RatingViewModel { BookingId = booking.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rate(int id, RatingViewModel model)
        {
            if (id != model.BookingId)
                return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Rating)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null || booking.UserId != GetCurrentUserId())
                return NotFound();

            if (booking.Status != BookingStatus.CheckedOut)
            {
                return BadRequest("Chỉ có thể đánh giá sau khi đã check-out.");
            }

            if (booking.Rating != null)
            {
                return RedirectToAction(nameof(Details), new { id = booking.Id });
            }

            var rating = new Rating
            {
                BookingId = booking.Id,
                RatingValue = model.Score,
                Comment = model.Comment,
                CreatedAt = DateTime.Now
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = booking.Id });
        }

        private async Task<bool> IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut, int? excludeBookingId = null)
        {
            var query = _context.Bookings
                .Where(b => b.RoomId == roomId &&
                           b.Status != BookingStatus.Cancelled &&
                           ((b.CheckInDate <= checkIn && b.CheckOutDate > checkIn) ||
                            (b.CheckInDate < checkOut && b.CheckOutDate >= checkOut) ||
                            (b.CheckInDate >= checkIn && b.CheckOutDate <= checkOut)));

            if (excludeBookingId.HasValue)
            {
                query = query.Where(b => b.Id != excludeBookingId.Value);
            }

            return !await query.AnyAsync();
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }
    }
} 