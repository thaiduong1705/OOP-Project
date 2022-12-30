using JobRecommendationWeb.AddingClasses;
using JobRecommendationWeb.CustomViewModel;
using JobRecommendationWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

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
            var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.IsDeleted == false).ToList();
            return View(listUngVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection? form)
        {

            string searchInput = form["input"];
            var listUngVien = new List<Ungvien>();

            if (string.IsNullOrEmpty(searchInput))
            {
                listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).ToList();
            }
            else
            {
                listUngVien = new List<Ungvien>();
                switch (form["UngVien"])
                {
                    case "":
                        {
                            listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.Ten.Contains(searchInput)
                            || x.Tuoi.ToString() == searchInput
                            || x.DiaChi.Contains(searchInput)
                            || x.Email.Contains(searchInput)
                            || x.Sdt.Contains(searchInput)).ToList();
                            break;
                        }

                    case "TenUngVien":
                        {
                            listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.Ten.Contains(searchInput)).ToList();

                            break;
                        }
                    case "Tuoi":
                        {
                            listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.Tuoi.ToString() == searchInput).ToList();

                            break;
                        }
                    case "DiaChi":
                        {
                            listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.DiaChi.Contains(searchInput)).ToList();

                            break;
                        }
                    case "Email":
                        {
                            listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.Email.Contains(searchInput)).ToList();

                            break;
                        }
                    case "Sdt":
                        {
                            listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.Sdt.Contains(searchInput)).ToList();

                            break;
                        }
                    case "ThamNien":
                        {
                            int thamnien = 0;
                            listUngVien = new List<Ungvien>();
                            if (int.TryParse(searchInput, out thamnien))
                            {
                                listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.ThamNien >= thamnien).ToList();

                            }
                            break;
                        }
                }
            }
            listUngVien = listUngVien.Where(x => x.IsDeleted == false).ToList();
            return View(listUngVien);
        }

        public IActionResult Create()
        {
            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var listkinang = _context.Kinangs.ToList();
            return View(listkinang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form, IFormFile CV)
        {
            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                Ungvien ungvien = new Ungvien();
                ungvien.Ten = form["Ten"];
                ungvien.Email = form["Email"];
                ungvien.DiaChi = form["DiaChi"];
                ungvien.ThamNien = Convert.ToInt32(form["ThamNien"]);
                ungvien.Sdt = form["Sdt"];
                ungvien.Tuoi = Convert.ToInt32(form["Tuoi"]);
                ungvien.GioiTinh = Convert.ToInt32(form["GioiTinh"]);

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
                TempData["sucsess"] = "Tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            else { }
            TempData["success"] = "Tạo thành công";
            return View(form);
        }

        public IActionResult Edit(int? id)
        {

            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Ungvien ungvien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.MaUngVien == id).FirstOrDefault();
            List<Kinang> kinangs = _context.Kinangs.ToList();

            ViewBag.EditingCandidate = ungvien;
            ViewBag.kinangList = kinangs;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormCollection form)
        {

            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int id = Convert.ToInt32(form["MaUngVien"]);
            Ungvien ungvien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.MaUngVien == id && x.IsDeleted == false).FirstOrDefault();

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
            ungvien.GioiTinh = Convert.ToInt32(form["GioiTinh"]);

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

            var cv = _context.Cvs.FirstOrDefault(x => x.MaUngVien == ungvien.MaUngVien);
            if (form.Files.Count() != 0)
            {
                using (var stream = new MemoryStream())
                {
                    await form.Files[0].CopyToAsync(stream);
                    cv.AnhCv = stream.ToArray();
                }
            }


            _context.Ungviens.Update(ungvien);
            _context.SaveChanges();
            TempData["success"] = "Chỉnh sửa thành công!";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {

            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (id == null || id == 0)
            {
                return NotFound();
            }

            var ungvien = _context.Ungviens.FirstOrDefault(x => x.MaUngVien == id);

            if (ungvien == null) return NotFound();

            ungvien.IsDeleted = true;

            ///////Xoa ki nang cua ungvien
            //foreach (var makn in ungvien.MaKiNangs)
            //{
            //    var kinang = _context.Kinangs.FirstOrDefault(x => x.MaKiNang == makn.MaKiNang);
            //    var listknuv = new List<Ungvien>(kinang.MaUngViens);
            //    listknuv.Remove(ungvien);
            //    kinang.MaUngViens = listknuv;
            //}

            /////Xoa ung tuyen cua ung vien
            //var dsUngtuyen = _context.Ungtuyens.Where(x => x.MaUngVien == ungvien.MaUngVien).ToList();
            //foreach (var ungtuyen in dsUngtuyen)
            //{
            //    var baidang = _context.Baidangs.FirstOrDefault(x => x.MaBaiDang == ungtuyen.MaBaiDang);
            //    if (baidang != null)
            //    {
            //        var listUngtuyen = new List<Ungtuyen>(baidang.Ungtuyens);
            //        listUngtuyen.Remove(ungtuyen);
            //        baidang.Ungtuyens = listUngtuyen;
            //    }
            //    _context.Ungtuyens.Remove(ungtuyen);
            //}

            ///////Xoa CV
            //foreach (var macv in ungvien.Cvs)
            //{
            //    var cv = _context.Cvs.FirstOrDefault(x => x.MaCv == macv.MaCv);
            //    if (cv != null) _context.Cvs.Remove(cv);
            //}

            //_context.Ungviens.Remove(ungvien);
            _context.SaveChanges();

            TempData["success"] = "Xoá thành công!";
            return RedirectToAction("Index");
        }

        public IActionResult CvImageDetail(int? id)
        {

            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

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
