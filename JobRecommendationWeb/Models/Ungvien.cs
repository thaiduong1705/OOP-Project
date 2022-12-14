using System;
using System.Collections.Generic;

namespace JobRecommendationWeb.Models;

public partial class Ungvien
{
    public int MaUngVien { get; set; }

    public string? Ten { get; set; }

    public int? Tuoi { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public int? ThamNien { get; set; }

    public int? GioiTinh { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Cv> Cvs { get; } = new List<Cv>();

    public virtual ICollection<Ungtuyen> Ungtuyens { get; set; } = new List<Ungtuyen>();

    public virtual ICollection<Kinang> MaKiNangs { get; set; } = new List<Kinang>();
}
