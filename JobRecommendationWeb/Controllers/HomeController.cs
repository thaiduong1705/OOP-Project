using JobRecommendationWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JobRecommendationWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly JobRecommendationContext _context;

        public HomeController(ILogger<HomeController> logger, JobRecommendationContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            List<Baidang> postList = _context.Baidangs.ToList();
            return View(postList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Kinangs/Details/5
        public async Task<IActionResult> KinangDetails(int? id)
        {
            if (id == null || _context.Kinangs == null)
            {
                return NotFound();
            }

            var kinang = await _context.Kinangs
                .FirstOrDefaultAsync(m => m.MaKiNang == id);
            if (kinang == null)
            {
                return NotFound();
            }

            return View(kinang);
        }

<<<<<<< Updated upstream
        // GET: Kinangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kinangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKiNang,TenKiNang")] Kinang kinang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kinang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kinang);
        }

        // GET: Kinangs/Edit/5
        public async Task<IActionResult> KinangEdit(int? id)
        {
            if (id == null || _context.Kinangs == null)
            {
                return NotFound();
            }

            var kinang = await _context.Kinangs.FindAsync(id);
            if (kinang == null)
            {
                return NotFound();
            }
            return View(kinang);
        }

        // POST: Kinangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KinangEdit(int id, [Bind("MaKiNang,TenKiNang")] Kinang kinang)
        {
            if (id != kinang.MaKiNang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kinang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KinangExists(kinang.MaKiNang))
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
            return View(kinang);
        }

        // GET: Kinangs/Delete/5
        public async Task<IActionResult> KinangDelete(int? id)
        {
            if (id == null || _context.Kinangs == null)
            {
                return NotFound();
            }

            var kinang = await _context.Kinangs
                .FirstOrDefaultAsync(m => m.MaKiNang == id);
            if (kinang == null)
            {
                return NotFound();
            }

            return View(kinang);
        }

        // POST: Kinangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kinangs == null)
            {
                return Problem("Entity set 'JobRecommendationContext.Kinangs'  is null.");
            }
            var kinang = await _context.Kinangs.FindAsync(id);
            if (kinang != null)
            {
                _context.Kinangs.Remove(kinang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KinangExists(int id)
=======
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
>>>>>>> Stashed changes
        {
            return _context.Kinangs.Any(e => e.MaKiNang == id);
        }
    }
}