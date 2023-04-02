using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using T3NITY_Realtors.Models;

namespace T3NITY_Realtors.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            if (CurrentUser.Role == UtilData.Customer)
            {
                return RedirectToAction("Listings", "Listing");
            }

            if (CurrentUser.Role == UtilData.Landlord)
            {
                return RedirectToAction("Dashboard", "LandLord");
            }

            if (CurrentUser.Role == UtilData.Admin)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}