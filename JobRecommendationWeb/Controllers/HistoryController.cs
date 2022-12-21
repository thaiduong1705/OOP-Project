using Microsoft.AspNetCore.Mvc;

namespace JobRecommendationWeb.Controllers
{
    public class HistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
