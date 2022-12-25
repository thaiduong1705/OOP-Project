using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobRecommendationWeb.Models;

public partial class JobRecommendationContext : DbContext
{
    public JobRecommendationContext()
    {
    }

    public JobRecommendationContext(DbContextOptions<JobRecommendationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Baidang> Baidangs { get; set; }

    public virtual DbSet<Chucvu> Chucvus { get; set; }

    public virtual DbSet<Cv> Cvs { get; set; }

    public virtual DbSet<Hosocongty> Hosocongties { get; set; }

    public virtual DbSet<Kinang> Kinangs { get; set; }

    public virtual DbSet<Lichsulamviec> Lichsulamviecs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Phieuphat> Phieuphats { get; set; }

    public virtual DbSet<Phieutocao> Phieutocaos { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Thongke> Thongkes { get; set; }

    public virtual DbSet<Tongthongke> Tongthongkes { get; set; }

    public virtual DbSet<Ungtuyen> Ungtuyens { get; set; }

    public virtual DbSet<Ungvien> Ungviens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baidang>(entity =>
        {
            entity.HasKey(e => e.MaBaiDang).HasName("BAIDANG_PK");

            entity.ToTable("BAIDANG");

            entity.Property(e => e.NgayDangBai).HasColumnType("datetime");

            entity.HasOne(d => d.MaCongTyNavigation).WithMany(p => p.Baidangs)
                .HasForeignKey(d => d.MaCongTy)
                .HasConstraintName("FK_MaCongTy_BD");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany(p => p.Baidangs)
                .HasForeignKey(d => d.MaTaiKhoan)
                .HasConstraintName("FK_MaTaiKhoan_BD");
        });

        modelBuilder.Entity<Chucvu>(entity =>
        {
            entity.HasKey(e => e.MaChucVu).HasName("CHUCVU_PK");

            entity.ToTable("CHUCVU");
        });

        modelBuilder.Entity<Cv>(entity =>
        {
            entity.HasKey(e => e.MaCv).HasName("CV_PK");

            entity.ToTable("CV");

            entity.Property(e => e.MaCv).HasColumnName("MaCV");
            entity.Property(e => e.AnhCv).HasColumnName("AnhCV");

            entity.HasOne(d => d.MaUngVienNavigation).WithMany(p => p.Cvs)
                .HasForeignKey(d => d.MaUngVien)
                .HasConstraintName("FK_MaUngVien_CV");
        });

        modelBuilder.Entity<Hosocongty>(entity =>
        {
            entity.HasKey(e => e.MaCongTy).HasName("HOSOCONGTY_PK");

            entity.ToTable("HOSOCONGTY");
        });

        modelBuilder.Entity<Kinang>(entity =>
        {
            entity.HasKey(e => e.MaKiNang).HasName("KINANG_PK");

            entity.ToTable("KINANG");

            entity.HasMany(d => d.MaBaiDangs).WithMany(p => p.MaKiNangs)
                .UsingEntity<Dictionary<string, object>>(
                    "KinangBaidang",
                    r => r.HasOne<Baidang>().WithMany()
                        .HasForeignKey("MaBaiDang")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MaBaiDang_KNBD"),
                    l => l.HasOne<Kinang>().WithMany()
                        .HasForeignKey("MaKiNang")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MaKiNang_KNBD"),
                    j =>
                    {
                        j.HasKey("MaKiNang", "MaBaiDang").HasName("KINANG_BAIDANG_PK");
                        j.ToTable("KINANG_BAIDANG");
                    });

            entity.HasMany(d => d.MaUngViens).WithMany(p => p.MaKiNangs)
                .UsingEntity<Dictionary<string, object>>(
                    "KinangUngvien",
                    r => r.HasOne<Ungvien>().WithMany()
                        .HasForeignKey("MaUngVien")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MaUngVien_KNUV"),
                    l => l.HasOne<Kinang>().WithMany()
                        .HasForeignKey("MaKiNang")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MaKiNang_KNUV"),
                    j =>
                    {
                        j.HasKey("MaKiNang", "MaUngVien").HasName("KINANG_UNGVIEN_PK");
                        j.ToTable("KINANG_UNGVIEN");
                    });
        });

        modelBuilder.Entity<Lichsulamviec>(entity =>
        {
            entity.HasKey(e => e.MaLslv).HasName("LICHSULAMVIEC_PK");

            entity.ToTable("LICHSULAMVIEC");

            entity.Property(e => e.MaLslv).HasColumnName("MaLSLV");
            entity.Property(e => e.NgayLamViec).HasColumnType("datetime");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany(p => p.Lichsulamviecs)
                .HasForeignKey(d => d.MaTaiKhoan)
                .HasConstraintName("FK_MaTaiKhoan_LSLV");

            entity.HasMany(d => d.MaBaiDangs).WithMany(p => p.MaLslvs)
                .UsingEntity<Dictionary<string, object>>(
                    "Chitietlamviec",
                    r => r.HasOne<Baidang>().WithMany()
                        .HasForeignKey("MaBaiDang")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MaBaiDang_CTLV"),
                    l => l.HasOne<Lichsulamviec>().WithMany()
                        .HasForeignKey("MaLslv")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_MaLSLV_CTLV"),
                    j =>
                    {
                        j.HasKey("MaLslv", "MaBaiDang").HasName("CHITIETLAMVIEC_PK");
                        j.ToTable("CHITIETLAMVIEC");
                    });
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("NHANVIEN_PK");

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SDT");
        });

        modelBuilder.Entity<Phieuphat>(entity =>
        {
            entity.HasKey(e => e.MaPhieuPhat).HasName("PHIEUPHAT_PK");

            entity.ToTable("PHIEUPHAT");

            entity.Property(e => e.ThoiGian).HasColumnType("datetime");

            entity.HasOne(d => d.MaPhieuToCaoNavigation).WithMany(p => p.Phieuphats)
                .HasForeignKey(d => d.MaPhieuToCao)
                .HasConstraintName("FK_MaPhieuToCao_PP");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany(p => p.Phieuphats)
                .HasForeignKey(d => d.MaTaiKhoan)
                .HasConstraintName("FK_MaTaiKhoan_PP");
        });

        modelBuilder.Entity<Phieutocao>(entity =>
        {
            entity.HasKey(e => e.MaPhieuToCao).HasName("PHIEUTOCAO_PK");

            entity.ToTable("PHIEUTOCAO");

            entity.Property(e => e.ThoiGian).HasColumnType("datetime");

            entity.HasOne(d => d.MaBaiDangNavigation).WithMany(p => p.Phieutocaos)
                .HasForeignKey(d => d.MaBaiDang)
                .HasConstraintName("FK_MaBaiDang_PTC");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan).HasName("TAIKHOAN_PK");

            entity.ToTable("TAIKHOAN");

            entity.Property(e => e.MatKhau).IsUnicode(false);
            entity.Property(e => e.TenDangNhap).IsUnicode(false);

            entity.HasOne(d => d.ChucVuNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.ChucVu)
                .HasConstraintName("FK_ChucVu_TK");

            entity.HasOne(d => d.ChucVu1).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.ChucVu)
                .HasConstraintName("FK_MaNhanVien_TK");
        });

