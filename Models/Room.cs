using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models;

public class Room
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string RoomNumber { get; set; } = string.Empty;

    [Required]
    public RoomType Type { get; set; }

    [Required]
    [Range(0, 10000)]
    public decimal Price { get; set; }

    [Required]
    public string BedType { get; set; } = string.Empty;

    [Required]
    public int MaxGuests { get; set; }

    [Required]
    public int Size { get; set; }

    public string MainImage { get; set; } = string.Empty;

    public List<string> Images { get; set; } = new List<string>();

    public bool IsAvailable { get; set; } = true;

    public string? Description { get; set; }

    public List<string> Amenities { get; set; } = new List<string>();

    [Column(TypeName = "decimal(18,2)")]
    public decimal SeasonalPrice { get; set; }

    public RoomStatus Status { get; set; } = RoomStatus.Available;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<RoomPrice> PriceHistory { get; set; } = new List<RoomPrice>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}

public enum RoomType
{
    Single,
    Double,
    Suite,
    Deluxe
}

public enum RoomStatus
{
    Available,
    Occupied,
    Cleaning,
    Maintenance,
    OutOfOrder
} 