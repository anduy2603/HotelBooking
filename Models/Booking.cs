using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public int GuestId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    public string? SpecialRequests { get; set; }

    [Required]
    public BookingStatus Status { get; set; }

    // New properties
    [Required]
    [StringLength(20)]
    public string BookingNumber { get; set; } = GenerateBookingNumber();

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? ModifiedAt { get; set; }

    [StringLength(500)]
    public string? CancellationReason { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DepositAmount { get; set; }

    [Required]
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

    [Required]
    [StringLength(50)]
    public string PaymentMethod { get; set; } = string.Empty;

    // Navigation properties
    public Room Room { get; set; } = null!;
    public Guest Guest { get; set; } = null!;
    public ICollection<BookingModification> Modifications { get; set; } = new List<BookingModification>();
    public CheckInOut? CheckInOut { get; set; }
    public Rating? Rating { get; set; }
    public User User { get; set; } = null!;

    private static string GenerateBookingNumber()
    {
        return $"BK{DateTime.Now:yyyyMMddHHmmss}";
    }
}

public enum BookingStatus
{
    Pending,
    Confirmed,
    CheckedIn,
    CheckedOut,
    Cancelled
}

public enum PaymentStatus
{
    Pending,
    Partial,
    Completed,
    Refunded
} 