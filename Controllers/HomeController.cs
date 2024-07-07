using Microsoft.AspNetCore.Mvc;

namespace API_ERP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
