namespace CinemaWeb.Dto
{
    public class RapDto
    {
        public int RapId { get; set; }
        public string TenRap { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string? ThanhPho { get; set; }
        public string? SoDienThoai { get; set; }
        public int SoLuongPhongChieu { get; set; }
    }
}
