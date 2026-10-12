using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("PhongChieu")]
    public class PhongChieuEntity
    {
        [Key]
        public int PhongChieuId { get; set; }

        public int RapId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenPhong { get; set; } = null!;

        [MaxLength(50)]
        public string LoaiPhong { get; set; } = "2D"; // 2D, 3D, IMAX

        public int TongSoGhe { get; set; }

        [ForeignKey(nameof(RapId))]
        public RapEntity Rap { get; set; } = null!;

        public ICollection<GheEntity> Ghes { get; set; } = new List<GheEntity>();

        public ICollection<SuatChieuEntity> SuatChieus { get; set; } = new List<SuatChieuEntity>();
    }
}
