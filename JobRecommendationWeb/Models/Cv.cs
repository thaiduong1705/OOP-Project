using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Cv
{
    public int MaCv { get; set; }

    public int? MaUngVien { get; set; }

    public string? LinkCv { get; set; }

    public virtual Ungvien? MaUngVienNavigation { get; set; }
}
