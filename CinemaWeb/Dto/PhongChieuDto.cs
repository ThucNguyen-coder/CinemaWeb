namespace CinemaWeb.Dto
{
    public class PhongChieuDto
    {
        public int PhongChieuId { get; set; }
        public int RapId { get; set; }
        public string TenRap { get; set; } = null!;
        public string TenPhong { get; set; } = null!;
        public string LoaiPhong { get; set; } = "2D";
        public int TongSoGhe { get; set; }
    }
}
