using Microsoft.AspNetCore.Mvc;

namespace T3NITY_Realtors.Controllers
{
    public class ListingController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
