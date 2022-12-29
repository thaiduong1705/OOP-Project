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
            string password = Encryptor.CreateMD5(Encryptor.Base64Encode(form["Password"]));
            List<Chucvu> chucvus = _context.Chucvus.ToList();
            List<Nhanvien> nhanviens = _context.Nhanviens.ToList();
            Taikhoan taikhoan = _context.Taikhoans.Where(x => x.TenDangNhap == username && x.MatKhau == password).FirstOrDefault();

            if (taikhoan == null)
            {
                TempData["error"] = "Đăng nhập thất bại!";
                return RedirectToAction("Index");
            }

            // Bind tai khoan va nhan vien
            UsingAccount.Instance.Taikhoan = taikhoan;
            UsingAccount.Instance.Nhanvien = taikhoan.MaNhanVienNavigation;
            TempData["success"] = "Đăng nhập thành công";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            UsingAccount.Instance.Taikhoan = null;
            UsingAccount.Instance.Nhanvien = null;
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
