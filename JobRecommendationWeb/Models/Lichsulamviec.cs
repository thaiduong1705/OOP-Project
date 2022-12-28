using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Lichsulamviec
{
    public int MaLslv { get; set; }

    public int? MaTaiKhoan { get; set; }

    public string? CaLamViec { get; set; }

    public DateTime? NgayLamViec { get; set; }

    public virtual Taikhoan? MaTaiKhoanNavigation { get; set; }

    public virtual ICollection<Baidang> MaBaiDangs { get; set; } = new List<Baidang>();
}
