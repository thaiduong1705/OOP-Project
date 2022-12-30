using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobRecommendationWeb.Models;

public partial class Taikhoan
{
    public int MaTaiKhoan { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
    [MinLength(6, ErrorMessage = "Tên đăng nhập ít nhất có 6 kí tự")]
    [MaxLength(20, ErrorMessage = "Tên đăng nhập không quá 20 kí tự")]
    [RegularExpression(@"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$", ErrorMessage = "Tên đăng nhập không hợp lệ")]
    public string? TenDangNhap { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [MinLength(6, ErrorMessage = "Mật khẩu phải ít nhất 6 kí tự")]
    public string? MatKhau { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn chức vụ")]
    public int? MaChucVu { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên nhân viên")]
    public string? TenNhanVien { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tuổi của nhân viên")]
    [Range(18, 65, ErrorMessage = "Tuổi phải từ 18 tới 65")]
    public int? Tuoi { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [MinLength(10, ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
    [MaxLength(10, ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
    [Phone(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại")]
    public string? Sdt { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
    public string? Email { get; set; }

    public int? GioiTinh { get; set; }

    public virtual ICollection<Baidang> Baidangs { get; set; } = new List<Baidang>();

    public virtual ICollection<Lichsulamviec> Lichsulamviecs { get; set; } = new List<Lichsulamviec>();

    public virtual Chucvu? MaChucVuNavigation { get; set; }

    public virtual ICollection<Phieuphat> Phieuphats { get; } = new List<Phieuphat>();
}
