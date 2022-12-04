using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Chucvu
{
    public int MaChucVu { get; set; }

    public string? TenChucVu { get; set; }

    public virtual ICollection<Taikhoan> Taikhoans { get; } = new List<Taikhoan>();
}
