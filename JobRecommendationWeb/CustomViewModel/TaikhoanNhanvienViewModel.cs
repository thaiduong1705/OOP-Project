using System.ComponentModel.DataAnnotations;

namespace JobRecommendationWeb.CustomViewModel
{
    public class TaikhoanNhanvienViewModel
    {
        public Taikhoan Taikhoan { get; set; }
        public TaikhoanNhanvienViewModel()
        {
            Taikhoan = new Taikhoan();
        }
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        public string RePassword { get; set; }
    }
}
