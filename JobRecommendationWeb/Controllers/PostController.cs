using Microsoft.AspNetCore.Http.Metadata;
using JobRecommendationWeb.CustomViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using JobRecommendationWeb.Models;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace JobRecommendationWeb.Controllers
{
    public class PostController : Controller
    {
        private readonly JobRecommendationContext _context;
        public PostController(JobRecommendationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var listBaiDang = _context.Baidangs.ToList();
            return View(listBaiDang);
        }

        public IActionResult Create()
        {
            List<Hosocongty> companylist = _context.Hosocongties.ToList();
            List<Kinang> kinangList = _context.Kinangs.ToList();
            ViewBag.kinangList = kinangList;
            ViewBag.companylist = companylist;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                Baidang value = new Baidang();
                value.TenCongViec = form["TenCongViec"];
                value.WebsiteBaiGoc = form["WebsiteBaiGoc"];
                value.MaCongTy = Convert.ToInt32(form["MaCongTy"]);
                value.MoTa = form["MoTa"];
                value.ThamNien = Convert.ToInt32(form["ThamNien"]);
                value.LuongMin = Convert.ToInt32(form["LuongMin"]);
                value.LuongMax = Convert.ToInt32(form["LuongMax"]);
                value.GhiChu = form["GhiChu"];

                _context.Baidangs.Add(value);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(form);
        }


        public async Task<IActionResult> AppliedCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Baidangs.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            List<Ungvien> candidates = new List<Ungvien>();
            List<Ungtuyen> u = _context.Ungtuyens.ToList();
            foreach (var item in u)
            {
                if (item.MaBaiDang == post.MaBaiDang)
                {
                    Ungvien? c = await _context.Ungviens.FindAsync(id);
                    if (c != null)
                        candidates.Add(c);
                }
            }

            ViewBag.candidates = candidates;
            return View();
        }

        public async Task<IActionResult> Apply(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Baidangs.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var candidates = _context.Ungviens.ToList();

            ViewBag.post = post;
            ViewBag.candidates = candidates;

            return View();
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _context.Baidangs.Include(x => x.MaKiNangs).Where(x => x.MaBaiDang == id).FirstOrDefault();
            if (post == null)
            {
                return NotFound();
            }
            var company = _context.Hosocongties.Where(x => x.MaCongTy == post.MaCongTy).FirstOrDefault();
            if (company == null)
            {
                return NotFound();
            }

            ViewBag.post = post;
            ViewBag.company = company;
            return View();
        }

        public async Task<IActionResult> CandidateApply(int? candidate, int? post)
        {
            Ungvien ungvien = await _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.MaUngVien == candidate).FirstOrDefaultAsync();
            Baidang baidang = await _context.Baidangs.Where(x => x.MaBaiDang == post).FirstOrDefaultAsync();

            Ungtuyen ungtuyen = new Ungtuyen();
            ungtuyen.MaUngVien = ungvien.MaUngVien;
            ungtuyen.MaBaiDang = baidang.MaBaiDang;
            ungtuyen.MaUngVienNavigation = ungvien;
            ungtuyen.MaBaiDangNavigation = baidang;
            ungtuyen.ChapThuan = true;
            ungtuyen.NgayUngTuyen = DateTime.Now;

            _context.Ungtuyens.Add(ungtuyen);
            await _context.SaveChangesAsync();
            return RedirectToAction("Detail", new {id = post});
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Baidang baidang = _context.Baidangs.Include(x => x.MaKiNangs).Where(x => x.MaBaiDang == id).FirstOrDefault();

            List<Hosocongty> companyList = _context.Hosocongties.ToList();
            List<Kinang> kinangList = _context.Kinangs.ToList();
            ViewBag.CompanyList = companyList;
            ViewBag.KinangList = kinangList;
            ViewBag.EditingPost = baidang;
            return View(baidang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IFormCollection form)
        {
            int id = Convert.ToInt32(form["MaBaiDang"]);
            Baidang baidang = _context.Baidangs.Include(x => x.MaKiNangs).Where(x => x.MaBaiDang == id).FirstOrDefault();

            foreach (Kinang kinang in baidang.MaKiNangs.ToList())
            {
                baidang.MaKiNangs.Remove(kinang);
            }

            baidang.TenCongViec = form["TenCongViec"];
            baidang.WebsiteBaiGoc = form["WebsiteBaiGoc"];
            baidang.MaCongTy = Convert.ToInt32(form["MaCongTy"]);
            baidang.MoTa = form["MoTa"];
            baidang.ThamNien = Convert.ToInt32(form["ThamNien"]);
            baidang.LuongMin = Convert.ToInt32(form["LuongMin"]);
            baidang.LuongMax = Convert.ToInt32(form["LuongMax"]);
            baidang.GhiChu = form["GhiChu"];
            
            List<Kinang> temp = new List<Kinang>();
            foreach (var item in form["KiNang"])
            {
                Kinang kinang = _context.Kinangs.Where(x => x.MaKiNang == int.Parse(item)).FirstOrDefault();
                if (kinang != null)
                {
                    temp.Add(kinang);
                }    
            }

            if (temp.Count > 0)
            {
                baidang.MaKiNangs = temp;
            }

            _context.Baidangs.Update(baidang);
            _context.SaveChanges();

            return RedirectToAction("Detail", new {id = id});
        }
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Baidang baidang = _context.Baidangs.Include(x => x.MaKiNangs).FirstOrDefault(x => x.MaBaiDang == id);

            foreach (var kinang in baidang.MaKiNangs)
            {
                baidang.MaKiNangs.Remove(kinang);
            }
            _context.Baidangs.Remove(baidang);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
