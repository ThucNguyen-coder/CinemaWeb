using System.ComponentModel.DataAnnotations;

namespace CinemaWeb.Dto
{
    public class SuatChieuAddDto
    {
        [Required(ErrorMessage = "Vui lòng chọn phim")]
        public int PhimId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phòng chiếu")]
        public int PhongChieuId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thời gian bắt đầu")]
        public DateTime ThoiGianBatDau { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thời gian kết thúc")]
        public DateTime ThoiGianKetThuc { get; set; }

        [Range(10000, 1000000, ErrorMessage = "Giá vé cơ bản từ 10.000 đến 1.000.000 VNĐ")]
        public decimal GiaVeCoBan { get; set; }
    }
}
