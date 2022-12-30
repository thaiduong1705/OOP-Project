using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Chitietlamviec
{
    public int MaCtlv { get; set; }

    public int? MaLslv { get; set; }

    public int? MaBaiDang { get; set; }

    public string? HoatDong { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual Lichsulamviec? MaLslvNavigation { get; set; }
}
