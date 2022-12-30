using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Lichsulamviec
{
    public int MaLslv { get; set; }

    public int? MaTaiKhoan { get; set; }

    public DateTime? NgayLamViec { get; set; }

    public virtual ICollection<Chitietlamviec> Chitietlamviecs { get; } = new List<Chitietlamviec>();

    public virtual Taikhoan? MaTaiKhoanNavigation { get; set; }
}
