using JobRecommendationWeb.AddingClasses;
using JobRecommendationWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

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
            List<Nhanvien> nhanviens = _context.Nhanviens.ToList();
            List<Taikhoan> tks = _context.Taikhoans.ToList();

            // Goi len View Bag
            ViewBag.TaiKhoan = UsingAccount.Instance.Taikhoan;
            ViewBag.Nhanvien = UsingAccount.Instance.Nhanvien;

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

        [HttpPost]
        public IActionResult SendMail(IFormCollection? form)
        {
            if (form["username"] != "" && form["message"] != "")
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("testemail65465@gmail.com");
                    mail.To.Add("testemail65465@gmail.com");
                    mail.Subject = form["username"] + ": " + form["subject"];
                    mail.Body = form["message"];
                    mail.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("testemail65465@gmail.com", "luboauymuxbdfuvh");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}