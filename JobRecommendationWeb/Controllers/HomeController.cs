using JobRecommendationWeb.AddingClasses;
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
            ViewBag.Action = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            List<Baidang> postList = _context.Baidangs.ToList();
            List<Chucvu> chucvus = _context.Chucvus.ToList();
            List<Taikhoan> tks = _context.Taikhoans.ToList();

            // Goi len View Bag
            ViewBag.TaiKhoan = UsingAccount.Instance.Taikhoan;

            return View(postList);
        }

        public IActionResult IntroductionPartial()
        {
            return PartialView("_Introduction");
        }

        public IActionResult AboutParital()
        {
            return PartialView("_About");
        }
        public IActionResult ContactPartial()
        {
            return PartialView("_Contact");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SendMail()
        {
            ///Gui mail ve 
            return RedirectToAction("Index");
        }
    }
}