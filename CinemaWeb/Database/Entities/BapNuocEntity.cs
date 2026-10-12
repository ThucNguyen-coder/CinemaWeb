using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("BapNuoc")]
    public class BapNuocEntity
    {
        [Key]
        public int BapNuocId { get; set; }

        [Required]
        [MaxLength(150)]
        public string TenCombo { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Gia { get; set; }

        [MaxLength(500)]
        public string? HinhAnh { get; set; }

        [MaxLength(500)]
        public string? MoTa { get; set; }

        public ICollection<ChiTietBapNuocEntity> ChiTietBapNuocs { get; set; } = new List<ChiTietBapNuocEntity>();
    }
}
