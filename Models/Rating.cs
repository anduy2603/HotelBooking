using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models;

public class Rating
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BookingId { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    [Range(1, 5)]
    public int RatingValue { get; set; }

    [StringLength(1000)]
    public string? Comment { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public List<string> Photos { get; set; } = new List<string>();

    public bool IsVerified { get; set; }

    [StringLength(1000)]
    public string? Response { get; set; }

    // Navigation properties
    public Booking Booking { get; set; } = null!;
    public Room Room { get; set; } = null!;
} 