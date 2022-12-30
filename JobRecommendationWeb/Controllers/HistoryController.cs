using Microsoft.AspNetCore.Mvc;
using JobRecommendationWeb.AddingClasses;
using JobRecommendationWeb.Models;

namespace JobRecommendationWeb.Controllers
{
    public class HistoryController : Controller
    {
        private readonly JobRecommendationContext _context;
        public HistoryController(JobRecommendationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var taikhoan = _context.Taikhoans.ToList();
            var lichsulamviec = _context.Lichsulamviecs.ToList();
            var chitietlamviec = _context.Chitietlamviecs.ToList();

            ViewBag.taikhoan = taikhoan;
            ViewBag.lichsulamviec = lichsulamviec;
            ViewBag.chitietlamviec = chitietlamviec;

            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            if (form["lslv"] == "")
            {
                return View();
            }
            int id = int.Parse(form["lslv"]);
            var taikhoan = _context.Taikhoans.ToList();
            var lichsulamviec = _context.Lichsulamviecs.ToList();
            var chitietlamviec = _context.Chitietlamviecs.ToList();
            var ct = new List<Chitietlamviec>();

            ViewBag.taikhoan = taikhoan;
            ViewBag.lichsulamviec = lichsulamviec;
            ViewBag.chitietlamviec = chitietlamviec;

            foreach (Chitietlamviec k in _context.Chitietlamviecs)
            {
                if (k.MaLslv == id)
                    ct.Add(k);
            }

            return View(ct);
        }
    }
}
