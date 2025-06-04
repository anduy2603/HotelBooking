using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models;

public class BookingModification
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BookingId { get; set; }

    [Required]
    public DateTime ModifiedAt { get; set; } = DateTime.Now;

    [Required]
    [StringLength(100)]
    public string ModifiedBy { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Changes { get; set; } = string.Empty;

    // Navigation property
    public Booking Booking { get; set; } = null!;
} 