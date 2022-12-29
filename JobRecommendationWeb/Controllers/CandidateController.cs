﻿using JobRecommendationWeb.CustomViewModel;
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
            var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).ToList();
            return View(listUngVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection form)
        {
            string searchInput = form["input"];

            if (string.IsNullOrEmpty(searchInput))
            {
                var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).ToList();
                return View(listUngVien);
            }
            else
            {
                switch (form["UngVien"])
                {
                    case "":
                        {
                            var listUngVien = new List<Ungvien>();

                            listUngVien = _context.Ungviens.Where(x => x.Ten.Contains(searchInput)
                            || x.Tuoi == int.Parse(searchInput)
                            || x.DiaChi.Contains(searchInput)
                            || x.Email.Contains(searchInput)
                            || x.Sdt.Contains(searchInput) ).ToList();

                            return View(listUngVien);
                        }

                    case "TenUngVien":
                        {
                            var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.Ten.Contains(searchInput)).ToList();
                            if (listUngVien.Count == 0)
                            {
                                return View(new List<Ungvien>());
                            }
                            return View(listUngVien);
                        }
                    case "Tuoi":
                        {
                            var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.Tuoi >= int.Parse(searchInput)).ToList();
                            if (listUngVien.Count == 0)
                            {
                                return View(new List<Ungvien>());
                            }
                            return View(listUngVien);
                        }
                    case "DiaChi":
                        {
                            var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.DiaChi.Contains(searchInput)).ToList();
                            if (listUngVien.Count == 0)
                            {
                                return View(new List<Ungvien>());
                            }
                            return View(listUngVien);
                        }
                    case "Email":
                        {
                            var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.Email.Contains(searchInput)).ToList();
                            if (listUngVien.Count == 0)
                            {
                                return View(new List<Ungvien>());
                            }
                            return View(listUngVien);
                        }
                    case "Sdt":
                        {
                            var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.Sdt.Contains(searchInput)).ToList();
                            if (listUngVien.Count == 0)
                            {
                                return View(new List<Ungvien>());
                            }
                            return View(listUngVien);
                        }
                    case "ThamNien":
                        {
                            var listUngVien = _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.ThamNien >= int.Parse(searchInput)).ToList();
                            if (listUngVien.Count == 0)
                            {
                                return View(new List<Ungvien>());
                            }
                            return View(listUngVien);
                        }
                    default:
                        {
                            var listUngVien = new List<Ungvien>();
                            return View(listUngVien);
                        }
                }
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
        public async Task<IActionResult> Edit(IFormCollection form)
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
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var ungvien = _context.Ungviens.Include(x => x.MaKiNangs).Include(x => x.Cvs).FirstOrDefault(x => x.MaUngVien == id);

            if (ungvien == null) return NotFound();

            /////Xoa ki nang cua ungvien
            foreach (var makn in ungvien.MaKiNangs)
            {
                var kinang = _context.Kinangs.FirstOrDefault(x => x.MaKiNang == makn.MaKiNang);
                var listknuv = new List<Ungvien>(kinang.MaUngViens);
                listknuv.Remove(ungvien);
                kinang.MaUngViens = listknuv;
            }

            ///Xoa ung tuyen cua ung vien
            var dsUngtuyen = _context.Ungtuyens.Where(x => x.MaUngVien == ungvien.MaUngVien).ToList();
            foreach (var ungtuyen in dsUngtuyen)
            {
                var baidang = _context.Baidangs.FirstOrDefault(x => x.MaBaiDang == ungtuyen.MaBaiDang);
                if (baidang != null)
                {
                    var listUngtuyen = new List<Ungtuyen>(baidang.Ungtuyens);
                    listUngtuyen.Remove(ungtuyen);
                    baidang.Ungtuyens = listUngtuyen;
                }
                _context.Ungtuyens.Remove(ungtuyen);
            }

            /////Xoa CV
            foreach (var macv in ungvien.Cvs)
            {
                var cv = _context.Cvs.FirstOrDefault(x => x.MaCv == macv.MaCv);
                if (cv != null) _context.Cvs.Remove(cv);
            }

            _context.Ungviens.Remove(ungvien);
            _context.SaveChanges();
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
