using ClosedXML.Excel;
using JobRecommendationWeb.AddingClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            var listBaiDang = _context.Baidangs.Include(x => x.MaCongTyNavigation).Where(x => x.IsDeleted == false).ToList();
            return View(listBaiDang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection? form)
        { 
            string searchInput = form["input"];
            var listBaiDang = _context.Baidangs.Include(x => x.MaCongTyNavigation).Where(x => x.IsDeleted == false).ToList();

            if (string.IsNullOrEmpty(searchInput))
            {
                listBaiDang = listBaiDang.Where(x => x.IsDeleted == false).ToList();
            }
            else
            {
                switch (form["baidang"])
                {
                    case "":
                        {
                            listBaiDang = listBaiDang.Where(x => x.MaCongTyNavigation.TenCongTy.Contains(searchInput)
                                || x.TenCongViec.Contains(searchInput)
                                || (x.MoTa == null)? true : x.MoTa.Contains(searchInput)).ToList();
                            break;

                        }
                    case "TenCongTy":
                        {
                            listBaiDang = listBaiDang.Where(x => x.MaCongTyNavigation.TenCongTy.Contains(searchInput)).ToList();
                            break;
                        }
                    case "TenCongViec":
                        {
                            listBaiDang = listBaiDang.Where(x => x.TenCongViec.Contains(searchInput)).ToList();
                            break;
                        }

                    case "LuongMax":
                        {
                            int luongMax;
                            if (int.TryParse(form["input"], out luongMax))
                            {
                                listBaiDang = listBaiDang.Where(x => x.LuongMax >= luongMax).ToList();
                            }
                            break;

                        }

                    case "LuongMin":
                        {
                            int luongMin;
                            if (int.TryParse(form["input"], out luongMin))
                            {
                                listBaiDang = listBaiDang.Where(x => x.LuongMin >= luongMin).ToList();
                            }
                            break;
                        }

                    case "ThamNien":
                        {
                            int thamNien;
                            if (int.TryParse(form["input"], out thamNien))
                            {
                                listBaiDang = listBaiDang.Where(x => x.LuongMin <= thamNien).ToList();
                            }
                            break;
                        }
                }
            }
            return View(listBaiDang);
        }


        public IActionResult Create()
        {
            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Hosocongty> companylist = _context.Hosocongties.Where(x => x.IsDeleted == false).ToList();
            List<Kinang> kinangList = _context.Kinangs.ToList();
            ViewBag.kinangList = kinangList;
            ViewBag.companylist = companylist;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                Baidang value = new Baidang();
                value.TenCongViec = form["TenCongViec"];
                value.WebsiteBaiGoc = form["WebsiteBaiGoc"];
                value.MaCongTy = Convert.ToInt32(form["MaCongTy"]);
                value.ThamNien = Convert.ToInt32(form["ThamNien"]);
                value.LuongMin = Convert.ToInt32(form["LuongMin"]);
                value.LuongMax = Convert.ToInt32(form["LuongMax"]);
                value.GhiChu = form["GhiChu"];
                value.MoTa = form["MoTaThem"];

                _context.Baidangs.Add(value);
                await _context.SaveChangesAsync();

                List<Kinang> kinangs = new List<Kinang>();

                foreach (var item in form["KiNang"])
                {
                    Kinang kinang = _context.Kinangs.Where(x => x.MaKiNang == Convert.ToInt32(item)).FirstOrDefault();
                    kinangs.Add(kinang);
                }
                value.MaKiNangs = kinangs;

                var chitietlamviec = new Chitietlamviec();
                var date = DateTime.Now;
                var lslv = _context.Lichsulamviecs.FirstOrDefault(x => x.MaTaiKhoan == UsingAccount.Instance.Taikhoan.MaTaiKhoan && x.NgayLamViec == DateTime.Today);

                chitietlamviec.MaLslv = lslv.MaLslv;
                chitietlamviec.HoatDong = "Thêm bài đăng " + value.TenCongViec;
                chitietlamviec.MaBaiDang = value.MaBaiDang;
                chitietlamviec.ThoiGian = DateTime.Now;

                _context.Chitietlamviecs.Add(chitietlamviec);
                await _context.SaveChangesAsync();

                TempData["success"] = "Tạo thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(form);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _context.Baidangs.Include(x => x.MaKiNangs).Where(x => x.MaBaiDang == id && x.IsDeleted == false).FirstOrDefault();
            if (post == null)
            {
                return NotFound();
            }
            var company = _context.Hosocongties.Where(x => x.MaCongTy == post.MaCongTy && x.IsDeleted == false).FirstOrDefault();
            if (company == null)
            {
                return NotFound();
            }

            ViewBag.post = post;
            ViewBag.company = company;
            return View();
        }

        public async Task<IActionResult> Apply(int? id)
        {
            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Baidangs.FirstOrDefaultAsync(x => x.MaBaiDang == id && x.IsDeleted == false);
            if (post == null)
            {
                return NotFound();
            }
            var candidates = _context.Ungviens.Where(x => x.IsDeleted == false).ToList();

            ViewBag.post = post;
            ViewBag.candidates = candidates;

            return View();
        }

        public async Task<IActionResult> CandidateApply(int? candidate, int? post)
        {
            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var checkExist = _context.Ungtuyens.Where(x => x.MaUngVien == candidate && x.MaBaiDang == post).ToList();
            if (checkExist.Count != 0)
            {
                return RedirectToAction("Apply", "Post", new {id = post});
            }

            Ungvien ungvien = await _context.Ungviens.Include(x => x.MaKiNangs).Where(x => x.MaUngVien == candidate && x.IsDeleted == false).FirstOrDefaultAsync();
            Baidang baidang = await _context.Baidangs.Where(x => x.MaBaiDang == post && x.IsDeleted == false).FirstOrDefaultAsync();

            Ungtuyen ungtuyen = new Ungtuyen();
            ungtuyen.MaUngVien = ungvien.MaUngVien;
            ungtuyen.MaBaiDang = baidang.MaBaiDang;
            ungtuyen.MaUngVienNavigation = ungvien;
            ungtuyen.MaBaiDangNavigation = baidang;
            ungtuyen.ChapThuan = true;
            ungtuyen.NgayUngTuyen = DateTime.Now;

            _context.Ungtuyens.Add(ungtuyen);
            await _context.SaveChangesAsync();
            TempData["success"] = "Ứng tuyển thành công!";
            return RedirectToAction("Detail", new { id = post });
        }

        public async Task<IActionResult> AppliedCandidate(int? id)
        {
            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Baidangs.FirstOrDefaultAsync(x => x.MaBaiDang == id && x.IsDeleted == false);
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
                    Ungvien? c = await _context.Ungviens.Include(x => x.MaKiNangs).FirstOrDefaultAsync(x => x.MaUngVien == item.MaUngVien && x.IsDeleted == false);
                    if (c != null)
                        candidates.Add(c);
                }
            }

            ViewBag.candidates = candidates;
            ViewBag.MaBaiDang = id;
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }
            Baidang baidang = _context.Baidangs.Include(x => x.MaKiNangs).Where(x => x.MaBaiDang == id && x.IsDeleted == false).FirstOrDefault();
            if (baidang == null)
            {
                return NotFound();
            }

            List<Hosocongty> companyList = _context.Hosocongties.Where(x => x.IsDeleted == false).ToList();
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
            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int id = Convert.ToInt32(form["MaBaiDang"]);
            Baidang baidang = _context.Baidangs.Include(x => x.MaKiNangs).Where(x => x.MaBaiDang == id && x.IsDeleted == false).FirstOrDefault();

            foreach (Kinang kinang in baidang.MaKiNangs.ToList())
            {
                baidang.MaKiNangs.Remove(kinang);
            }

            baidang.TenCongViec = form["TenCongViec"];
            baidang.WebsiteBaiGoc = form["WebsiteBaiGoc"];
            baidang.MaCongTy = Convert.ToInt32(form["MaCongTy"]);
            baidang.MoTa = form["MoTaThem"];
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

            var chitietlamviec = new Chitietlamviec();
            var date = DateTime.Now;
            var lslv = _context.Lichsulamviecs.FirstOrDefault(x => x.MaTaiKhoan == UsingAccount.Instance.Taikhoan.MaTaiKhoan && x.NgayLamViec == DateTime.Today);

            chitietlamviec.MaLslv = lslv.MaLslv;
            chitietlamviec.HoatDong = "Sửa bài đăng " + baidang.TenCongViec;
            chitietlamviec.MaBaiDang = baidang.MaBaiDang;
            chitietlamviec.ThoiGian = DateTime.Now;

            _context.Chitietlamviecs.Add(chitietlamviec);
            _context.SaveChangesAsync();


            TempData["success"] = "Chỉnh sửa thành công!";

            return RedirectToAction("Detail", new { id = id });
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

            Baidang baidang = _context.Baidangs.Include(x => x.MaKiNangs).FirstOrDefault(x => x.MaBaiDang == id);

            if (baidang == null) return NotFound();

            /////Xoa phieu to cao lien quan toi bai dang
            //foreach (var pToCao in baidang.Phieutocaos)
            //{
            //    var pPhat = _context.Phieuphats.FirstOrDefault(x => x.MaPhieuToCao == pToCao.MaPhieuToCao);
            //    if (pPhat != null) _context.Phieuphats.Remove(pPhat);
            //    _context.Phieutocaos.Remove(pToCao);
            //}

            /////Xoa ki nang cua bai dang
            //foreach (var makn in baidang.MaKiNangs)
            //{
            //    var kinang = _context.Kinangs.FirstOrDefault(x => x.MaKiNang == makn.MaKiNang);
            //    var listknbd = new List<Baidang>(kinang.MaBaiDangs);
            //    listknbd.Remove(baidang);
            //    kinang.MaBaiDangs = listknbd;

            //}

            /////Xoa ung tuyen cua ung vien
            //var dsUngtuyen = _context.Ungtuyens.Where(x => x.MaBaiDang == baidang.MaBaiDang).ToList();
            //foreach (var ungtuyen in dsUngtuyen)
            //{
            //    var ungvien = _context.Ungviens.FirstOrDefault(x => x.MaUngVien == ungtuyen.MaUngVien);
            //    if (ungvien != null)
            //    {
            //        var listUngtuyen = new List<Ungtuyen>(ungvien.Ungtuyens);
            //        listUngtuyen.Remove(ungtuyen);
            //        ungvien.Ungtuyens = listUngtuyen;
            //    }
            //    _context.Ungtuyens.Remove(ungtuyen);
            //}

            /////Xoa lich su lam viec cua bai dang
            //var listLsLv = _context.Lichsulamviecs.Where(x => x.MaTaiKhoan == baidang.MaTaiKhoan).ToList();
            //foreach (var lslv in listLsLv)
            //{
            //    var listBaiDang = new List<Baidang>(lslv.MaBaiDangs);
            //    listBaiDang.Remove(baidang);
            //    lslv.MaBaiDangs = listBaiDang;
            //}

            //_context.Baidangs.Remove(baidang);

            baidang.IsDeleted = true;

            _context.SaveChanges();

            var chitietlamviec = new Chitietlamviec();
            var date = DateTime.Now;
            var lslv = _context.Lichsulamviecs.FirstOrDefault(x => x.MaTaiKhoan == UsingAccount.Instance.Taikhoan.MaTaiKhoan && x.NgayLamViec == DateTime.Today);

            chitietlamviec.MaLslv = lslv.MaLslv;
            chitietlamviec.HoatDong = "Xóa bài đăng " + baidang.TenCongViec;
            chitietlamviec.MaBaiDang = baidang.MaBaiDang;
            chitietlamviec.ThoiGian = DateTime.Now;

            _context.Chitietlamviecs.Add(chitietlamviec);
            _context.SaveChangesAsync();

            TempData["Success"] = "Xoá thành công!";

            return RedirectToAction("Index");
        }

        public IActionResult ExportToExcel(int? id)
        {
            if (UsingAccount.Instance.Taikhoan == null)
            {
                return RedirectToAction("Index", "Home");
            }


            Baidang post = _context.Baidangs.FirstOrDefault(x => x.MaBaiDang == id && x.IsDeleted == false);

            List<Ungvien> candidates = new List<Ungvien>();
            List<Ungtuyen> u = _context.Ungtuyens.ToList();

            foreach (var item in u)
            {
                if (item.MaBaiDang == post.MaBaiDang)
                {
                    Ungvien? c = _context.Ungviens.Include(x => x.MaKiNangs).FirstOrDefault(x => x.MaUngVien == item.MaUngVien && x.IsDeleted == false);
                    if (c != null)
                        candidates.Add(c);
                }
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Mã ứng viên";
                worksheet.Cell(currentRow, 2).Value = "Tên ứng viên";
                worksheet.Cell(currentRow, 3).Value = "Tuổi";
                worksheet.Cell(currentRow, 4).Value = "Kĩ năng";
                worksheet.Cell(currentRow, 5).Value = "Địa chỉ";
                worksheet.Cell(currentRow, 6).Value = "Email";
                worksheet.Cell(currentRow, 7).Value = "Số điện thoại";
                worksheet.Cell(currentRow, 8).Value = "Thâm niên";
                worksheet.Cell(currentRow, 9).Value = "Giới tính";

                foreach (var user in candidates)
                {
                    string kinang = "";
                    foreach (var item in user.MaKiNangs)
                    {
                        if (kinang == "")
                        {
                            kinang = item.TenKiNang;
                        }
                        else
                        {
                            kinang = kinang + ", " + item.TenKiNang;
                        }
                    }

                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.MaUngVien;
                    worksheet.Cell(currentRow, 2).Value = user.Ten;
                    worksheet.Cell(currentRow, 3).Value = user.Tuoi.ToString();
                    worksheet.Cell(currentRow, 4).Value = kinang;
                    worksheet.Cell(currentRow, 5).Value = user.DiaChi;
                    worksheet.Cell(currentRow, 6).Value = user.Email;
                    worksheet.Cell(currentRow, 7).Value = user.Sdt;
                    worksheet.Cell(currentRow, 8).Value = user.ThamNien + " năm";
                    worksheet.Cell(currentRow, 9).Value = (user.GioiTinh == 0) ? "Nam" : (user.GioiTinh == 1) ? "Nữ" : "Khác";
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "danhsachungtuyen.xlsx");
                }
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
