namespace JobRecommendationWeb.CustomViewModel
{
    public class AddPostViewModel
    {
        public Baidang baidang { get; set; }
        public List<Kinang> listKiNang { get; set; } = new List<Kinang>();
        public List<Hosocongty> listCongTy { get; set; } = new List<Hosocongty>();    
        public string[] listKiNangIdBinding { get; set; }

    }
}
