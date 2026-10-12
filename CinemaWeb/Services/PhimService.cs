using CinemaWeb.Database;
using CinemaWeb.Database.Entities;
using CinemaWeb.Dto;
using CinemaWeb.IServices;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Services
{
    public class PhimService : IPhimService
    {
        private readonly CinemaDbContext db;

        public PhimService(CinemaDbContext context)
        {
            db = context;
        }

        public async Task<List<PhimDto>> GetAllAsync(string? trangThai = null)
        {
            var query = db.Phim
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(p => p.TrangThai.ToLower() == trangThai.ToLower());
            }

            var danhSachPhim = await query
                .Select(p => new PhimDto
                {
                    PhimId = p.PhimId,
                    TenPhim = p.TenPhim,
                    DaoDien = p.DaoDien,
                    DienVien = p.DienVien,
                    ThoiLuong = p.ThoiLuong,
                    NgayKhoiChieu = p.NgayKhoiChieu,
                    MoTa = p.MoTa,
                    PosterUrl = p.PosterUrl,
                    TrailerUrl = p.TrailerUrl,
                    TrangThai = p.TrangThai,
                    TheLoais = p.PhimTheLoais.Select(pt => pt.TheLoai.TenTheLoai).ToList()
                })
                .ToListAsync();

            return danhSachPhim;
        }

        public async Task<PhimDto?> GetByIdAsync(int phimId)
        {
            var phim = await db.Phim
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .Where(p => p.PhimId == phimId)
                .Select(p => new PhimDto
                {
                    PhimId = p.PhimId,
                    TenPhim = p.TenPhim,
                    DaoDien = p.DaoDien,
                    DienVien = p.DienVien,
                    ThoiLuong = p.ThoiLuong,
                    NgayKhoiChieu = p.NgayKhoiChieu,
                    MoTa = p.MoTa,
                    PosterUrl = p.PosterUrl,
                    TrailerUrl = p.TrailerUrl,
                    TrangThai = p.TrangThai,
                    TheLoais = p.PhimTheLoais.Select(pt => pt.TheLoai.TenTheLoai).ToList()
                })
                .FirstOrDefaultAsync();

            return phim;
        }

        public async Task<bool> AddAsync(PhimAddDto req)
        {
            bool tonTaiPhim = await db.Phim
                .AnyAsync(p => p.TenPhim.ToLower() == req.TenPhim.Trim().ToLower());

            if (tonTaiPhim)
            {
                return false;
            }

            var newPhim = new PhimEntity
            {
                TenPhim = req.TenPhim.Trim(),
                DaoDien = req.DaoDien,
                DienVien = req.DienVien,
                ThoiLuong = req.ThoiLuong,
                NgayKhoiChieu = req.NgayKhoiChieu,
                MoTa = req.MoTa,
                PosterUrl = req.PosterUrl,
                TrailerUrl = req.TrailerUrl,
                TrangThai = req.TrangThai
            };

            await db.Phim.AddAsync(newPhim);
            await db.SaveChangesAsync();

            if (req.TheLoaiIds.Any())
            {
                foreach (var theLoaiId in req.TheLoaiIds)
                {
                    db.PhimTheLoai.Add(new PhimTheLoaiEntity
                    {
                        PhimId = newPhim.PhimId,
                        TheLoaiId = theLoaiId
                    });
                }
                await db.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> EditAsync(PhimEditDto req)
        {
            var phim = await db.Phim
                .Include(p => p.PhimTheLoais)
                .FirstOrDefaultAsync(p => p.PhimId == req.PhimId);

            if (phim == null)
            {
                return false;
            }

            phim.TenPhim = req.TenPhim.Trim();
            phim.DaoDien = req.DaoDien;
            phim.DienVien = req.DienVien;
            phim.ThoiLuong = req.ThoiLuong;
            phim.NgayKhoiChieu = req.NgayKhoiChieu;
            phim.MoTa = req.MoTa;
            phim.PosterUrl = req.PosterUrl;
            phim.TrailerUrl = req.TrailerUrl;
            phim.TrangThai = req.TrangThai;

            // Cập nhật lại danh sách thể loại
            db.PhimTheLoai.RemoveRange(phim.PhimTheLoais);
            foreach (var theLoaiId in req.TheLoaiIds)
            {
                db.PhimTheLoai.Add(new PhimTheLoaiEntity
                {
                    PhimId = phim.PhimId,
                    TheLoaiId = theLoaiId
                });
            }

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int phimId)
        {
            var phim = await db.Phim.FirstOrDefaultAsync(p => p.PhimId == phimId);
            if (phim == null)
            {
                return false;
            }

            // Kiểm tra xem phim đã có suất chiếu chưa
            bool coSuatChieu = await db.SuatChieu.AnyAsync(s => s.PhimId == phimId);
            if (coSuatChieu)
            {
                // Nếu đã có suất chiếu, chỉ chuyển trạng thái sang "NgungChieu"
                phim.TrangThai = "NgungChieu";
            }
            else
            {
                // Nếu chưa có suất chiếu nào, cho phép xóa khỏi DB
                db.Phim.Remove(phim);
            }

            await db.SaveChangesAsync();
            return true;
        }
    }
}
