using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Thongke
{
    public int MaThongKe { get; set; }

    public DateTime? ThoiGian { get; set; }

    public int? SoBaiDangDaThem { get; set; }

    public int? SoCvdaThem { get; set; }

    public int? SoNguoiDaUngTuyen { get; set; }
}
