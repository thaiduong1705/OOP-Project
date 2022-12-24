using Microsoft.AspNetCore.Mvc;

namespace JobRecommendationWeb.Controllers
{
    public class CompanyController : Controller
    {
        private readonly JobRecommendationContext _context;
        public CompanyController(JobRecommendationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var listCongTy = _context.Hosocongties.ToList();
            return View(listCongTy);
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
