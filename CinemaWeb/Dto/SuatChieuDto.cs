namespace CinemaWeb.Dto
{
    public class SuatChieuDto
    {
        public int SuatChieuId { get; set; }
        public int PhimId { get; set; }
        public string TenPhim { get; set; } = null!;
        public string? PosterUrl { get; set; }
        public int ThoiLuong { get; set; }
        public int PhongChieuId { get; set; }
        public string TenPhong { get; set; } = null!;
        public string LoaiPhong { get; set; } = null!;
        public int RapId { get; set; }
        public string TenRap { get; set; } = null!;
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public decimal GiaVeCoBan { get; set; }
    }
}
