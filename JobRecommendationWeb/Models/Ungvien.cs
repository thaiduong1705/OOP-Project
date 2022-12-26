using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobRecommendationWeb.Models;

public partial class Ungvien
{
    public int MaUngVien { get; set; }

    public string? Ten { get; set; }

    [DisplayName("Tuổi")]
    [Range(18, 65,
        ErrorMessage = "{0} phải nằm trong khoảng từ {1} tới {2}.")]
    public int? Tuoi { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public int? ThamNien { get; set; }

    public virtual ICollection<Cv> Cvs { get; } = new List<Cv>();

    public virtual ICollection<Ungtuyen> Ungtuyens { get; } = new List<Ungtuyen>();

    public virtual ICollection<Kinang> MaKiNangs { get; set; } = new List<Kinang>();
}
