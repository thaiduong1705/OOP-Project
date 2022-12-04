using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Phieuphat
{
    public int MaPhieuPhat { get; set; }

    public int? MaPhieuToCao { get; set; }

    public int? MaTaiKhoan { get; set; }

    public int? SoTienPhat { get; set; }

    public DateTime? ThoiGian { get; set; }

    public string? MoTa { get; set; }

    public virtual Phieutocao? MaPhieuToCaoNavigation { get; set; }

    public virtual Taikhoan? MaTaiKhoanNavigation { get; set; }
}
