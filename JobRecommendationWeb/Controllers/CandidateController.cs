using JobRecommendationWeb.CustomViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).ToList();

            return View(listUngVien);
        }

        public IActionResult Create()
        {
            return View();
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


                _context.Ungviens.Add(value);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(form);
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
