using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using T3NITY_Realtors.Models;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Controllers
{
    public class LandLordController : BaseController
    {
        protected IListingsServices _listingsServices;
        private readonly IToastNotification _toastNotification;

        public LandLordController(IListingsServices listingsServices, IToastNotification toastNotification)
        {
            _listingsServices = listingsServices;
            _toastNotification = toastNotification;
        }

        public IActionResult Dashboard()
        {
            if (IsLoggedin)
            {
                var listData = _listingsServices.GetListings(CurrentUser.Id);
                var posted = listData.Count();
                var approved = listData.Count(l => l.Status == "Approved");
                var pending = listData.Count(l => l.Status == "Pending");

                return View(new LandLordDashModel()
                {
                    Listings = listData,
                    Approved = approved,
                    Pending = pending,
                    Posted = posted
                });
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

            if (IsLoggedin)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (_listingsServices.CreateListing(model, CurrentUser.Id))
                {
                    _toastNotification.AddSuccessToastMessage($"{model.Name} Added successfully!!!");
                    return RedirectToAction("Dashboard");
                }
                _toastNotification.AddErrorToastMessage("An Error Occured...");
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }


        public IActionResult Deletlisting(int id)
        {
            if (_listingsServices.DeletListing(id))
            {
                _toastNotification.AddSuccessToastMessage("Deleted");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("An error occured...");
            }
            return RedirectToAction("Dashboard");
        }

        public IActionResult UpdateListing(int id)
        {
            if (IsLoggedin)
            {
                if (id == 0)
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
