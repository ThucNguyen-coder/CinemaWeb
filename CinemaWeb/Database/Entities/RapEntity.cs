using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("Rap")]
    public class RapEntity
    {
        [Key]
        public int RapId { get; set; }

        [Required]
        [MaxLength(150)]
        public string TenRap { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string DiaChi { get; set; } = null!;

        [MaxLength(100)]
        public string? ThanhPho { get; set; }

        [MaxLength(20)]
        public string? SoDienThoai { get; set; }

        public ICollection<PhongChieuEntity> PhongChieus { get; set; } = new List<PhongChieuEntity>();
    }
}
