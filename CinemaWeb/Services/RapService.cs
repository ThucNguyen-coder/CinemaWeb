using CinemaWeb.Database;
using CinemaWeb.Dto;
using CinemaWeb.IServices;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Services
{
    public class RapService : IRapService
    {
        private readonly CinemaDbContext db;

        public RapService(CinemaDbContext context)
        {
            db = context;
        }

        public async Task<List<RapDto>> GetAllAsync()
        {
            var danhSachRap = await db.Rap
                .Select(r => new RapDto
                {
                    RapId = r.RapId,
                    TenRap = r.TenRap,
                    DiaChi = r.DiaChi,
                    ThanhPho = r.ThanhPho,
                    SoDienThoai = r.SoDienThoai,
                    SoLuongPhongChieu = r.PhongChieus.Count
                })
                .ToListAsync();

            return danhSachRap;
        }

        public async Task<RapDto?> GetByIdAsync(int rapId)
        {
            var rap = await db.Rap
                .Where(r => r.RapId == rapId)
                .Select(r => new RapDto
                {
                    RapId = r.RapId,
                    TenRap = r.TenRap,
                    DiaChi = r.DiaChi,
                    ThanhPho = r.ThanhPho,
                    SoDienThoai = r.SoDienThoai,
                    SoLuongPhongChieu = r.PhongChieus.Count
                })
                .FirstOrDefaultAsync();

            return rap;
        }

        public async Task<List<PhongChieuDto>> GetPhongChieuByRapIdAsync(int rapId)
        {
            var danhSachPhong = await db.PhongChieu
                .Where(pc => pc.RapId == rapId)
                .Select(pc => new PhongChieuDto
                {
                    PhongChieuId = pc.PhongChieuId,
                    RapId = pc.RapId,
                    TenRap = pc.Rap.TenRap,
                    TenPhong = pc.TenPhong,
                    LoaiPhong = pc.LoaiPhong,
                    TongSoGhe = pc.TongSoGhe
                })
                .ToListAsync();

            return danhSachPhong;
        }
    }
}
