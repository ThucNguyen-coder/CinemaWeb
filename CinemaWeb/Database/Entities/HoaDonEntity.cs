using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaWeb.Database.Entities
{
    [Table("HoaDon")]
    public class HoaDonEntity
    {
        [Key]
        public int HoaDonId { get; set; }

        public int NguoiDungId { get; set; }

        public DateTime NgayDat { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TongTien { get; set; }

        [MaxLength(50)]
        public string PhuongThucThanhToan { get; set; } = "TienMat"; // TienMat, VNPAY, MoMo...

        [MaxLength(50)]
        public string TrangThaiThanhToan { get; set; } = "DaThanhToan"; // ChoThanhToan, DaThanhToan, DaHuy

        [ForeignKey(nameof(NguoiDungId))]
        public NguoiDungEntity NguoiDung { get; set; } = null!;

        public ICollection<VeEntity> Ves { get; set; } = new List<VeEntity>();

        public ICollection<ChiTietBapNuocEntity> ChiTietBapNuocs { get; set; } = new List<ChiTietBapNuocEntity>();
    }
}
