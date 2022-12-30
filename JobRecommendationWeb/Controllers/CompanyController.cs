using JobRecommendationWeb.Models;
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
            var listCongTy = _context.Hosocongties.Where(x => x.IsDeleted == false).ToList();
            return View(listCongTy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IFormCollection form)
        {
            string searchInput = form["input"];
            var listCongTy = new List<Hosocongty>();
            if (string.IsNullOrEmpty(searchInput))
            {
                listCongTy = _context.Hosocongties.ToList();
            }
            else
            {
                switch (form["filter"])
                {
                    case "":
                        {
                            listCongTy = _context.Hosocongties.Where(x => x.TenCongTy.Contains(searchInput)
                            || x.DiaChi.Contains(searchInput)
                            || x.Website.Contains(searchInput)
                            || x.QuocTich.Contains(searchInput)
                            || x.CheDoDaiNgo.Contains(searchInput)).ToList();
                            break;
                        }
                    case "TenCongTy":
                        {
                            listCongTy = _context.Hosocongties.Where(x => x.TenCongTy.Contains(searchInput)).ToList();
                            break;
                        }
                    case "QuocTich":
                        {
                            listCongTy = _context.Hosocongties.Where(x => x.QuocTich.Contains(searchInput)).ToList();
                            break;
                        }
                }
            }
            listCongTy = listCongTy.Where(x => x.IsDeleted == false).ToList();
            return View(listCongTy);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var company = await _context.Hosocongties.FirstOrDefaultAsync(x => x.MaCongTy == id && x.IsDeleted == false);
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

                using (var stream = new MemoryStream())
                {
                    await form.Files[0].CopyToAsync(stream);
                    value.AnhCongTy = stream.ToArray();
                }

                _context.Hosocongties.Add(value);
                await _context.SaveChangesAsync();
                TempData["success"] = "Tạo thành công!";
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

            var value = await _context.Hosocongties.FirstOrDefaultAsync(x => x.MaCongTy == id && x.IsDeleted == false);
            if (value == null)
            {
                return NotFound();
            }
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormCollection form)
        {
            var value = await _context.Hosocongties.FindAsync(int.Parse(form["MaCongTy"]));
            if (ModelState.IsValid)
            {
                if (form.Files.Count() != 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await form.Files[0].CopyToAsync(stream);
                        value.AnhCongTy = stream.ToArray();
                    }
                }

                value.TenCongTy = form["TenCongTy"];
                value.Website = form["Website"];
                value.DiaChi = form["DiaChi"];
                value.QuocTich = form["QuocTich"];
                value.CheDoDaiNgo = form["CheDoDaiNgo"];
                value.MoTaThem = form["MoTaThem"];

                _context.Update(value);
                await _context.SaveChangesAsync();

                //try
                //{
                //    if (value.AnhCongTy == null)
                //    {
                //        byte[] anh = _context.Hosocongties.FirstOrDefault(x => x.MaCongTy == value.MaCongTy).AnhCongTy;
                //        if (anh != null) value.AnhCongTy = anh;
                //    }

                //    _context.Update(value);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (value == null)
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                TempData["success"] = "Chỉnh sửa thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(value);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var congty = _context.Hosocongties.FirstOrDefault(x => x.MaCongTy == id);

            if (congty == null) return NotFound();
            congty.IsDeleted = true;

            List<Baidang> listbaidang = _context.Baidangs.Where(x => x.MaCongTy == congty.MaCongTy).ToList();

            foreach (var baidang in listbaidang)
            {
                baidang.IsDeleted = true;
            }

            _context.SaveChanges();
            TempData["success"] = "Xoá thành công!";
            return RedirectToAction("Index");
        }
    }
}
