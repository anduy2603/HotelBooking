using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models;

public class Room
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string RoomNumber { get; set; } = string.Empty;

    [Required]
    public RoomType Type { get; set; }

    [Required]
    [Range(0, 10000)]
    public decimal PricePerNight { get; set; }

    public bool IsAvailable { get; set; } = true;

    public string? Description { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

public enum RoomType
{
    Single,
    Double,
    Suite,
    Deluxe
} 