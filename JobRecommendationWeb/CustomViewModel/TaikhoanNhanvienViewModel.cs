namespace JobRecommendationWeb.CustomViewModel
{
    public class TaikhoanNhanvienViewModel
    {
        public Taikhoan Taikhoan { get; set; }
        public TaikhoanNhanvienViewModel()
        {
            Taikhoan = new Taikhoan();
        }
        public string RePassword { get; set; }
    }
}
