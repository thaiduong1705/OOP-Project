using JobRecommendationWeb.CustomViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JobRecommendationWeb.Controllers
{
    public class CandidateController : Controller
    {
        private readonly JobRecommendationContext _context;
        private List<CandidateViewModel> _customCandidate;
        public List<CandidateViewModel> CustomCandidate
        {
            get { return _customCandidate; }
            set { _customCandidate = value; }
        }

        public CandidateController(JobRecommendationContext context)
        {
            CustomCandidate = new List<CandidateViewModel>();
            _context = context;
        }
        public IActionResult Index()
        {
            var listUngVien = _context.Ungviens.ToList();
            var kinangungvien = _context.Kinangs.ToList();
            foreach (var ungvien in listUngVien)
            {
                CandidateViewModel candidateView = new CandidateViewModel();
                candidateView.ungvien = ungvien;
                

            }
            
            return View(listUngVien);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
