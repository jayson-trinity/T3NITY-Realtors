using Microsoft.AspNetCore.Mvc;

namespace T3NITY_Realtors.Controllers
{
    public class BaseController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
