using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var company = await _context.Hosocongties.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
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
                Hosocongty value = new Hosocongty();
                value.TenCongTy = form["TenCongTy"];
                value.Website = form["Website"];
                value.DiaChi = form["DiaChi"];
                value.QuocTich = form["country"];
                value.CheDoDaiNgo = form["CheDoDaiNgo"];
                value.MoTaThem = form["MoTaThem"];

                _context.Hosocongties.Add(value);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(form);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var value = await _context.Hosocongties.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Hosocongty value)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(value);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (value == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(value);
        }

    }
}
