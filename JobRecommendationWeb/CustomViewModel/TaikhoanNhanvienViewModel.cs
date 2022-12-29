namespace JobRecommendationWeb.CustomViewModel
{
    public class TaikhoanNhanvienViewModel
    {
        public Nhanvien Nhanvien { get; set; }
        public Taikhoan Taikhoan { get; set; }
        public TaikhoanNhanvienViewModel()
        {
            Nhanvien = new Nhanvien();
            Taikhoan = new Taikhoan();
        }
        public string RePassword { get; set; }
    }
}
