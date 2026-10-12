namespace CinemaWeb.Dto
{
    public class PhimDto
    {
        public int PhimId { get; set; }
        public string TenPhim { get; set; } = null!;
        public string? DaoDien { get; set; }
        public string? DienVien { get; set; }
        public int ThoiLuong { get; set; }
        public DateTime? NgayKhoiChieu { get; set; }
        public string? MoTa { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public string TrangThai { get; set; } = null!;
        public List<string> TheLoais { get; set; } = new List<string>();
    }
}
