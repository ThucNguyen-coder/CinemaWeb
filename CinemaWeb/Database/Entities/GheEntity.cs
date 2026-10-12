using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("Ghe")]
    public class GheEntity
    {
        [Key]
        public int GheId { get; set; }

        public int PhongChieuId { get; set; }

        [Required]
        [MaxLength(10)]
        public string SoGhe { get; set; } = null!; // Ví dụ: A1, A2, B1...

        [Required]
        [MaxLength(50)]
        public string LoaiGhe { get; set; } = "Thuong"; // Thuong, VIP, Sweetbox

        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaGhePhuThu { get; set; } = 0; // Giá phụ thu ghế VIP hoặc Đôi

        [ForeignKey(nameof(PhongChieuId))]
        public PhongChieuEntity PhongChieu { get; set; } = null!;

        public ICollection<VeEntity> Ves { get; set; } = new List<VeEntity>();
    }
}
