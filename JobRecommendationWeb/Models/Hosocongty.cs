using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Hosocongty
{
    public int MaCongTy { get; set; }

    public string? TenCongTy { get; set; }

    public string? Website { get; set; }

    public string? DiaChi { get; set; }

    public string? QuocTich { get; set; }

    public string? CheDoDaiNgo { get; set; }

    public string? MoTaThem { get; set; }

    public byte[]? AnhCongTy { get; set; }

    public virtual ICollection<Baidang> Baidangs { get; } = new List<Baidang>();
}
