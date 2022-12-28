using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Kinang
{
    public int MaKiNang { get; set; }

    public string? TenKiNang { get; set; }

    public virtual ICollection<Baidang> MaBaiDangs { get; set; } = new List<Baidang>();

    public virtual ICollection<Ungvien> MaUngViens { get; set; } = new List<Ungvien>();
}
