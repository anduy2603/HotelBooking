using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class User
    {
        public User()
        {
            Bookings = new List<Booking>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [Required]
        public required string Role { get; set; } // "Admin" hoặc "User"

        public ICollection<Booking> Bookings { get; set; }
    }
} 