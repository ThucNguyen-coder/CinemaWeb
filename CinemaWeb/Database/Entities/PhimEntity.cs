using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("Phim")]
    public class PhimEntity
    {
        [Key]
        public int PhimId { get; set; }

        [Required]
        [MaxLength(250)]
        public string TenPhim { get; set; } = null!;

        [MaxLength(100)]
        public string? DaoDien { get; set; }

        [MaxLength(250)]
        public string? DienVien { get; set; }

        public int ThoiLuong { get; set; } // Thời lượng tính bằng phút

        public DateTime? NgayKhoiChieu { get; set; }

        [MaxLength(1000)]
        public string? MoTa { get; set; }

        [MaxLength(500)]
        public string? PosterUrl { get; set; }

        [MaxLength(500)]
        public string? TrailerUrl { get; set; }

        [MaxLength(50)]
        public string TrangThai { get; set; } = "DangChieu"; // DangChieu, SapChieu, NgungChieu

        public ICollection<PhimTheLoaiEntity> PhimTheLoais { get; set; } = new List<PhimTheLoaiEntity>();

        public ICollection<SuatChieuEntity> SuatChieus { get; set; } = new List<SuatChieuEntity>();
    }
}
