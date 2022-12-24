using Microsoft.AspNetCore.Mvc;

namespace JobRecommendationWeb.Controllers
{
    public class CandidateController : Controller
    {
        private JobRecommendationContext _context;
        public CandidateController(JobRecommendationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var listUngVien = _context.Ungviens.ToList();
            return View(listUngVien);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
