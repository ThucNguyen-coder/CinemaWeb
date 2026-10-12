using System.ComponentModel.DataAnnotations;

namespace CinemaWeb.Dto
{
    public class DatVeRequestDto
    {
        [Required(ErrorMessage = "Mã người dùng không được để trống")]
        public int NguoiDungId { get; set; }

        [Required(ErrorMessage = "Mã suất chiếu không được để trống")]
        public int SuatChieuId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh sách ghế")]
        [MinLength(1, ErrorMessage = "Vui lòng chọn ít nhất 1 ghế")]
        public List<int> GheIds { get; set; } = new List<int>();

        public List<BapNuocOrderDto>? BapNuocs { get; set; }

        public string PhuongThucThanhToan { get; set; } = "TienMat"; // TienMat, VNPAY, MoMo
    }

    public class KetQuaDatVeDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public HoaDonDto? HoaDon { get; set; }
    }
}
