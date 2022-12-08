using Microsoft.AspNetCore.Mvc;

namespace JobRecommendationWeb.Controllers
{
    public class SkillController : Controller
    {
        private readonly JobRecommendationContext _context;

        public SkillController(JobRecommendationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Kinang> kinangs = _context.Kinangs.ToList();
            return View(kinangs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Kinang obj)
        {
            if (string.IsNullOrEmpty(obj.TenKiNang))
            {
                ModelState.AddModelError("tenkinang", "test");
            }    
            if (ModelState.IsValid)
            {
                _context.Kinangs.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        
    }
}
