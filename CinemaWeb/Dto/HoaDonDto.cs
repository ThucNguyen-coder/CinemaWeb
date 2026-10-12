namespace CinemaWeb.Dto
{
    public class ChiTietVeDto
    {
        public int VeId { get; set; }
        public int GheId { get; set; }
        public string SoGhe { get; set; } = null!;
        public string LoaiGhe { get; set; } = null!;
        public decimal GiaVe { get; set; }
    }

    public class ChiTietBapNuocDto
    {
        public int BapNuocId { get; set; }
        public string TenCombo { get; set; } = null!;
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }

    public class HoaDonDto
    {
        public int HoaDonId { get; set; }
        public int NguoiDungId { get; set; }
        public string HoTenNguoiDung { get; set; } = null!;
        public string EmailNguoiDung { get; set; } = null!;
        public string SoDienThoai { get; set; } = "";
        public DateTime NgayDat { get; set; }
        public string TenPhim { get; set; } = null!;
        public string TenRap { get; set; } = null!;
        public string TenPhong { get; set; } = null!;
        public DateTime ThoiGianChieu { get; set; }
        public decimal TongTien { get; set; }
        public string PhuongThucThanhToan { get; set; } = null!;
        public string TrangThaiThanhToan { get; set; } = null!;
        public List<ChiTietVeDto> DanhSachVe { get; set; } = new List<ChiTietVeDto>();
        public List<ChiTietBapNuocDto> DanhSachBapNuoc { get; set; } = new List<ChiTietBapNuocDto>();
    }
}
