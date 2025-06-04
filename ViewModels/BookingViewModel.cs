using System.ComponentModel.DataAnnotations;
using HotelBooking.Models;

namespace HotelBooking.ViewModels;

public class BookingViewModel
{
    public BookingViewModel()
    {
        BookingNumber = string.Empty;
        SpecialRequests = string.Empty;
        Modifications = new List<BookingModification>();
        PaymentMethod = string.Empty;
    }

    public int Id { get; set; }

    [Required]
    public string BookingNumber { get; set; }

    [Required]
    public int RoomId { get; set; }
    public Room? Room { get; set; }

    [Required]
    [Display(Name = "Ngày nhận phòng")]
    public DateTime CheckInDate { get; set; }

    [Required]
    [Display(Name = "Ngày trả phòng")]
    public DateTime CheckOutDate { get; set; }

    [Required]
    [Display(Name = "Tổng tiền")]
    public decimal TotalPrice { get; set; }

    [Required]
    [Display(Name = "Tiền đặt cọc")]
    public decimal DepositAmount { get; set; }

    [Required]
    public BookingStatus Status { get; set; }

    [Required]
    public PaymentStatus PaymentStatus { get; set; }

    [Display(Name = "Ngày tạo")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Ngày cập nhật")]
    public DateTime? ModifiedAt { get; set; }

    public ICollection<BookingModification> Modifications { get; set; }

    public CheckInOut? CheckInOut { get; set; }

    public Rating? Rating { get; set; }

    [Display(Name = "Yêu cầu đặc biệt")]
    public string SpecialRequests { get; set; }

    [Display(Name = "Lý do hủy")]
    public string? CancellationReason { get; set; }

    [Display(Name = "Thời gian hủy")]
    public DateTime? CancelledAt { get; set; }

    [Display(Name = "Số người")]
    public int NumberOfGuests { get; set; }

    [Display(Name = "Phương thức thanh toán")]
    public string PaymentMethod { get; set; }
} 