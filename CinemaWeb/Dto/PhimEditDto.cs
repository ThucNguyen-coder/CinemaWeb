using System.ComponentModel.DataAnnotations;

namespace CinemaWeb.Dto
{
    public class PhimEditDto
    {
        [Required(ErrorMessage = "Mã phim không được để trống")]
        public int PhimId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên phim")]
        [MaxLength(250, ErrorMessage = "Tên phim tối đa 250 ký tự")]
        public string TenPhim { get; set; } = null!;

        [MaxLength(100, ErrorMessage = "Tên đạo diễn tối đa 100 ký tự")]
        public string? DaoDien { get; set; }

        [MaxLength(250, ErrorMessage = "Tên diễn viên tối đa 250 ký tự")]
        public string? DienVien { get; set; }

        [Range(1, 600, ErrorMessage = "Thời lượng phim phải từ 1 đến 600 phút")]
        public int ThoiLuong { get; set; }

        public DateTime? NgayKhoiChieu { get; set; }

        [MaxLength(1000, ErrorMessage = "Mô tả tối đa 1000 ký tự")]
        public string? MoTa { get; set; }

        [MaxLength(500, ErrorMessage = "Đường dẫn Poster tối đa 500 ký tự")]
        public string? PosterUrl { get; set; }

        [MaxLength(500, ErrorMessage = "Đường dẫn Trailer tối đa 500 ký tự")]
        public string? TrailerUrl { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trạng thái phim")]
        public string TrangThai { get; set; } = "DangChieu";

        public List<int> TheLoaiIds { get; set; } = new List<int>();
    }
}
