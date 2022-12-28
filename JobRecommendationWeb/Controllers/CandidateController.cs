using JobRecommendationWeb.CustomViewModel;
using JobRecommendationWeb.Models;
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
        public IActionResult Index(String? input)
        {
            if(input == null)
            {
                var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).ToList();
                return View(listUngVien);
            }    
            else
            {
                var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.Ten == input).ToList();
                if (listUngVien.Count == 0)
                {
                    return View(_context.Ungviens.Include(x => x.MaKiNangs).ToList());
                }
                return View(listUngVien);
            }    
        }

        public IActionResult Create()
        {
            var listkinang = _context.Kinangs.ToList();
            return View(listkinang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form, IFormFile CV)
        {
            if (ModelState.IsValid)
            {
                Ungvien ungvien = new Ungvien();
                ungvien.Ten = form["Ten"];
                ungvien.Email = form["Email"];
                ungvien.DiaChi = form["DiaChi"];
                ungvien.ThamNien = Convert.ToInt32(form["ThamNien"]);
                ungvien.Sdt = form["Sdt"];
                ungvien.Tuoi = Convert.ToInt32(form["Tuoi"]);

                List<Kinang> listkinang = new List<Kinang>();
                foreach(var k in form["Kinang"])
                {
                    var kinang = _context.Kinangs.FirstOrDefault(x => x.MaKiNang == int.Parse(k));
                    if (kinang != null)
                    {
                        listkinang.Add(kinang);
                    }
                }
                ungvien.MaKiNangs = listkinang;
                _context.Ungviens.Add(ungvien);
                await _context.SaveChangesAsync();

                Cv cv = new Cv();
                using (var stream = new MemoryStream())
                {
                    await form.Files[0].CopyToAsync(stream);
                    cv.AnhCv = stream.ToArray();
                }

                cv.MaUngVien = ungvien.MaUngVien;
                cv.MaUngVienNavigation = ungvien;

                _context.Cvs.Add(cv);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                
            }
            return View(form);
        }

        public IActionResult Edit(int? id)
        {
            Ungvien ungvien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.MaUngVien == id).FirstOrDefault();
            List<Kinang> kinangs = _context.Kinangs.ToList();

            ViewBag.EditingCandidate = ungvien;
            ViewBag.kinangList = kinangs;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IFormCollection form)
        {
            int id = Convert.ToInt32(form["MaUngVien"]);
            Ungvien ungvien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.MaUngVien == id).FirstOrDefault();

            if (ungvien == null)
            {
                return NotFound();
            }

            foreach (Kinang kinang in ungvien.MaKiNangs.ToList())
            {
                ungvien.MaKiNangs.Remove(kinang);
            }

            ungvien.Ten = form["Ten"];
            ungvien.Email = form["Email"];
            ungvien.DiaChi = form["DiaChi"];
            ungvien.ThamNien = Convert.ToInt32(form["ThamNien"]);
            ungvien.Sdt = form["Sdt"];
            ungvien.Tuoi = Convert.ToInt32(form["Tuoi"]);

            List<Kinang> listkinang = new List<Kinang>();
            foreach (var k in form["Kinang"])
            {
                var kinang = _context.Kinangs.FirstOrDefault(x => x.MaKiNang == int.Parse(k));
                if (kinang != null)
                {
                    listkinang.Add(kinang);
                }
            }
            ungvien.MaKiNangs = listkinang;

            _context.Ungviens.Update(ungvien);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            
            return RedirectToAction("Index");
        }

        public IActionResult CvImageDetail(int? id)
        {
            Cv cv = _context.Cvs.Where(x => x.MaUngVien == id).FirstOrDefault();
            if (cv == null)
            {
                return NotFound();
            }
            ViewBag.Cv = cv;
            return View();
        }
    }
}
