namespace CinemaWeb.Dto
{
    public class GheDto
    {
        public int GheId { get; set; }
        public int PhongChieuId { get; set; }
        public string SoGhe { get; set; } = null!;
        public string LoaiGhe { get; set; } = "Thuong";
        public decimal GiaGhePhuThu { get; set; }
        public decimal TongGiaGhe { get; set; } // GiaVeCoBan + GiaGhePhuThu (tiện lợi cho client tính tiền)
        public string TrangThai { get; set; } = "Trong"; // Trong hoặc DaDat
    }
}
