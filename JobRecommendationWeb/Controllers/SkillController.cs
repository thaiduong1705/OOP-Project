using JobRecommendationWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

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

        [HttpGet]
        public IActionResult Create()
        {
            Kinang kinang = new Kinang();
            return PartialView("_CreateSkillModal", kinang);
        }

        [HttpPost]
        public IActionResult Create(Kinang obj)
        {
            if (string.IsNullOrEmpty(obj.TenKiNang))
            {
                ModelState.AddModelError("tenkinang", "Tên kĩ năng không được trùng");
            }
            if (ModelState.IsValid)
            {
                _context.Kinangs.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("_EditSkillModal", obj);
        }

        
        public IActionResult Edit(int id)
        {
            var kinang = _context.Kinangs.Where(x => x.MaKiNang == id).FirstOrDefault();

            return PartialView("_EditSkillModal", kinang);
        }

        [HttpPost] 
        public IActionResult Edit(Kinang kinang)
        {
            if (string.IsNullOrEmpty(kinang.TenKiNang))
            {
                ModelState.AddModelError("tenkinang", "Tên kĩ năng không được trùng");
            }    
            if (ModelState.IsValid)
            {
                _context.Kinangs.Update(kinang);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("_EditSkillModal", kinang);
        }

    }
}
