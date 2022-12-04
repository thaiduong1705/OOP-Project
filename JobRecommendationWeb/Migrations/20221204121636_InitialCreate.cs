using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobRecommendationWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CHUCVU",
                columns: table => new
                {
                    MaChucVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucVu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CHUCVU_PK", x => x.MaChucVu);
                });

            migrationBuilder.CreateTable(
                name: "HOSOCONGTY",
                columns: table => new
                {
                    MaCongTy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCongTy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuocTich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheDoDaiNgo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTaThem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HOSOCONGTY_PK", x => x.MaCongTy);
                });

            migrationBuilder.CreateTable(
                name: "KINANG",
                columns: table => new
                {
                    MaKiNang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKiNang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("KINANG_PK", x => x.MaKiNang);
                });

            migrationBuilder.CreateTable(
                name: "NHANVIEN",
                columns: table => new
                {
                    MaNhanVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tuoi = table.Column<int>(type: "int", nullable: true),
                    SDT = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("NHANVIEN_PK", x => x.MaNhanVien);
                });

            migrationBuilder.CreateTable(
                name: "THONGKE",
                columns: table => new
                {
                    MaThongKe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThoiGian = table.Column<DateTime>(type: "datetime", nullable: true),
                    SoBaiDangDaThem = table.Column<int>(type: "int", nullable: true),
                    SoCVDaThem = table.Column<int>(type: "int", nullable: true),
                    SoNguoiDaUngTuyen = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("THONGKE_PK", x => x.MaThongKe);
                });

            migrationBuilder.CreateTable(
                name: "TONGTHONGKE",
                columns: table => new
                {
                    MaTongTK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TongBaiDang = table.Column<int>(type: "int", nullable: true),
                    TongCV = table.Column<int>(type: "int", nullable: true),
                    TongNguoiDaUngTuyen = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TONGTHONGKE_PK", x => x.MaTongTK);
                });

            migrationBuilder.CreateTable(
                name: "UNGVIEN",
                columns: table => new
                {
                    MaUngVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tuoi = table.Column<int>(type: "int", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThamNien = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UNGVIEN_PK", x => x.MaUngVien);
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    MatKhau = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ChucVu = table.Column<int>(type: "int", nullable: true),
                    MaNhanVien = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TAIKHOAN_PK", x => x.MaTaiKhoan);
                    table.ForeignKey(
                        name: "FK_ChucVu_TK",
                        column: x => x.ChucVu,
                        principalTable: "CHUCVU",
                        principalColumn: "MaChucVu");
                    table.ForeignKey(
                        name: "FK_MaNhanVien_TK",
                        column: x => x.ChucVu,
                        principalTable: "NHANVIEN",
                        principalColumn: "MaNhanVien");
                });

            migrationBuilder.CreateTable(
                name: "CV",
                columns: table => new
                {
                    MaCV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaUngVien = table.Column<int>(type: "int", nullable: true),
                    LinkCV = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CV_PK", x => x.MaCV);
                    table.ForeignKey(
                        name: "FK_MaUngVien_CV",
                        column: x => x.MaUngVien,
                        principalTable: "UNGVIEN",
                        principalColumn: "MaUngVien");
                });

            migrationBuilder.CreateTable(
                name: "KINANG_UNGVIEN",
                columns: table => new
                {
                    MaKiNang = table.Column<int>(type: "int", nullable: false),
                    MaUngVien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("KINANG_UNGVIEN_PK", x => new { x.MaKiNang, x.MaUngVien });
                    table.ForeignKey(
                        name: "FK_MaKiNang_KNUV",
                        column: x => x.MaKiNang,
                        principalTable: "KINANG",
                        principalColumn: "MaKiNang");
                    table.ForeignKey(
                        name: "FK_MaUngVien_KNUV",
                        column: x => x.MaUngVien,
                        principalTable: "UNGVIEN",
                        principalColumn: "MaUngVien");
                });

            migrationBuilder.CreateTable(
                name: "BAIDANG",
                columns: table => new
                {
                    MaBaiDang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCongTy = table.Column<int>(type: "int", nullable: true),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViecLam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuongMin = table.Column<int>(type: "int", nullable: true),
                    LuongMax = table.Column<int>(type: "int", nullable: true),
                    ThamNien = table.Column<int>(type: "int", nullable: true),
                    WebsiteBaiGoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayDangBai = table.Column<DateTime>(type: "datetime", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BAIDANG_PK", x => x.MaBaiDang);
                    table.ForeignKey(
                        name: "FK_MaCongTy_BD",
                        column: x => x.MaCongTy,
                        principalTable: "HOSOCONGTY",
                        principalColumn: "MaCongTy");
                    table.ForeignKey(
                        name: "FK_MaTaiKhoan_BD",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TAIKHOAN",
                        principalColumn: "MaTaiKhoan");
                });

            migrationBuilder.CreateTable(
                name: "LICHSULAMVIEC",
                columns: table => new
                {
                    MaLSLV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: true),
                    CaLamViec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayLamViec = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("LICHSULAMVIEC_PK", x => x.MaLSLV);
                    table.ForeignKey(
                        name: "FK_MaTaiKhoan_LSLV",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TAIKHOAN",
                        principalColumn: "MaTaiKhoan");
                });

            migrationBuilder.CreateTable(
                name: "KINANG_BAIDANG",
                columns: table => new
                {
                    MaKiNang = table.Column<int>(type: "int", nullable: false),
                    MaBaiDang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("KINANG_BAIDANG_PK", x => new { x.MaKiNang, x.MaBaiDang });
                    table.ForeignKey(
                        name: "FK_MaBaiDang_KNBD",
                        column: x => x.MaBaiDang,
                        principalTable: "BAIDANG",
                        principalColumn: "MaBaiDang");
                    table.ForeignKey(
                        name: "FK_MaKiNang_KNBD",
                        column: x => x.MaKiNang,
                        principalTable: "KINANG",
                        principalColumn: "MaKiNang");
                });

            migrationBuilder.CreateTable(
                name: "PHIEUTOCAO",
                columns: table => new
                {
                    MaPhieuToCao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaBaiDang = table.Column<int>(type: "int", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "datetime", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PHIEUTOCAO_PK", x => x.MaPhieuToCao);
                    table.ForeignKey(
                        name: "FK_MaBaiDang_PTC",
                        column: x => x.MaBaiDang,
                        principalTable: "BAIDANG",
                        principalColumn: "MaBaiDang");
                });

            migrationBuilder.CreateTable(
                name: "UNGTUYEN",
                columns: table => new
                {
                    MaUngVien = table.Column<int>(type: "int", nullable: false),
                    MaBaiDang = table.Column<int>(type: "int", nullable: false),
                    NgayUngTuyen = table.Column<DateTime>(type: "datetime", nullable: true),
                    ChapThuan = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UNGTUYEN_PK", x => new { x.MaUngVien, x.MaBaiDang });
                    table.ForeignKey(
                        name: "FK_MaBaiDang_UT",
                        column: x => x.MaBaiDang,
                        principalTable: "BAIDANG",
                        principalColumn: "MaBaiDang");
                    table.ForeignKey(
                        name: "FK_MaUngVien_UT",
                        column: x => x.MaUngVien,
                        principalTable: "UNGVIEN",
                        principalColumn: "MaUngVien");
                });

            migrationBuilder.CreateTable(
                name: "CHITIETLAMVIEC",
                columns: table => new
                {
                    MaLslv = table.Column<int>(type: "int", nullable: false),
                    MaBaiDang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CHITIETLAMVIEC_PK", x => new { x.MaLslv, x.MaBaiDang });
                    table.ForeignKey(
                        name: "FK_MaBaiDang_CTLV",
                        column: x => x.MaBaiDang,
                        principalTable: "BAIDANG",
                        principalColumn: "MaBaiDang");
                    table.ForeignKey(
                        name: "FK_MaLSLV_CTLV",
                        column: x => x.MaLslv,
                        principalTable: "LICHSULAMVIEC",
                        principalColumn: "MaLSLV");
                });

            migrationBuilder.CreateTable(
                name: "PHIEUPHAT",
                columns: table => new
                {
                    MaPhieuPhat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieuToCao = table.Column<int>(type: "int", nullable: true),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: true),
                    SoTienPhat = table.Column<int>(type: "int", nullable: true),
                    ThoiGian = table.Column<DateTime>(type: "datetime", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PHIEUPHAT_PK", x => x.MaPhieuPhat);
                    table.ForeignKey(
                        name: "FK_MaPhieuToCao_PP",
                        column: x => x.MaPhieuToCao,
                        principalTable: "PHIEUTOCAO",
                        principalColumn: "MaPhieuToCao");
                    table.ForeignKey(
                        name: "FK_MaTaiKhoan_PP",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TAIKHOAN",
                        principalColumn: "MaTaiKhoan");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BAIDANG_MaCongTy",
                table: "BAIDANG",
                column: "MaCongTy");

            migrationBuilder.CreateIndex(
                name: "IX_BAIDANG_MaTaiKhoan",
                table: "BAIDANG",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETLAMVIEC_MaBaiDang",
                table: "CHITIETLAMVIEC",
                column: "MaBaiDang");

            migrationBuilder.CreateIndex(
                name: "IX_CV_MaUngVien",
                table: "CV",
                column: "MaUngVien");

            migrationBuilder.CreateIndex(
                name: "IX_KINANG_BAIDANG_MaBaiDang",
                table: "KINANG_BAIDANG",
                column: "MaBaiDang");

            migrationBuilder.CreateIndex(
                name: "IX_KINANG_UNGVIEN_MaUngVien",
                table: "KINANG_UNGVIEN",
                column: "MaUngVien");

            migrationBuilder.CreateIndex(
                name: "IX_LICHSULAMVIEC_MaTaiKhoan",
                table: "LICHSULAMVIEC",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_PHIEUPHAT_MaPhieuToCao",
                table: "PHIEUPHAT",
                column: "MaPhieuToCao");

            migrationBuilder.CreateIndex(
                name: "IX_PHIEUPHAT_MaTaiKhoan",
                table: "PHIEUPHAT",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_PHIEUTOCAO_MaBaiDang",
                table: "PHIEUTOCAO",
                column: "MaBaiDang");

            migrationBuilder.CreateIndex(
                name: "IX_TAIKHOAN_ChucVu",
                table: "TAIKHOAN",
                column: "ChucVu");

            migrationBuilder.CreateIndex(
                name: "IX_UNGTUYEN_MaBaiDang",
                table: "UNGTUYEN",
                column: "MaBaiDang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHITIETLAMVIEC");

            migrationBuilder.DropTable(
                name: "CV");

            migrationBuilder.DropTable(
                name: "KINANG_BAIDANG");

            migrationBuilder.DropTable(
                name: "KINANG_UNGVIEN");

            migrationBuilder.DropTable(
                name: "PHIEUPHAT");

            migrationBuilder.DropTable(
                name: "THONGKE");

            migrationBuilder.DropTable(
                name: "TONGTHONGKE");

            migrationBuilder.DropTable(
                name: "UNGTUYEN");

            migrationBuilder.DropTable(
                name: "LICHSULAMVIEC");

            migrationBuilder.DropTable(
                name: "KINANG");

            migrationBuilder.DropTable(
                name: "PHIEUTOCAO");

            migrationBuilder.DropTable(
                name: "UNGVIEN");

            migrationBuilder.DropTable(
                name: "BAIDANG");

            migrationBuilder.DropTable(
                name: "HOSOCONGTY");

            migrationBuilder.DropTable(
                name: "TAIKHOAN");

            migrationBuilder.DropTable(
                name: "CHUCVU");

            migrationBuilder.DropTable(
                name: "NHANVIEN");
        }
    }
}
