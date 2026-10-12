using CinemaWeb.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaWeb.Database
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
        }

        public DbSet<NguoiDungEntity> NguoiDung => Set<NguoiDungEntity>();
        public DbSet<TheLoaiEntity> TheLoai => Set<TheLoaiEntity>();
        public DbSet<PhimEntity> Phim => Set<PhimEntity>();
        public DbSet<PhimTheLoaiEntity> PhimTheLoai => Set<PhimTheLoaiEntity>();
        public DbSet<RapEntity> Rap => Set<RapEntity>();
        public DbSet<PhongChieuEntity> PhongChieu => Set<PhongChieuEntity>();
        public DbSet<GheEntity> Ghe => Set<GheEntity>();
        public DbSet<SuatChieuEntity> SuatChieu => Set<SuatChieuEntity>();
        public DbSet<HoaDonEntity> HoaDon => Set<HoaDonEntity>();
        public DbSet<VeEntity> Ve => Set<VeEntity>();
        public DbSet<BapNuocEntity> BapNuoc => Set<BapNuocEntity>();
        public DbSet<ChiTietBapNuocEntity> ChiTietBapNuoc => Set<ChiTietBapNuocEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. UNIQUE INDEX
            // Chống đặt trùng ghế trong cùng một suất chiếu
            modelBuilder.Entity<VeEntity>()
                .HasIndex(v => new { v.SuatChieuId, v.GheId })
                .IsUnique();

            // Khóa duy nhất cho liên kết Phim và Thể loại
            modelBuilder.Entity<PhimTheLoaiEntity>()
                .HasIndex(pt => new { pt.PhimId, pt.TheLoaiId })
                .IsUnique();

            // Unique Email người dùng
            modelBuilder.Entity<NguoiDungEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // 2. CẤU HÌNH DELETE BEHAVIOR (Tránh lỗi Multiple Cascade Paths trong SQL Server)
            modelBuilder.Entity<VeEntity>()
                .HasOne(v => v.SuatChieu)
                .WithMany(s => s.Ves)
                .HasForeignKey(v => v.SuatChieuId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VeEntity>()
                .HasOne(v => v.Ghe)
                .WithMany(g => g.Ves)
                .HasForeignKey(v => v.GheId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VeEntity>()
                .HasOne(v => v.HoaDon)
                .WithMany(h => h.Ves)
                .HasForeignKey(v => v.HoaDonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HoaDonEntity>()
                .HasOne(h => h.NguoiDung)
                .WithMany(u => u.HoaDons)
                .HasForeignKey(h => h.NguoiDungId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChiTietBapNuocEntity>()
                .HasOne(c => c.BapNuoc)
                .WithMany(b => b.ChiTietBapNuocs)
                .HasForeignKey(c => c.BapNuocId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChiTietBapNuocEntity>()
                .HasOne(c => c.HoaDon)
                .WithMany(h => h.ChiTietBapNuocs)
                .HasForeignKey(c => c.HoaDonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SuatChieuEntity>()
                .HasOne(s => s.Phim)
                .WithMany(p => p.SuatChieus)
                .HasForeignKey(s => s.PhimId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SuatChieuEntity>()
                .HasOne(s => s.PhongChieu)
                .WithMany(pc => pc.SuatChieus)
                .HasForeignKey(s => s.PhongChieuId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhongChieuEntity>()
                .HasOne(pc => pc.Rap)
                .WithMany(r => r.PhongChieus)
                .HasForeignKey(pc => pc.RapId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhimTheLoaiEntity>()
                .HasOne(pt => pt.TheLoai)
                .WithMany(t => t.PhimTheLoais)
                .HasForeignKey(pt => pt.TheLoaiId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. SEED DỮ LIỆU MẪU (HAS DATA)
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Thể loại
            modelBuilder.Entity<TheLoaiEntity>().HasData(
                new TheLoaiEntity { TheLoaiId = 1, TenTheLoai = "Hành Động", MoTa = "Phim kịch tính, hành động mãn nhãn" },
                new TheLoaiEntity { TheLoaiId = 2, TenTheLoai = "Tình Cảm", MoTa = "Phim lãng mạn, tâm lý tình cảm sâu sắc" },
                new TheLoaiEntity { TheLoaiId = 3, TenTheLoai = "Hoạt Hình", MoTa = "Phim gia đình, anime, hoạt họa đặc sắc" },
                new TheLoaiEntity { TheLoaiId = 4, TenTheLoai = "Kinh Dị", MoTa = "Phim giật gân, rùng rợn, ly kỳ" },
                new TheLoaiEntity { TheLoaiId = 5, TenTheLoai = "Khoa Học Viễn Tưởng", MoTa = "Phim viễn tưởng, tương lai, kỹ xảo hoành tráng" }
            );

            // Seed Người dùng mẫu
            modelBuilder.Entity<NguoiDungEntity>().HasData(
                new NguoiDungEntity
                {
                    NguoiDungId = 1,
                    HoTen = "Quản Trị Viên",
                    Email = "admin@cinema.com",
                    SoDienThoai = "0901234567",
                    MatKhau = "Admin@123",
                    VaiTro = "Admin",
                    NgayTao = new DateTime(2025, 1, 1)
                },
                new NguoiDungEntity
                {
                    NguoiDungId = 2,
                    HoTen = "Nguyễn Huỳnh Duy Thức",
                    Email = "thuc@gmail.com",
                    SoDienThoai = "0987654321",
                    MatKhau = "User@123",
                    VaiTro = "KhachHang",
                    NgayTao = new DateTime(2025, 1, 1)
                }
            );

            // Seed Rạp
            modelBuilder.Entity<RapEntity>().HasData(
                new RapEntity
                {
                    RapId = 1,
                    TenRap = "Cinema Star Nguyễn Du",
                    DiaChi = "116 Nguyễn Du, Phường Bến Thành, Quận 1",
                    ThanhPho = "Hồ Chí Minh",
                    SoDienThoai = "0283823456"
                },
                new RapEntity
                {
                    RapId = 2,
                    TenRap = "Cinema Star Biên Hòa",
                    DiaChi = "Khu Phố 2, Phường Long Bình, TP. Biên Hòa",
                    ThanhPho = "Đồng Nai",
                    SoDienThoai = "0251398765"
                }
            );

            // Seed Phòng chiếu
            modelBuilder.Entity<PhongChieuEntity>().HasData(
                new PhongChieuEntity { PhongChieuId = 1, RapId = 1, TenPhong = "Cinema Hall 01 (Standard)", LoaiPhong = "2D", TongSoGhe = 20 },
                new PhongChieuEntity { PhongChieuId = 2, RapId = 1, TenPhong = "Cinema Hall 02 (IMAX)", LoaiPhong = "IMAX", TongSoGhe = 20 },
                new PhongChieuEntity { PhongChieuId = 3, RapId = 2, TenPhong = "Cinema Hall 01 (3D Laser)", LoaiPhong = "3D", TongSoGhe = 20 }
            );

            // Seed Ghế cho Phòng 1 (20 ghế: Hàng A, B là Thường, Hàng C là VIP, Hàng D là VIP/Sweetbox)
            var danhSachGhe = new List<GheEntity>();
            int gheIdCounter = 1;

            string[] hangGhe = { "A", "B", "C", "D" };
            foreach (var hang in hangGhe)
            {
                for (int so = 1; so <= 5; so++)
                {
                    string loai = (hang == "A" || hang == "B") ? "Thuong" : (hang == "C" ? "VIP" : "Sweetbox");
                    decimal phuThu = loai == "VIP" ? 15000m : (loai == "Sweetbox" ? 30000m : 0m);

                    danhSachGhe.Add(new GheEntity
                    {
                        GheId = gheIdCounter++,
                        PhongChieuId = 1,
                        SoGhe = $"{hang}{so}",
                        LoaiGhe = loai,
                        GiaGhePhuThu = phuThu
                    });
                }
            }
            modelBuilder.Entity<GheEntity>().HasData(danhSachGhe);

            // Seed Phim
            modelBuilder.Entity<PhimEntity>().HasData(
                new PhimEntity
                {
                    PhimId = 1,
                    TenPhim = "Mai",
                    DaoDien = "Trấn Thành",
                    DienVien = "Phương Anh Đào, Tuấn Trần, Trấn Thành, Hồng Đào",
                    ThoiLuong = 131,
                    NgayKhoiChieu = new DateTime(2024, 2, 10),
                    MoTa = "Bộ phim xoay quanh số phận của Mai - một phụ nữ làm nghề massage trị liệu và tình yêu ngọt ngào nhưng đầy trắc trở với Dương.",
                    PosterUrl = "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?w=500",
                    TrailerUrl = "https://youtube.com/watch?v=sample1",
                    TrangThai = "DangChieu"
                },
                new PhimEntity
                {
                    PhimId = 2,
                    TenPhim = "Godzilla x Kong: Đế Chế Mới",
                    DaoDien = "Adam Wingard",
                    DienVien = "Rebecca Hall, Brian Tyree Henry, Dan Stevens",
                    ThoiLuong = 115,
                    NgayKhoiChieu = new DateTime(2024, 3, 29),
                    MoTa = "Trận chiến bùng nổ giữa hai quái thú huyền thoại khi một thế lực nguy hiểm mới xuất hiện đe dọa sự tồn vong của Trái Đất.",
                    PosterUrl = "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=500",
                    TrailerUrl = "https://youtube.com/watch?v=sample2",
                    TrangThai = "DangChieu"
                },
                new PhimEntity
                {
                    PhimId = 3,
                    TenPhim = "Doraemon: Bản Giao Hưởng Địa Cầu",
                    DaoDien = "Imai Kazuaki",
                    DienVien = "Wasabi Mizuta, Megumi Ohara",
                    ThoiLuong = 115,
                    NgayKhoiChieu = new DateTime(2024, 5, 24),
                    MoTa = "Nhóm bạn Nobita cùng chú mèo máy Doraemon khám phá cuộc phiêu lưu âm nhạc kỳ thú để giải cứu thế giới khỏi sự tĩnh lặng vĩnh viễn.",
                    PosterUrl = "https://images.unsplash.com/photo-1517604931442-7e0c8ed2963c?w=500",
                    TrailerUrl = "https://youtube.com/watch?v=sample3",
                    TrangThai = "DangChieu"
                },
                new PhimEntity
                {
                    PhimId = 4,
                    TenPhim = "Lật Mặt 7: Một Điều Ước",
                    DaoDien = "Lý Hải",
                    DienVien = "Thanh Hiền, Trương Minh Cường, Đinh Y Nhung",
                    ThoiLuong = 138,
                    NgayKhoiChieu = new DateTime(2024, 4, 26),
                    MoTa = "Câu chuyện gia đình xúc động về bà Hai 73 tuổi cùng 5 người con với những hoàn cảnh và góc nhìn cuộc sống khác nhau.",
                    PosterUrl = "https://images.unsplash.com/photo-1478720568477-152d9b164e26?w=500",
                    TrailerUrl = "https://youtube.com/watch?v=sample4",
                    TrangThai = "SapChieu"
                }
            );

            // Seed Phim - Thể loại
            modelBuilder.Entity<PhimTheLoaiEntity>().HasData(
                new PhimTheLoaiEntity { PhimTheLoaiId = 1, PhimId = 1, TheLoaiId = 2 }, // Mai - Tình Cảm
                new PhimTheLoaiEntity { PhimTheLoaiId = 2, PhimId = 2, TheLoaiId = 1 }, // Godzilla - Hành Động
                new PhimTheLoaiEntity { PhimTheLoaiId = 3, PhimId = 2, TheLoaiId = 5 }, // Godzilla - Khoa Học Viễn Tưởng
                new PhimTheLoaiEntity { PhimTheLoaiId = 4, PhimId = 3, TheLoaiId = 3 }, // Doraemon - Hoạt Hình
                new PhimTheLoaiEntity { PhimTheLoaiId = 5, PhimId = 4, TheLoaiId = 2 }  // Lật Mặt 7 - Tình Cảm
            );

            // Seed Bắp Nước
            modelBuilder.Entity<BapNuocEntity>().HasData(
                new BapNuocEntity
                {
                    BapNuocId = 1,
                    TenCombo = "Combo Solo",
                    Gia = 79000m,
                    HinhAnh = "https://images.unsplash.com/photo-1572177191856-3cde618dee1f?w=500",
                    MoTa = "1 Bắp rang bơ ngọt vừa + 1 Nước ngọt lớn 32oz"
                },
                new BapNuocEntity
                {
                    BapNuocId = 2,
                    TenCombo = "Combo Couple",
                    Gia = 109000m,
                    HinhAnh = "https://images.unsplash.com/photo-1585647347483-22b66260dfff?w=500",
                    MoTa = "1 Bắp rang vị phô mai đặc biệt lớn + 2 Nước ngọt lớn 32oz"
                },
                new BapNuocEntity
                {
                    BapNuocId = 3,
                    TenCombo = "Combo Family Đỉnh Cao",
                    Gia = 179000m,
                    HinhAnh = "https://images.unsplash.com/photo-1505686994434-e3cc5abf1330?w=500",
                    MoTa = "2 Bắp rang bơ cỡ đại + 4 Nước ngọt + 1 Phần Snack khoai tây"
                }
            );
        }
    }
}
