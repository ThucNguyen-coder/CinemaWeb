using CinemaWeb.Database;
using CinemaWeb.Database.Entities;
using CinemaWeb.Dto;
using CinemaWeb.IServices;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Services
{
    public class DatVeService : IDatVeService
    {
        private readonly CinemaDbContext db;

        public DatVeService(CinemaDbContext context)
        {
            db = context;
        }

        public async Task<KetQuaDatVeDto> DatVeAsync(DatVeRequestDto req)
        {
            if (req.GheIds == null || !req.GheIds.Any())
            {
                return new KetQuaDatVeDto { Success = false, Message = "Vui lòng chọn ít nhất 1 ghế để đặt vé!" };
            }

            // Bắt đầu một Database Transaction để đảm bảo tính toàn vẹn và chống trùng ghế tuyệt đối
            await using var transaction = await db.Database.BeginTransactionAsync();

            try
            {
                // 1. Kiểm tra Suất chiếu
                var suatChieu = await db.SuatChieu
                    .Include(s => s.Phim)
                    .Include(s => s.PhongChieu)
                    .ThenInclude(pc => pc.Rap)
                    .FirstOrDefaultAsync(s => s.SuatChieuId == req.SuatChieuId);

                if (suatChieu == null)
                {
                    return new KetQuaDatVeDto { Success = false, Message = "Suất chiếu không tồn tại!" };
                }

                if (suatChieu.ThoiGianBatDau <= DateTime.Now)
                {
                    return new KetQuaDatVeDto { Success = false, Message = "Suất chiếu này đã diễn ra, không thể đặt vé!" };
                }

                // 2. Kiểm tra Người dùng
                var nguoiDung = await db.NguoiDung.FirstOrDefaultAsync(u => u.NguoiDungId == req.NguoiDungId);
                if (nguoiDung == null)
                {
                    return new KetQuaDatVeDto { Success = false, Message = "Người dùng không tồn tại trong hệ thống!" };
                }

                // 3. Kiểm tra các ghế hợp lệ trong phòng chiếu
                var danhSachGheDuocChon = await db.Ghe
                    .Where(g => req.GheIds.Contains(g.GheId) && g.PhongChieuId == suatChieu.PhongChieuId)
                    .ToListAsync();

                if (danhSachGheDuocChon.Count != req.GheIds.Distinct().Count())
                {
                    return new KetQuaDatVeDto { Success = false, Message = "Một số ghế được chọn không tồn tại hoặc không thuộc phòng chiếu này!" };
                }

                // 4. CHỐNG TRÙNG GHẾ (Race Condition Check)
                // Kiểm tra xem trong suất chiếu này, các ghế được chọn đã có vé chưa
                var danhSachGheDaDat = await db.Ve
                    .Include(v => v.Ghe)
                    .Where(v => v.SuatChieuId == req.SuatChieuId && req.GheIds.Contains(v.GheId) && v.TrangThaiVe != "DaHuy")
                    .Select(v => v.Ghe.SoGhe)
                    .ToListAsync();

                if (danhSachGheDaDat.Any())
                {
                    await transaction.RollbackAsync();
                    return new KetQuaDatVeDto
                    {
                        Success = false,
                        Message = $"Rất tiếc! Ghế [{string.Join(", ", danhSachGheDaDat)}] vừa có người khác đặt trước. Vui lòng chọn ghế khác!"
                    };
                }

                // 5. Tính tiền vé
                decimal tongTienVe = 0;
                var danhSachVeMoi = new List<VeEntity>();
                var chiTietVeDtos = new List<ChiTietVeDto>();

                foreach (var ghe in danhSachGheDuocChon)
                {
                    decimal giaVe = suatChieu.GiaVeCoBan + ghe.GiaGhePhuThu;
                    tongTienVe += giaVe;

                    danhSachVeMoi.Add(new VeEntity
                    {
                        SuatChieuId = req.SuatChieuId,
                        GheId = ghe.GheId,
                        GiaVe = giaVe,
                        TrangThaiVe = "DaDat"
                    });

                    chiTietVeDtos.Add(new ChiTietVeDto
                    {
                        GheId = ghe.GheId,
                        SoGhe = ghe.SoGhe,
                        LoaiGhe = ghe.LoaiGhe,
                        GiaVe = giaVe
                    });
                }

                // 6. Tính tiền Bắp Nước (nếu có)
                decimal tongTienBapNuoc = 0;
                var danhSachBapNuocMoi = new List<ChiTietBapNuocEntity>();
                var chiTietBapNuocDtos = new List<ChiTietBapNuocDto>();

                if (req.BapNuocs != null && req.BapNuocs.Any())
                {
                    var bapNuocIds = req.BapNuocs.Select(b => b.BapNuocId).ToList();
                    var thongTinBapNuocs = await db.BapNuoc
                        .Where(b => bapNuocIds.Contains(b.BapNuocId))
                        .ToDictionaryAsync(b => b.BapNuocId);

                    foreach (var item in req.BapNuocs)
                    {
                        if (thongTinBapNuocs.TryGetValue(item.BapNuocId, out var bn))
                        {
                            decimal thanhTien = bn.Gia * item.SoLuong;
                            tongTienBapNuoc += thanhTien;

                            danhSachBapNuocMoi.Add(new ChiTietBapNuocEntity
                            {
                                BapNuocId = item.BapNuocId,
                                SoLuong = item.SoLuong,
                                DonGia = bn.Gia
                            });

                            chiTietBapNuocDtos.Add(new ChiTietBapNuocDto
                            {
                                BapNuocId = item.BapNuocId,
                                TenCombo = bn.TenCombo,
                                SoLuong = item.SoLuong,
                                DonGia = bn.Gia
                            });
                        }
                    }
                }

                // 7. Tạo Hóa đơn
                decimal tongTienHoaDon = tongTienVe + tongTienBapNuoc;
                var hoaDonMoi = new HoaDonEntity
                {
                    NguoiDungId = req.NguoiDungId,
                    NgayDat = DateTime.Now,
                    TongTien = tongTienHoaDon,
                    PhuongThucThanhToan = req.PhuongThucThanhToan,
                    TrangThaiThanhToan = "DaThanhToan"
                };

                await db.HoaDon.AddAsync(hoaDonMoi);
                await db.SaveChangesAsync(); // Lưu để sinh HoaDonId

                // 8. Gán HoaDonId vào danh sách Vé và Bắp nước
                foreach (var ve in danhSachVeMoi)
                {
                    ve.HoaDonId = hoaDonMoi.HoaDonId;
                }
                await db.Ve.AddRangeAsync(danhSachVeMoi);

                foreach (var bn in danhSachBapNuocMoi)
                {
                    bn.HoaDonId = hoaDonMoi.HoaDonId;
                }
                if (danhSachBapNuocMoi.Any())
                {
                    await db.ChiTietBapNuoc.AddRangeAsync(danhSachBapNuocMoi);
                }

                await db.SaveChangesAsync();

                // 9. Commit Transaction an toàn
                await transaction.CommitAsync();

                // Cập nhật lại VeId cho response DTO
                for (int i = 0; i < danhSachVeMoi.Count; i++)
                {
                    chiTietVeDtos[i].VeId = danhSachVeMoi[i].VeId;
                }

                var hoaDonDto = new HoaDonDto
                {
                    HoaDonId = hoaDonMoi.HoaDonId,
                    NguoiDungId = nguoiDung.NguoiDungId,
                    HoTenNguoiDung = nguoiDung.HoTen,
                    EmailNguoiDung = nguoiDung.Email,
                    SoDienThoai = nguoiDung.SoDienThoai ?? "",
                    NgayDat = hoaDonMoi.NgayDat,
                    TenPhim = suatChieu.Phim.TenPhim,
                    TenRap = suatChieu.PhongChieu.Rap.TenRap,
                    TenPhong = suatChieu.PhongChieu.TenPhong,
                    ThoiGianChieu = suatChieu.ThoiGianBatDau,
                    TongTien = hoaDonMoi.TongTien,
                    PhuongThucThanhToan = hoaDonMoi.PhuongThucThanhToan,
                    TrangThaiThanhToan = hoaDonMoi.TrangThaiThanhToan,
                    DanhSachVe = chiTietVeDtos,
                    DanhSachBapNuoc = chiTietBapNuocDtos
                };

                return new KetQuaDatVeDto
                {
                    Success = true,
                    Message = "Đặt vé xem phim thành công!",
                    HoaDon = hoaDonDto
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new KetQuaDatVeDto
                {
                    Success = false,
                    Message = $"Đã xảy ra lỗi trong quá trình đặt vé: {ex.Message}"
                };
            }
        }

        public async Task<HoaDonDto?> GetHoaDonByIdAsync(int hoaDonId)
        {
            var hoaDon = await db.HoaDon
                .AsSplitQuery()
                .Include(h => h.NguoiDung)
                .Include(h => h.Ves)
                    .ThenInclude(v => v.Ghe)
                .Include(h => h.Ves)
                    .ThenInclude(v => v.SuatChieu)
                    .ThenInclude(s => s.Phim)
                .Include(h => h.Ves)
                    .ThenInclude(v => v.SuatChieu)
                    .ThenInclude(s => s.PhongChieu)
                    .ThenInclude(pc => pc.Rap)
                .Include(h => h.ChiTietBapNuocs)
                    .ThenInclude(c => c.BapNuoc)
                .FirstOrDefaultAsync(h => h.HoaDonId == hoaDonId);

            if (hoaDon == null)
            {
                return null;
            }

            var veDauTien = hoaDon.Ves.FirstOrDefault();
            var suatChieu = veDauTien?.SuatChieu;

            return new HoaDonDto
            {
                HoaDonId = hoaDon.HoaDonId,
                NguoiDungId = hoaDon.NguoiDungId,
                HoTenNguoiDung = hoaDon.NguoiDung.HoTen,
                EmailNguoiDung = hoaDon.NguoiDung.Email,
                SoDienThoai = hoaDon.NguoiDung.SoDienThoai ?? "",
                NgayDat = hoaDon.NgayDat,
                TenPhim = suatChieu?.Phim?.TenPhim ?? "",
                TenRap = suatChieu?.PhongChieu?.Rap?.TenRap ?? "",
                TenPhong = suatChieu?.PhongChieu?.TenPhong ?? "",
                ThoiGianChieu = suatChieu?.ThoiGianBatDau ?? DateTime.MinValue,
                TongTien = hoaDon.TongTien,
                PhuongThucThanhToan = hoaDon.PhuongThucThanhToan,
                TrangThaiThanhToan = hoaDon.TrangThaiThanhToan,
                DanhSachVe = hoaDon.Ves.Select(v => new ChiTietVeDto
                {
                    VeId = v.VeId,
                    GheId = v.GheId,
                    SoGhe = v.Ghe.SoGhe,
                    LoaiGhe = v.Ghe.LoaiGhe,
                    GiaVe = v.GiaVe
                }).ToList(),
                DanhSachBapNuoc = hoaDon.ChiTietBapNuocs.Select(c => new ChiTietBapNuocDto
                {
                    BapNuocId = c.BapNuocId,
                    TenCombo = c.BapNuoc.TenCombo,
                    SoLuong = c.SoLuong,
                    DonGia = c.DonGia
                }).ToList()
            };
        }

        public async Task<List<HoaDonDto>> GetLichSuDatVeByUserIdAsync(int userId)
        {
            var danhSachHoaDon = await db.HoaDon
                .AsSplitQuery()
                .Include(h => h.NguoiDung)
                .Include(h => h.Ves)
                    .ThenInclude(v => v.Ghe)
                .Include(h => h.Ves)
                    .ThenInclude(v => v.SuatChieu)
                    .ThenInclude(s => s.Phim)
                .Include(h => h.Ves)
                    .ThenInclude(v => v.SuatChieu)
                    .ThenInclude(s => s.PhongChieu)
                    .ThenInclude(pc => pc.Rap)
                .Include(h => h.ChiTietBapNuocs)
                    .ThenInclude(c => c.BapNuoc)
                .Where(h => h.NguoiDungId == userId)
                .OrderByDescending(h => h.NgayDat)
                .ToListAsync();

            return danhSachHoaDon.Select(h =>
            {
                var veDauTien = h.Ves.FirstOrDefault();
                var suatChieu = veDauTien?.SuatChieu;

                return new HoaDonDto
                {
                    HoaDonId = h.HoaDonId,
                    NguoiDungId = h.NguoiDungId,
                    HoTenNguoiDung = h.NguoiDung.HoTen,
                    EmailNguoiDung = h.NguoiDung.Email,
                    SoDienThoai = h.NguoiDung.SoDienThoai ?? "",
                    NgayDat = h.NgayDat,
                    TenPhim = suatChieu?.Phim?.TenPhim ?? "",
                    TenRap = suatChieu?.PhongChieu?.Rap?.TenRap ?? "",
                    TenPhong = suatChieu?.PhongChieu?.TenPhong ?? "",
                    ThoiGianChieu = suatChieu?.ThoiGianBatDau ?? DateTime.MinValue,
                    TongTien = h.TongTien,
                    PhuongThucThanhToan = h.PhuongThucThanhToan,
                    TrangThaiThanhToan = h.TrangThaiThanhToan,
                    DanhSachVe = h.Ves.Select(v => new ChiTietVeDto
                    {
                        VeId = v.VeId,
                        GheId = v.GheId,
                        SoGhe = v.Ghe.SoGhe,
                        LoaiGhe = v.Ghe.LoaiGhe,
                        GiaVe = v.GiaVe
                    }).ToList(),
                    DanhSachBapNuoc = h.ChiTietBapNuocs.Select(c => new ChiTietBapNuocDto
                    {
                        BapNuocId = c.BapNuocId,
                        TenCombo = c.BapNuoc.TenCombo,
                        SoLuong = c.SoLuong,
                        DonGia = c.DonGia
                    }).ToList()
                };
            }).ToList();
        }

        public async Task<List<BapNuocDto>> GetDanhSachBapNuocAsync()
        {
            return await db.BapNuoc
                .Select(b => new BapNuocDto
                {
                    BapNuocId = b.BapNuocId,
                    TenCombo = b.TenCombo,
                    Gia = b.Gia,
                    HinhAnh = b.HinhAnh,
                    MoTa = b.MoTa
                })
                .ToListAsync();
        }
    }
}
