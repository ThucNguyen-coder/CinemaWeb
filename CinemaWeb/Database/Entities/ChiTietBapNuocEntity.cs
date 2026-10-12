using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("ChiTietBapNuoc")]
    public class ChiTietBapNuocEntity
    {
        [Key]
        public int ChiTietBapNuocId { get; set; }

        public int HoaDonId { get; set; }

        public int BapNuocId { get; set; }

        public int SoLuong { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; }

        [ForeignKey(nameof(HoaDonId))]
        public HoaDonEntity HoaDon { get; set; } = null!;

        [ForeignKey(nameof(BapNuocId))]
        public BapNuocEntity BapNuoc { get; set; } = null!;
    }
}
