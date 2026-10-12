using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("PhimTheLoai")]
    public class PhimTheLoaiEntity
    {
        [Key]
        public int PhimTheLoaiId { get; set; }

        public int PhimId { get; set; }

        public int TheLoaiId { get; set; }

        [ForeignKey(nameof(PhimId))]
        public PhimEntity Phim { get; set; } = null!;

        [ForeignKey(nameof(TheLoaiId))]
        public TheLoaiEntity TheLoai { get; set; } = null!;
    }
}
