using Microsoft.AspNetCore.Mvc;

namespace JobRecommendationWeb.Controllers
{
    // quan ly nhan vien
    public class StaffController : Controller
    {
        private readonly JobRecommendationContext _context;
        public StaffController(JobRecommendationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Nhanvien> nhanvien = _context.Nhanviens.ToList();

            ViewBag.nhanvien = nhanvien;
            return View();
        }
    }
}
