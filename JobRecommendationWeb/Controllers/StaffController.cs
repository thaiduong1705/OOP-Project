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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection form)
        {
            if (form["Search"] == "")
            {
                var listNhanvien = new List<Nhanvien>();
                return View(listNhanvien);
            }
            else
            {
                var listNhanvien = _context.Nhanviens.Where(x => x.TenNhanVien.Contains(form["Search"])).ToList();
                if (listNhanvien.Count == 0)
                {
                    return View(new List<Nhanvien>());
                }
                return View(listNhanvien);
            }
        }
    }
}
