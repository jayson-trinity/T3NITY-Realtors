using Microsoft.AspNetCore.Mvc;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Controllers
{
    public class ListingController : BaseController
    {
        protected IListingsServices _listingsServices;

        public ListingController(IListingsServices listingsServices)
        {
            _listingsServices = listingsServices;
        }

        public IActionResult Listings()
        {
            var listData = _listingsServices.GetAllListings();
            return View(listData);
        }

        public IActionResult ViewListing(int id)
        {
            var listData = _listingsServices.GetListingById(id);
            return View(listData);
        }
    }
}
