using JobRecommendationWeb.CustomViewModel;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

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
            AddPostViewModel viewModel = new AddPostViewModel()
            {
                listKiNang = _context.Kinangs.ToList(),
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddPostViewModel viewModel)
        {
            
            return RedirectToAction("Create");
        }


        public IActionResult Detail()
        {
            return View();
        }
    }
}
