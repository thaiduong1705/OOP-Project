using Microsoft.AspNetCore.Mvc;

namespace JobRecommendationWeb.Controllers
{
    // quan ly nhan vien
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
