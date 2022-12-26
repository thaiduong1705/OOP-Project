using JobRecommendationWeb.CustomViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobRecommendationWeb.Controllers
{
    public class CandidateController : Controller
    {
        private readonly JobRecommendationContext _context;

        public CandidateController(JobRecommendationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).ToList();

            return View(listUngVien);
        }

        public IActionResult Create()
        {
            var listkinang = _context.Kinangs.ToList();
            return View(listkinang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                Ungvien value = new Ungvien();
                value.Ten = form["Ten"];
                value.Email = form["Email"];
                value.DiaChi = form["DiaChi"];
                value.ThamNien = Convert.ToInt32(form["ThamNien"]);
                value.Sdt = form["Sdt"];
                value.Tuoi = Convert.ToInt32(form["Tuoi"]);

                List<Kinang> listkinang = new List<Kinang>();
                foreach(var k in form["Kinang"])
                {
                    var kinang = _context.Kinangs.FirstOrDefault(x => x.MaKiNang == int.Parse(k));
                    if (kinang != null)
                    {
                        listkinang.Add(kinang);
                    }
                }
                value.MaKiNangs = listkinang;

                _context.Ungviens.Add(value);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                
            }
            return View(form);
        }

        public IActionResult Add()
        {
            return View();

        }
    }
}
