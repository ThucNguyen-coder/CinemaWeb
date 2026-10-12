using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCinemaDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BapNuoc",
                columns: table => new
                {
                    BapNuocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCombo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BapNuoc", x => x.BapNuocId);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    NguoiDungId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.NguoiDungId);
                });

            migrationBuilder.CreateTable(
                name: "Phim",
                columns: table => new
                {
                    PhimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhim = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DaoDien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DienVien = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ThoiLuong = table.Column<int>(type: "int", nullable: false),
                    NgayKhoiChieu = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PosterUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrailerUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phim", x => x.PhimId);
                });

            migrationBuilder.CreateTable(
                name: "Rap",
                columns: table => new
                {
                    RapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenRap = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ThanhPho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rap", x => x.RapId);
                });

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    TheLoaiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTheLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoai", x => x.TheLoaiId);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    HoaDonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrangThaiThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.HoaDonId);
                    table.ForeignKey(
                        name: "FK_HoaDon_NguoiDung_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhongChieu",
                columns: table => new
                {
                    PhongChieuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RapId = table.Column<int>(type: "int", nullable: false),
                    TenPhong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LoaiPhong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TongSoGhe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongChieu", x => x.PhongChieuId);
                    table.ForeignKey(
                        name: "FK_PhongChieu_Rap_RapId",
                        column: x => x.RapId,
                        principalTable: "Rap",
                        principalColumn: "RapId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhimTheLoai",
                columns: table => new
                {
                    PhimTheLoaiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    TheLoaiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhimTheLoai", x => x.PhimTheLoaiId);
                    table.ForeignKey(
                        name: "FK_PhimTheLoai_Phim_PhimId",
                        column: x => x.PhimId,
                        principalTable: "Phim",
                        principalColumn: "PhimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhimTheLoai_TheLoai_TheLoaiId",
                        column: x => x.TheLoaiId,
                        principalTable: "TheLoai",
                        principalColumn: "TheLoaiId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietBapNuoc",
                columns: table => new
                {
                    ChiTietBapNuocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonId = table.Column<int>(type: "int", nullable: false),
                    BapNuocId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietBapNuoc", x => x.ChiTietBapNuocId);
                    table.ForeignKey(
                        name: "FK_ChiTietBapNuoc_BapNuoc_BapNuocId",
                        column: x => x.BapNuocId,
                        principalTable: "BapNuoc",
                        principalColumn: "BapNuocId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietBapNuoc_HoaDon_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDon",
                        principalColumn: "HoaDonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ghe",
                columns: table => new
                {
                    GheId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhongChieuId = table.Column<int>(type: "int", nullable: false),
                    SoGhe = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LoaiGhe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GiaGhePhuThu = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ghe", x => x.GheId);
                    table.ForeignKey(
                        name: "FK_Ghe_PhongChieu_PhongChieuId",
                        column: x => x.PhongChieuId,
                        principalTable: "PhongChieu",
                        principalColumn: "PhongChieuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuatChieu",
                columns: table => new
                {
                    SuatChieuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    PhongChieuId = table.Column<int>(type: "int", nullable: false),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiaVeCoBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuatChieu", x => x.SuatChieuId);
                    table.ForeignKey(
                        name: "FK_SuatChieu_Phim_PhimId",
                        column: x => x.PhimId,
                        principalTable: "Phim",
                        principalColumn: "PhimId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuatChieu_PhongChieu_PhongChieuId",
                        column: x => x.PhongChieuId,
                        principalTable: "PhongChieu",
                        principalColumn: "PhongChieuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ve",
                columns: table => new
                {
                    VeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonId = table.Column<int>(type: "int", nullable: false),
                    SuatChieuId = table.Column<int>(type: "int", nullable: false),
                    GheId = table.Column<int>(type: "int", nullable: false),
                    GiaVe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThaiVe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ve", x => x.VeId);
                    table.ForeignKey(
                        name: "FK_Ve_Ghe_GheId",
                        column: x => x.GheId,
                        principalTable: "Ghe",
                        principalColumn: "GheId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ve_HoaDon_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDon",
                        principalColumn: "HoaDonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ve_SuatChieu_SuatChieuId",
                        column: x => x.SuatChieuId,
                        principalTable: "SuatChieu",
                        principalColumn: "SuatChieuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BapNuoc",
                columns: new[] { "BapNuocId", "Gia", "HinhAnh", "MoTa", "TenCombo" },
                values: new object[,]
                {
                    { 1, 79000m, "https://images.unsplash.com/photo-1572177191856-3cde618dee1f?w=500", "1 Bắp rang bơ ngọt vừa + 1 Nước ngọt lớn 32oz", "Combo Solo" },
                    { 2, 109000m, "https://images.unsplash.com/photo-1585647347483-22b66260dfff?w=500", "1 Bắp rang vị phô mai đặc biệt lớn + 2 Nước ngọt lớn 32oz", "Combo Couple" },
                    { 3, 179000m, "https://images.unsplash.com/photo-1505686994434-e3cc5abf1330?w=500", "2 Bắp rang bơ cỡ đại + 4 Nước ngọt + 1 Phần Snack khoai tây", "Combo Family Đỉnh Cao" }
                });

            migrationBuilder.InsertData(
                table: "NguoiDung",
                columns: new[] { "NguoiDungId", "Email", "HoTen", "MatKhau", "NgayTao", "SoDienThoai", "VaiTro" },
                values: new object[,]
                {
                    { 1, "admin@cinema.com", "Quản Trị Viên", "Admin@123", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0901234567", "Admin" },
                    { 2, "thuc@gmail.com", "Nguyễn Huỳnh Duy Thức", "User@123", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0987654321", "KhachHang" }
                });

            migrationBuilder.InsertData(
                table: "Phim",
                columns: new[] { "PhimId", "DaoDien", "DienVien", "MoTa", "NgayKhoiChieu", "PosterUrl", "TenPhim", "ThoiLuong", "TrailerUrl", "TrangThai" },
                values: new object[,]
                {
                    { 1, "Trấn Thành", "Phương Anh Đào, Tuấn Trần, Trấn Thành, Hồng Đào", "Bộ phim xoay quanh số phận của Mai - một phụ nữ làm nghề massage trị liệu và tình yêu ngọt ngào nhưng đầy trắc trở với Dương.", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?w=500", "Mai", 131, "https://youtube.com/watch?v=sample1", "DangChieu" },
                    { 2, "Adam Wingard", "Rebecca Hall, Brian Tyree Henry, Dan Stevens", "Trận chiến bùng nổ giữa hai quái thú huyền thoại khi một thế lực nguy hiểm mới xuất hiện đe dọa sự tồn vong của Trái Đất.", new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1536440136628-849c177e76a1?w=500", "Godzilla x Kong: Đế Chế Mới", 115, "https://youtube.com/watch?v=sample2", "DangChieu" },
                    { 3, "Imai Kazuaki", "Wasabi Mizuta, Megumi Ohara", "Nhóm bạn Nobita cùng chú mèo máy Doraemon khám phá cuộc phiêu lưu âm nhạc kỳ thú để giải cứu thế giới khỏi sự tĩnh lặng vĩnh viễn.", new DateTime(2024, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1517604931442-7e0c8ed2963c?w=500", "Doraemon: Bản Giao Hưởng Địa Cầu", 115, "https://youtube.com/watch?v=sample3", "DangChieu" },
                    { 4, "Lý Hải", "Thanh Hiền, Trương Minh Cường, Đinh Y Nhung", "Câu chuyện gia đình xúc động về bà Hai 73 tuổi cùng 5 người con với những hoàn cảnh và góc nhìn cuộc sống khác nhau.", new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://images.unsplash.com/photo-1478720568477-152d9b164e26?w=500", "Lật Mặt 7: Một Điều Ước", 138, "https://youtube.com/watch?v=sample4", "SapChieu" }
                });

            migrationBuilder.InsertData(
                table: "Rap",
                columns: new[] { "RapId", "DiaChi", "SoDienThoai", "TenRap", "ThanhPho" },
                values: new object[,]
                {
                    { 1, "116 Nguyễn Du, Phường Bến Thành, Quận 1", "0283823456", "Cinema Star Nguyễn Du", "Hồ Chí Minh" },
                    { 2, "Khu Phố 2, Phường Long Bình, TP. Biên Hòa", "0251398765", "Cinema Star Biên Hòa", "Đồng Nai" }
                });

            migrationBuilder.InsertData(
                table: "TheLoai",
                columns: new[] { "TheLoaiId", "MoTa", "TenTheLoai" },
                values: new object[,]
                {
                    { 1, "Phim kịch tính, hành động mãn nhãn", "Hành Động" },
                    { 2, "Phim lãng mạn, tâm lý tình cảm sâu sắc", "Tình Cảm" },
                    { 3, "Phim gia đình, anime, hoạt họa đặc sắc", "Hoạt Hình" },
                    { 4, "Phim giật gân, rùng rợn, ly kỳ", "Kinh Dị" },
                    { 5, "Phim viễn tưởng, tương lai, kỹ xảo hoành tráng", "Khoa Học Viễn Tưởng" }
                });

            migrationBuilder.InsertData(
                table: "PhimTheLoai",
                columns: new[] { "PhimTheLoaiId", "PhimId", "TheLoaiId" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 1 },
                    { 3, 2, 5 },
                    { 4, 3, 3 },
                    { 5, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "PhongChieu",
                columns: new[] { "PhongChieuId", "LoaiPhong", "RapId", "TenPhong", "TongSoGhe" },
                values: new object[,]
                {
                    { 1, "2D", 1, "Cinema Hall 01 (Standard)", 20 },
                    { 2, "IMAX", 1, "Cinema Hall 02 (IMAX)", 20 },
                    { 3, "3D", 2, "Cinema Hall 01 (3D Laser)", 20 }
                });

            migrationBuilder.InsertData(
                table: "Ghe",
                columns: new[] { "GheId", "GiaGhePhuThu", "LoaiGhe", "PhongChieuId", "SoGhe" },
                values: new object[,]
                {
                    { 1, 0m, "Thuong", 1, "A1" },
                    { 2, 0m, "Thuong", 1, "A2" },
                    { 3, 0m, "Thuong", 1, "A3" },
                    { 4, 0m, "Thuong", 1, "A4" },
                    { 5, 0m, "Thuong", 1, "A5" },
                    { 6, 0m, "Thuong", 1, "B1" },
                    { 7, 0m, "Thuong", 1, "B2" },
                    { 8, 0m, "Thuong", 1, "B3" },
                    { 9, 0m, "Thuong", 1, "B4" },
                    { 10, 0m, "Thuong", 1, "B5" },
                    { 11, 15000m, "VIP", 1, "C1" },
                    { 12, 15000m, "VIP", 1, "C2" },
                    { 13, 15000m, "VIP", 1, "C3" },
                    { 14, 15000m, "VIP", 1, "C4" },
                    { 15, 15000m, "VIP", 1, "C5" },
                    { 16, 30000m, "Sweetbox", 1, "D1" },
                    { 17, 30000m, "Sweetbox", 1, "D2" },
                    { 18, 30000m, "Sweetbox", 1, "D3" },
                    { 19, 30000m, "Sweetbox", 1, "D4" },
                    { 20, 30000m, "Sweetbox", 1, "D5" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBapNuoc_BapNuocId",
                table: "ChiTietBapNuoc",
                column: "BapNuocId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBapNuoc_HoaDonId",
                table: "ChiTietBapNuoc",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_Ghe_PhongChieuId",
                table: "Ghe",
                column: "PhongChieuId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_NguoiDungId",
                table: "HoaDon",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_Email",
                table: "NguoiDung",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhimTheLoai_PhimId_TheLoaiId",
                table: "PhimTheLoai",
                columns: new[] { "PhimId", "TheLoaiId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhimTheLoai_TheLoaiId",
                table: "PhimTheLoai",
                column: "TheLoaiId");

            migrationBuilder.CreateIndex(
                name: "IX_PhongChieu_RapId",
                table: "PhongChieu",
                column: "RapId");

            migrationBuilder.CreateIndex(
                name: "IX_SuatChieu_PhimId",
                table: "SuatChieu",
                column: "PhimId");

            migrationBuilder.CreateIndex(
                name: "IX_SuatChieu_PhongChieuId",
                table: "SuatChieu",
                column: "PhongChieuId");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_GheId",
                table: "Ve",
                column: "GheId");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_HoaDonId",
                table: "Ve",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_SuatChieuId_GheId",
                table: "Ve",
                columns: new[] { "SuatChieuId", "GheId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietBapNuoc");

            migrationBuilder.DropTable(
                name: "PhimTheLoai");

            migrationBuilder.DropTable(
                name: "Ve");

            migrationBuilder.DropTable(
                name: "BapNuoc");

            migrationBuilder.DropTable(
                name: "TheLoai");

            migrationBuilder.DropTable(
                name: "Ghe");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "SuatChieu");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "Phim");

            migrationBuilder.DropTable(
                name: "PhongChieu");

            migrationBuilder.DropTable(
                name: "Rap");
        }
    }
}
