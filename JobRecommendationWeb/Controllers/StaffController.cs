using JobRecommendationWeb.AddingClasses;
using JobRecommendationWeb.CustomViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JobRecommendationWeb.Controllers
{
    // quan ly nhan vien
    public class StaffController : Controller
    {
        private readonly JobRecommendationContext _context;
        public StaffController(JobRecommendationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Nhanvien> nhanvien = _context.Nhanviens.ToList();
            ViewBag.nhanvien = nhanvien;
            return View();
        }
        public IActionResult Create()
        {
            TaikhoanNhanvienViewModel value = new TaikhoanNhanvienViewModel();
            ViewBag.Chucvu = _context.Chucvus.ToList();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaikhoanNhanvienViewModel value)
        {
            if (value.Taikhoan.MatKhau != value.RePassword)
            {
                ModelState.AddModelError("RePassword", "Mật khẩu nhập lại không đúng");
            }

            var ktTaikhoan = _context.Taikhoans.Where(x => x.TenDangNhap == value.Taikhoan.TenDangNhap).ToList();
            if (ktTaikhoan.Count > 0)
            {
                ModelState.AddModelError("Taikhoan.TenDangNhap", "Tên đăng nhập đã có người sử dụng");
            }

            if (ModelState.IsValid)
            {
                var nhanvien = new Nhanvien();
                nhanvien.TenNhanVien = value.Nhanvien.TenNhanVien;
                nhanvien.Email = value.Nhanvien.Email;
                nhanvien.Sdt = value.Nhanvien.Sdt;
                nhanvien.Tuoi = value.Nhanvien.Tuoi;

                _context.Nhanviens.Add(nhanvien);
                _context.SaveChanges();

                var taikhoan = new Taikhoan();
                taikhoan.MaNhanVien = nhanvien.MaNhanVien;
                taikhoan.MaChucVu = value.Taikhoan.MaChucVu;
                taikhoan.TenDangNhap = value.Taikhoan.TenDangNhap;

                var pass = Encryptor.CreateMD5(Encryptor.Base64Encode(value.Taikhoan.MatKhau));
                taikhoan.MatKhau = pass;

                _context.SaveChanges();
                TempData["success"] = "Tạo thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.Chucvu = _context.Chucvus.ToList();
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection form)
        {
            string searchInput = form["input"];
            var listNhanvien = new List<Nhanvien>();
            if (string.IsNullOrEmpty(searchInput))
            {
                listNhanvien = _context.Nhanviens.ToList();
                ViewBag.nhanvien = listNhanvien;
                return View();
            }
            switch (form["NhanVien"])
            {
                case "":
                    listNhanvien = _context.Nhanviens.Where(x => x.TenNhanVien.Contains(searchInput)
                    || x.Tuoi.ToString() == searchInput
                    || x.Email.Contains(searchInput)
                    || x.Sdt.Contains(searchInput) ).ToList();
                    break;
                case "TenNhanVien":
                    listNhanvien = _context.Nhanviens.Where(x => x.TenNhanVien.Contains(searchInput)).ToList();
                    break;
                case "Tuoi":
                    listNhanvien = _context.Nhanviens.Where(x => x.Tuoi.ToString() == searchInput).ToList();
                    break;
                case "Sdt":
                    listNhanvien = _context.Nhanviens.Where(x => x.Sdt.Contains(searchInput)).ToList();
                    break;
                case "Email":
                    listNhanvien = _context.Nhanviens.Where(x => x.Email.Contains(searchInput)).ToList();
                    break;

            }

            ViewBag.nhanvien = listNhanvien;
            return View();
        }

        public IActionResult Edit(int? id)
        {
            return View(id);
        }
    }
}
