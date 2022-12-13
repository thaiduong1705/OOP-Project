using JobRecommendationWeb.Models;
using Microsoft.AspNetCore.Mvc;
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
            List<Kinang> skillList = _context.Kinangs.ToList();
            return View(skillList);
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
    }
}