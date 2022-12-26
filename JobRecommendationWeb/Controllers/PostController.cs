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
                value.MaCongTy = Convert.ToInt32(form["maCongTy"]);
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

        public async Task<IActionResult> Detail(int? id)
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
            var company = await _context.Hosocongties.Where(x => x.MaCongTy == post.MaCongTy).FirstOrDefaultAsync();
            if (company == null)
            {
                return NotFound();
            }

            ViewBag.post = post;
            ViewBag.company = company;
            return View();
        }

        public async Task<IActionResult> CandidateApply(Ungvien c, Baidang p)
        {
            Ungtuyen u = new Ungtuyen();

            u.MaBaiDang = p.MaBaiDang;
            u.MaBaiDangNavigation = p;
            u.MaUngVien = c.MaUngVien;
            u.MaUngVienNavigation = c;
            u.NgayUngTuyen = DateTime.Today;
            u.ChapThuan = true;

            _context.Ungtuyens.Add(u);
            p.Ungtuyens.Add(u);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
