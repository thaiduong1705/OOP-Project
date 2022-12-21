using Microsoft.AspNetCore.Mvc;

namespace JobRecommendationWeb.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
