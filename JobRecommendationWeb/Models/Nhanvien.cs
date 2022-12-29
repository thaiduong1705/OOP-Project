using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobRecommendationWeb.Models;

public partial class Nhanvien
{
    public int MaNhanVien { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên nhân viên")]
    public string? TenNhanVien { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tuổi của nhân viên")]
    [Range(18, 65, ErrorMessage = "Tuổi phải từ 18 tới 65")]
    public int? Tuoi { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [MinLength(6, ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
    [Phone(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
    public string? Sdt { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập email")]
    //[RegularExpression(@"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$", ErrorMessage = "Vui lòng nhập đúng định dạng email")]
    [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
    public string? Email { get; set; }

    public virtual ICollection<Taikhoan> Taikhoans { get; } = new List<Taikhoan>();
}
