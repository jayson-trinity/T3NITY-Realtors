using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using T3NITY_Realtors.Models;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Controllers
{
    public class AdminController : BaseController
    {

        protected IListingsServices _listingsServices;
        private readonly IToastNotification _toastNotification;

        public AdminController(IListingsServices listingsServices, IToastNotification toastNotification)
        {
            _listingsServices = listingsServices;
            _toastNotification = toastNotification;
        }

        public IActionResult Dashboard()
        {
            if (IsLoggedin && CurrentUser!.Role == UtilData.Admin)
            {
                var listData = _listingsServices.GetListings();
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


        public IActionResult ViewListing(int id)
        {
            if (IsLoggedin && CurrentUser!.Role == UtilData.Admin)
            {
                var listData = _listingsServices.GetListingById(id);
                return View(listData);
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AddMessage(ListingsViewModel model)
        {
            if (IsLoggedin && CurrentUser!.Role == UtilData.Admin)
            {
                if (_listingsServices.UpdateListingWithMessage(model))
                {
                    _toastNotification.AddSuccessToastMessage("Message Added");
                }
                return RedirectToAction("ViewListing", new { id = model.Id });

            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AddStatus(int id, string status)
        {
            if (IsLoggedin && CurrentUser!.Role == UtilData.Admin)
            {
                if (_listingsServices.ChangeStatusListing(id, status))
                {
                    _toastNotification.AddSuccessToastMessage("Property " + status);
                    return RedirectToAction("Dashboard");
                }
                _toastNotification.AddErrorToastMessage("An Error Occured. Ensure you add Message.");
                return RedirectToAction("ViewListing", new { id });
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
