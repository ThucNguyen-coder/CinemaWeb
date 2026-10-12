using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("NguoiDung")]
    public class NguoiDungEntity
    {
        [Key]
        public int NguoiDungId { get; set; }

        [Required]
        [MaxLength(100)]
        public string HoTen { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng định dạng")]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [MaxLength(20)]
        public string? SoDienThoai { get; set; }

        [Required]
        [MaxLength(255)]
        public string MatKhau { get; set; } = null!;

        [MaxLength(50)]
        public string VaiTro { get; set; } = "KhachHang"; // Admin, NhanVien, KhachHang

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public ICollection<HoaDonEntity> HoaDons { get; set; } = new List<HoaDonEntity>();
    }
}
