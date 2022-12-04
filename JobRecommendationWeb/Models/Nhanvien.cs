using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Nhanvien
{
    public int MaNhanVien { get; set; }

    public string? TenNhanVien { get; set; }

    public int? Tuoi { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Taikhoan> Taikhoans { get; } = new List<Taikhoan>();
}
