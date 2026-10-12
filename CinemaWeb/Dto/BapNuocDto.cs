using System.ComponentModel.DataAnnotations;

namespace CinemaWeb.Dto
{
    public class BapNuocDto
    {
        public int BapNuocId { get; set; }
        public string TenCombo { get; set; } = null!;
        public decimal Gia { get; set; }
        public string? HinhAnh { get; set; }
        public string? MoTa { get; set; }
    }

    public class BapNuocOrderDto
    {
        [Required(ErrorMessage = "Mã bắp nước không được để trống")]
        public int BapNuocId { get; set; }

        [Range(1, 20, ErrorMessage = "Số lượng từ 1 đến 20")]
        public int SoLuong { get; set; }
    }
}
