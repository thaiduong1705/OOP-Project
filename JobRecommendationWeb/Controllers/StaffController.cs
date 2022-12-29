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
            ViewBag.TaikhoanExisted = false;
            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaikhoanNhanvienViewModel value)
        {
            var ktTaikhoan = _context.Taikhoans.Where(x => x.TenDangNhap == value.Taikhoan.TenDangNhap).ToList();
            if (ktTaikhoan.Count > 0)
            {
                ViewBag.TaikhoanExisted = true;
                ViewBag.Chucvu = _context.Chucvus.ToList();
                return View(value);
            }

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
            return RedirectToAction("Index");
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
                    || x.Tuoi == int.Parse(searchInput)
                    || x.Email.Contains(searchInput)
                    || x.Sdt.Contains(searchInput) ).ToList();
                    break;
                case "TenNhanVien":
                    listNhanvien = _context.Nhanviens.Where(x => x.TenNhanVien.Contains(searchInput)).ToList();
                    break;
                case "Tuoi":
                    listNhanvien = _context.Nhanviens.Where(x => x.Tuoi == int.Parse(searchInput)).ToList();
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

            //else
            //{
            //    var listNhanvien = _context.Nhanviens.Where(x => x.TenNhanVien.Contains(form["Search"])).ToList();
            //    if (listNhanvien.Count == 0)
            //    {
            //        return View(new List<Nhanvien>());
            //    }
            //    return View(listNhanvien);
            //}
        }
    }
}
