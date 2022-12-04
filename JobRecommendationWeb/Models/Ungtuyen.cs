using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Ungtuyen
{
    public int MaUngVien { get; set; }

    public int MaBaiDang { get; set; }

    public DateTime? NgayUngTuyen { get; set; }

    public bool? ChapThuan { get; set; }

    public virtual Baidang MaBaiDangNavigation { get; set; } = null!;

    public virtual Ungvien MaUngVienNavigation { get; set; } = null!;
}
