using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("Ve")]
    public class VeEntity
    {
        [Key]
        public int VeId { get; set; }

        public int HoaDonId { get; set; }

        public int SuatChieuId { get; set; }

        public int GheId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaVe { get; set; }

        [MaxLength(50)]
        public string TrangThaiVe { get; set; } = "DaDat"; // DaDat, DaHuy

        [ForeignKey(nameof(HoaDonId))]
        public HoaDonEntity HoaDon { get; set; } = null!;

        [ForeignKey(nameof(SuatChieuId))]
        public SuatChieuEntity SuatChieu { get; set; } = null!;

        [ForeignKey(nameof(GheId))]
        public GheEntity Ghe { get; set; } = null!;
    }
}
