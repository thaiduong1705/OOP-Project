using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Taikhoan
{
    public int MaTaiKhoan { get; set; }

    public string? TenDangNhap { get; set; }

    public string? MatKhau { get; set; }

    public int? MaChucVu { get; set; }

    public int? MaNhanVien { get; set; }

    public virtual ICollection<Baidang> Baidangs { get; } = new List<Baidang>();

    public virtual ICollection<Lichsulamviec> Lichsulamviecs { get; } = new List<Lichsulamviec>();

    public virtual Chucvu? MaChucVuNavigation { get; set; }

    public virtual Nhanvien? MaNhanVienNavigation { get; set; }

    public virtual ICollection<Phieuphat> Phieuphats { get; } = new List<Phieuphat>();
}
