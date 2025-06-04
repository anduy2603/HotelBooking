using System.ComponentModel.DataAnnotations;

namespace HotelBooking.ViewModels
{
    public class RatingViewModel
    {
        public RatingViewModel()
        {
            Comment = string.Empty;
        }

        public int BookingId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn số sao đánh giá")]
        [Range(1, 5, ErrorMessage = "Đánh giá phải từ 1 đến 5 sao")]
        [Display(Name = "Đánh giá")]
        public int Score { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nhận xét")]
        [StringLength(500, ErrorMessage = "Nhận xét không được vượt quá 500 ký tự")]
        [Display(Name = "Nhận xét")]
        public string Comment { get; set; }
    }
} 