        modelBuilder.Entity<Thongke>(entity =>
        {
            entity.HasKey(e => e.MaThongKe).HasName("THONGKE_PK");

            entity.ToTable("THONGKE");

            entity.Property(e => e.SoCvdaThem).HasColumnName("SoCVDaThem");
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");
        });

        modelBuilder.Entity<Tongthongke>(entity =>
        {
            entity.HasKey(e => e.MaTongTk).HasName("TONGTHONGKE_PK");

            entity.ToTable("TONGTHONGKE");

            entity.Property(e => e.MaTongTk).HasColumnName("MaTongTK");
            entity.Property(e => e.TongCv).HasColumnName("TongCV");
        });

        modelBuilder.Entity<Ungtuyen>(entity =>
        {
            entity.HasKey(e => new { e.MaUngVien, e.MaBaiDang }).HasName("UNGTUYEN_PK");

            entity.ToTable("UNGTUYEN");

            entity.Property(e => e.NgayUngTuyen).HasColumnType("datetime");

            entity.HasOne(d => d.MaBaiDangNavigation).WithMany(p => p.Ungtuyens)
                .HasForeignKey(d => d.MaBaiDang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaBaiDang_UT");

            entity.HasOne(d => d.MaUngVienNavigation).WithMany(p => p.Ungtuyens)
                .HasForeignKey(d => d.MaUngVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaUngVien_UT");
        });

        modelBuilder.Entity<Ungvien>(entity =>
        {
            entity.HasKey(e => e.MaUngVien).HasName("UNGVIEN_PK");

            entity.ToTable("UNGVIEN");

            entity.Property(e => e.Sdt).HasColumnName("SDT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
