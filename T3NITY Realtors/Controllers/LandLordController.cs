using Microsoft.AspNetCore.Mvc;
using T3NITY_Realtors.Models;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Controllers
{
    public class LandLordController : BaseController
    {
        protected IListingsServices _listingsServices;

        public LandLordController(IListingsServices listingsServices)
        {
            _listingsServices = listingsServices;
        }

        public IActionResult Dashboard()
        {
            if (IsLoggedin)
            {
                var listData = _listingsServices.GetListings(CurrentUser.Id);
                var posted = listData.Count();
                var approved = listData.Count(l => l.Status == "Approved");
                var pending = listData.Count(l => l.Status == "Pending");

                return View(new LandLordDashModel() { 
                    Listings = listData, Approved = approved, Pending = pending, Posted = posted });
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult CreateListing()
        {
            if (IsLoggedin)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }  
        
        [HttpPost]
        public IActionResult CreateListing(ListingsModel model)
        {
            var cj = ModelState.IsValid;
            if (IsLoggedin)
            {
                var check = _listingsServices.CreateListing(model, CurrentUser.Id);
                return View();
            }
            return RedirectToAction("Login", "Account");
        }  
        
        
        
        
        public IActionResult UpdateListing(int id)
        {
            if (IsLoggedin)
            {
                if (id==0)
                {
                    return View();
                }
                var listData = _listingsServices.GetListingById(id);
              
                return View(listData);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
