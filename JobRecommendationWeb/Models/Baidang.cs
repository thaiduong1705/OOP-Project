using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Baidang
{
    public int MaBaiDang { get; set; }

    public int? MaCongTy { get; set; }

    public string? TenCongViec { get; set; }

    public string? MoTa { get; set; }

    public int? LuongMin { get; set; }

    public int? LuongMax { get; set; }

    public int? ThamNien { get; set; }

    public string? WebsiteBaiGoc { get; set; }

    public DateTime? NgayDangBai { get; set; }

    public string? GhiChu { get; set; }

    public int? MaTaiKhoan { get; set; }

    public virtual Hosocongty? MaCongTyNavigation { get; set; }

    public virtual Taikhoan? MaTaiKhoanNavigation { get; set; }

    public virtual ICollection<Phieutocao> Phieutocaos { get; } = new List<Phieutocao>();

    public virtual ICollection<Ungtuyen> Ungtuyens { get; set; } = new List<Ungtuyen>();

    public virtual ICollection<Kinang> MaKiNangs { get; set; } = new List<Kinang>();

    public virtual ICollection<Lichsulamviec> MaLslvs { get; set; } = new List<Lichsulamviec>();
}
