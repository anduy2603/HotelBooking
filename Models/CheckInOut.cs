using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models;

public class CheckInOut
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BookingId { get; set; }

    public DateTime? ActualCheckIn { get; set; }

    public DateTime? ActualCheckOut { get; set; }

    [StringLength(1000)]
    public string? CheckInNotes { get; set; }

    [StringLength(1000)]
    public string? CheckOutNotes { get; set; }

    public List<string> Damages { get; set; } = new List<string>();

    [Column(TypeName = "decimal(18,2)")]
    public decimal AdditionalCharges { get; set; }

    // Navigation property
    public Booking Booking { get; set; } = null!;
} 