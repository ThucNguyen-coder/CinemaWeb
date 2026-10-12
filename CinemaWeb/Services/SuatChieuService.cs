using CinemaWeb.Database;
using CinemaWeb.Database.Entities;
using CinemaWeb.Dto;
using CinemaWeb.IServices;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Services
{
    public class SuatChieuService : ISuatChieuService
    {
        private readonly CinemaDbContext db;

        public SuatChieuService(CinemaDbContext context)
        {
            db = context;
        }

        public async Task<List<SuatChieuDto>> GetAllAsync()
        {
            return await db.SuatChieu
                .OrderBy(s => s.ThoiGianBatDau)
                .Select(s => new SuatChieuDto
                {
                    SuatChieuId = s.SuatChieuId,
                    PhimId = s.PhimId,
                    TenPhim = s.Phim.TenPhim,
                    PosterUrl = s.Phim.PosterUrl,
                    ThoiLuong = s.Phim.ThoiLuong,
                    PhongChieuId = s.PhongChieuId,
                    TenPhong = s.PhongChieu.TenPhong,
                    LoaiPhong = s.PhongChieu.LoaiPhong,
                    RapId = s.PhongChieu.RapId,
                    TenRap = s.PhongChieu.Rap.TenRap,
                    ThoiGianBatDau = s.ThoiGianBatDau,
                    ThoiGianKetThuc = s.ThoiGianKetThuc,
                    GiaVeCoBan = s.GiaVeCoBan
                })
                .ToListAsync();
        }

        public async Task<List<SuatChieuDto>> GetByPhimAsync(int phimId, DateTime? ngayChieu = null)
        {
            var query = db.SuatChieu
                .Where(s => s.PhimId == phimId);

            if (ngayChieu.HasValue)
            {
                var ngay = ngayChieu.Value.Date;
                query = query.Where(s => s.ThoiGianBatDau.Date == ngay);
            }

            return await query
                .OrderBy(s => s.ThoiGianBatDau)
                .Select(s => new SuatChieuDto
                {
                    SuatChieuId = s.SuatChieuId,
                    PhimId = s.PhimId,
                    TenPhim = s.Phim.TenPhim,
                    PosterUrl = s.Phim.PosterUrl,
                    ThoiLuong = s.Phim.ThoiLuong,
                    PhongChieuId = s.PhongChieuId,
                    TenPhong = s.PhongChieu.TenPhong,
                    LoaiPhong = s.PhongChieu.LoaiPhong,
                    RapId = s.PhongChieu.RapId,
                    TenRap = s.PhongChieu.Rap.TenRap,
                    ThoiGianBatDau = s.ThoiGianBatDau,
                    ThoiGianKetThuc = s.ThoiGianKetThuc,
                    GiaVeCoBan = s.GiaVeCoBan
                })
                .ToListAsync();
        }

        public async Task<List<SuatChieuDto>> GetByRapAsync(int rapId, DateTime? ngayChieu = null)
        {
            var query = db.SuatChieu
                .Where(s => s.PhongChieu.RapId == rapId);

            if (ngayChieu.HasValue)
            {
                var ngay = ngayChieu.Value.Date;
                query = query.Where(s => s.ThoiGianBatDau.Date == ngay);
            }

            return await query
                .OrderBy(s => s.ThoiGianBatDau)
                .Select(s => new SuatChieuDto
                {
                    SuatChieuId = s.SuatChieuId,
                    PhimId = s.PhimId,
                    TenPhim = s.Phim.TenPhim,
                    PosterUrl = s.Phim.PosterUrl,
                    ThoiLuong = s.Phim.ThoiLuong,
                    PhongChieuId = s.PhongChieuId,
                    TenPhong = s.PhongChieu.TenPhong,
                    LoaiPhong = s.PhongChieu.LoaiPhong,
                    RapId = s.PhongChieu.RapId,
                    TenRap = s.PhongChieu.Rap.TenRap,
                    ThoiGianBatDau = s.ThoiGianBatDau,
                    ThoiGianKetThuc = s.ThoiGianKetThuc,
                    GiaVeCoBan = s.GiaVeCoBan
                })
                .ToListAsync();
        }

        public async Task<SuatChieuDto?> GetByIdAsync(int suatChieuId)
        {
            return await db.SuatChieu
                .Where(s => s.SuatChieuId == suatChieuId)
                .Select(s => new SuatChieuDto
                {
                    SuatChieuId = s.SuatChieuId,
                    PhimId = s.PhimId,
                    TenPhim = s.Phim.TenPhim,
                    PosterUrl = s.Phim.PosterUrl,
                    ThoiLuong = s.Phim.ThoiLuong,
                    PhongChieuId = s.PhongChieuId,
                    TenPhong = s.PhongChieu.TenPhong,
                    LoaiPhong = s.PhongChieu.LoaiPhong,
                    RapId = s.PhongChieu.RapId,
                    TenRap = s.PhongChieu.Rap.TenRap,
                    ThoiGianBatDau = s.ThoiGianBatDau,
                    ThoiGianKetThuc = s.ThoiGianKetThuc,
                    GiaVeCoBan = s.GiaVeCoBan
                })
                .FirstOrDefaultAsync();
        }

        public async Task<SoDoGheSuatChieuDto?> GetSoDoGheAsync(int suatChieuId)
        {
            var suatChieu = await db.SuatChieu
                .Include(s => s.Phim)
                .Include(s => s.PhongChieu)
                .ThenInclude(pc => pc.Rap)
                .FirstOrDefaultAsync(s => s.SuatChieuId == suatChieuId);

            if (suatChieu == null)
            {
                return null;
            }

            // Lấy tất cả ghế thuộc phòng chiếu
            var danhSachGhe = await db.Ghe
                .Where(g => g.PhongChieuId == suatChieu.PhongChieuId)
                .OrderBy(g => g.SoGhe)
                .ToListAsync();

            // Lấy tập hợp Id các ghế đã có người đặt trong suất chiếu này
            var gheDaDatList = await db.Ve
                .Where(v => v.SuatChieuId == suatChieuId && v.TrangThaiVe != "DaHuy")
                .Select(v => v.GheId)
                .ToListAsync();
            var gheDaDatIds = gheDaDatList.ToHashSet();

            var soDoGhe = new SoDoGheSuatChieuDto
            {
                SuatChieuId = suatChieu.SuatChieuId,
                PhimId = suatChieu.PhimId,
                TenPhim = suatChieu.Phim.TenPhim,
                TenRap = suatChieu.PhongChieu.Rap.TenRap,
                TenPhong = suatChieu.PhongChieu.TenPhong,
                LoaiPhong = suatChieu.PhongChieu.LoaiPhong,
                ThoiGianBatDau = suatChieu.ThoiGianBatDau,
                GiaVeCoBan = suatChieu.GiaVeCoBan,
                DanhSachGhe = danhSachGhe.Select(g => new GheDto
                {
                    GheId = g.GheId,
                    PhongChieuId = g.PhongChieuId,
                    SoGhe = g.SoGhe,
                    LoaiGhe = g.LoaiGhe,
                    GiaGhePhuThu = g.GiaGhePhuThu,
                    TongGiaGhe = suatChieu.GiaVeCoBan + g.GiaGhePhuThu,
                    TrangThai = gheDaDatIds.Contains(g.GheId) ? "DaDat" : "Trong"
                }).ToList()
            };

            return soDoGhe;
        }

        public async Task<bool> AddAsync(SuatChieuAddDto req)
        {
            if (req.ThoiGianKetThuc <= req.ThoiGianBatDau)
            {
                return false;
            }

            // Kiểm tra xung đột lịch chiếu tại phòng chiếu này
            bool biTrungLich = await db.SuatChieu
                .AnyAsync(s => s.PhongChieuId == req.PhongChieuId &&
                               req.ThoiGianBatDau < s.ThoiGianKetThuc &&
                               req.ThoiGianKetThuc > s.ThoiGianBatDau);

            if (biTrungLich)
            {
                return false;
            }

            var newSuatChieu = new SuatChieuEntity
            {
                PhimId = req.PhimId,
                PhongChieuId = req.PhongChieuId,
                ThoiGianBatDau = req.ThoiGianBatDau,
                ThoiGianKetThuc = req.ThoiGianKetThuc,
                GiaVeCoBan = req.GiaVeCoBan
            };

            await db.SuatChieu.AddAsync(newSuatChieu);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int suatChieuId)
        {
            var suatChieu = await db.SuatChieu.FirstOrDefaultAsync(s => s.SuatChieuId == suatChieuId);
            if (suatChieu == null)
            {
                return false;
            }

            bool daCoVe = await db.Ve.AnyAsync(v => v.SuatChieuId == suatChieuId);
            if (daCoVe)
            {
                return false; // Không được xóa suất chiếu đã có khách đặt vé
            }

            db.SuatChieu.Remove(suatChieu);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
