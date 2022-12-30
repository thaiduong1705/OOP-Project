using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Taikhoan
{
    public int MaTaiKhoan { get; set; }

    public string? TenDangNhap { get; set; }

    public string? MatKhau { get; set; }

    public int? MaChucVu { get; set; }

    public string? TenNhanVien { get; set; }

    public int? Tuoi { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public int? GioiTinh { get; set; }

    public virtual ICollection<Baidang> Baidangs { get; set; } = new List<Baidang>();

    public virtual ICollection<Lichsulamviec> Lichsulamviecs { get; set; } = new List<Lichsulamviec>();

    public virtual Chucvu? MaChucVuNavigation { get; set; }

    public virtual ICollection<Phieuphat> Phieuphats { get; } = new List<Phieuphat>();
}
