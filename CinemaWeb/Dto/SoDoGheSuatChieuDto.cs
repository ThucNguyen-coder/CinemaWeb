namespace CinemaWeb.Dto
{
    public class SoDoGheSuatChieuDto
    {
        public int SuatChieuId { get; set; }
        public int PhimId { get; set; }
        public string TenPhim { get; set; } = null!;
        public string TenRap { get; set; } = null!;
        public string TenPhong { get; set; } = null!;
        public string LoaiPhong { get; set; } = null!;
        public DateTime ThoiGianBatDau { get; set; }
        public decimal GiaVeCoBan { get; set; }
        public List<GheDto> DanhSachGhe { get; set; } = new List<GheDto>();
    }
}
