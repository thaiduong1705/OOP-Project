using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Phieutocao
{
    public int MaPhieuToCao { get; set; }

    public int? MaBaiDang { get; set; }

    public string? LyDo { get; set; }

    public DateTime? ThoiGian { get; set; }

    public string? MoTa { get; set; }

    public virtual Baidang? MaBaiDangNavigation { get; set; }

    public virtual ICollection<Phieuphat> Phieuphats { get; } = new List<Phieuphat>();
}
