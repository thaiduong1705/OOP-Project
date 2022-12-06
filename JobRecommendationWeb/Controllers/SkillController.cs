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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Kinang obj)
        {
            _context.Kinangs.Add(obj);
            _context.SaveChanges();
            return RedirectToAction("Index", "Skill");
        }

        
    }
}
