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
            var listCongTy = _context.Hosocongties.ToList();
            return View(listCongTy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IFormCollection form)
        {
            string searchInput = form["input"];
            if (string.IsNullOrEmpty(searchInput))
			{
                var listCongTy = _context.Hosocongties.ToList();
                return View(listCongTy);
            }
            else
            {
                switch (form["filter"])
                {
                    case "":
                        {
                            var listCongTy = new List<Hosocongty>();
                            listCongTy = _context.Hosocongties.Where(x => x.TenCongTy.Contains(searchInput)
                            || x.DiaChi.Contains(searchInput)
                            || x.Website.Contains(searchInput)
                            || x.QuocTich.Contains(searchInput)
                            || x.CheDoDaiNgo.Contains(searchInput)).ToList();
                            return View(listCongTy);
                        }
                    case "TenCongTy":
                        {
                            var listCongTy = _context.Hosocongties.Where(x => x.TenCongTy.Contains(searchInput)).ToList();
                            if (listCongTy.Count == 0)
                            {
                                return View(new List<Hosocongty>());
                            }
                            return View(listCongTy);
                        }
                    case "QuocTich":
                        {
                            var listCongTy = _context.Hosocongties.Where(x => x.QuocTich.Contains(searchInput)).ToList();
                            if (listCongTy.Count == 0)
                            {
                                return View(new List<Hosocongty>());
                            }
                            return View(listCongTy);
                        }
                    default:
                        {
                            var listUngVien = new List<Hosocongty>();
                            return View(listUngVien);
                        }
                }
            }
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

                using (var stream = new MemoryStream())
                {
                    await form.Files[0].CopyToAsync(stream);
                    value.AnhCongTy = stream.ToArray();
                }

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

            var congty = _context.Hosocongties.Include(x => x.Baidangs).FirstOrDefault(x => x.MaCongTy == id);

            if (congty == null) return NotFound();

            foreach (var baidang in congty.Baidangs)
            {
                ///Xoa phieu to cao lien quan toi bai dang
                foreach (var pToCao in baidang.Phieutocaos)
                {
                    var pPhat = _context.Phieuphats.FirstOrDefault(x => x.MaPhieuToCao == pToCao.MaPhieuToCao);
                    if (pPhat != null) _context.Phieuphats.Remove(pPhat);
                    _context.Phieutocaos.Remove(pToCao);
                }

                ///Xoa ki nang cua bai dang
                foreach (var makn in baidang.MaKiNangs)
                {
                    var kinang = _context.Kinangs.FirstOrDefault(x => x.MaKiNang == makn.MaKiNang);
                    var listknbd = new List<Baidang>(kinang.MaBaiDangs);
                    listknbd.Remove(baidang);
                    kinang.MaBaiDangs = listknbd;

                }

                ///Xoa ung tuyen cua ung vien
                var dsUngtuyen = _context.Ungtuyens.Where(x => x.MaBaiDang == baidang.MaBaiDang).ToList();
                foreach (var ungtuyen in dsUngtuyen)
                {
                    var ungvien = _context.Ungviens.FirstOrDefault(x => x.MaUngVien == ungtuyen.MaUngVien);
                    if (ungvien != null)
                    {
                        var listUngtuyen = new List<Ungtuyen>(ungvien.Ungtuyens);
                        listUngtuyen.Remove(ungtuyen);
                        ungvien.Ungtuyens = listUngtuyen;
                    }
                    _context.Ungtuyens.Remove(ungtuyen);
                }

                ///Xoa lich su lam viec cua bai dang
                var listLsLv = _context.Lichsulamviecs.Where(x => x.MaTaiKhoan == baidang.MaTaiKhoan).ToList();
                foreach (var lslv in listLsLv)
                {
                    var listBaiDang = new List<Baidang>(lslv.MaBaiDangs);
                    listBaiDang.Remove(baidang);
                    lslv.MaBaiDangs = listBaiDang;
                }

                _context.Baidangs.Remove(baidang);
            }

            _context.Hosocongties.Remove(congty);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
