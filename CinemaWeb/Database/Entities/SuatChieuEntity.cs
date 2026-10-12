using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("SuatChieu")]
    public class SuatChieuEntity
    {
        [Key]
        public int SuatChieuId { get; set; }

        public int PhimId { get; set; }

        public int PhongChieuId { get; set; }

        public DateTime ThoiGianBatDau { get; set; }

        public DateTime ThoiGianKetThuc { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaVeCoBan { get; set; } // Giá vé chuẩn của suất chiếu

        [ForeignKey(nameof(PhimId))]
        public PhimEntity Phim { get; set; } = null!;

        [ForeignKey(nameof(PhongChieuId))]
        public PhongChieuEntity PhongChieu { get; set; } = null!;

        public ICollection<VeEntity> Ves { get; set; } = new List<VeEntity>();
    }
}
