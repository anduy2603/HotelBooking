using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models;

public class EmailTemplate
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public string Body { get; set; } = string.Empty;

    [Required]
    public EmailType Type { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? ModifiedAt { get; set; }
}

public enum EmailType
{
    BookingConfirmation,
    CheckInReminder,
    CheckOutReminder,
    ThankYou,
    Cancellation
} 