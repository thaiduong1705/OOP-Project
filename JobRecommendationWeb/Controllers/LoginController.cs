using JobRecommendationWeb.AddingClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobRecommendationWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly JobRecommendationContext _context;

        public LoginController(JobRecommendationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(IFormCollection form)
        {
            string username = form["Username"];
            string password = form["Password"];
            List<Chucvu> chucvus = _context.Chucvus.ToList();
            Taikhoan taikhoan = _context.Taikhoans.Where(x => x.TenDangNhap == username && x.MatKhau == password).FirstOrDefault();
            Nhanvien nhanvien = _context.Nhanviens.Where(x => x.MaNhanVien == taikhoan.MaNhanVien).FirstOrDefault();

            if (taikhoan == null || nhanvien == null)
            {
                return NotFound();
            }

            // Bind tai khoan va nhan vien
            UsingAccount.Instance.Taikhoan = taikhoan;
            UsingAccount.Instance.Nhanvien = nhanvien;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            UsingAccount.Instance.Taikhoan = null;
            UsingAccount.Instance.Nhanvien = null;
            return RedirectToAction("Index");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
