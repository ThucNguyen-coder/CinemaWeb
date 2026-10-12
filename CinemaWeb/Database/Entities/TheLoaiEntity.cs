using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("TheLoai")]
    public class TheLoaiEntity
    {
        [Key]
        public int TheLoaiId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenTheLoai { get; set; } = null!;

        [MaxLength(500)]
        public string? MoTa { get; set; }

        public ICollection<PhimTheLoaiEntity> PhimTheLoais { get; set; } = new List<PhimTheLoaiEntity>();
    }
}
