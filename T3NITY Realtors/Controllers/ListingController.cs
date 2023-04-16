using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Controllers
{
    public class ListingController : BaseController
    {
        protected IListingsServices _listingsServices;
        protected IPaymentServices _paymentServices;
        protected ICustomerServices _customerServices;
        private readonly IToastNotification _toastNotification;

        public ListingController(IListingsServices listingsServices, IPaymentServices paymentServices, ICustomerServices customerServices, IToastNotification toastNotification)
        {
            _listingsServices = listingsServices;
            _paymentServices = paymentServices;
            _customerServices = customerServices;
            _toastNotification = toastNotification;
        }

        public IActionResult Listings()
        {
            if (!IsLoggedin)
            {
                return RedirectToAction("Login", "Account");
            }
            var listData = _listingsServices.GetAllListings();
            return View(listData);
        }

        public IActionResult ViewListing(int id)
        {
            if (!IsLoggedin)
            {
                return RedirectToAction("Login", "Account");
            }
            var listData = _listingsServices.GetListingById(id);
            return View(listData);
        }

        public async Task<IActionResult> PayForListing(int id)
        {
            if (!IsLoggedin)
            {
                return RedirectToAction("Login", "Account");
            }
            var listData = _listingsServices.GetListingById(id);
            var cust = _customerServices.GetCustomerByID(CurrentUser.Id);
            var url = await _paymentServices.Pay(listData, cust);
            if (url != null)
            {
                _toastNotification.AddSuccessToastMessage("Going to Payment Page");
                return Redirect(url);
            }
            return View("Listings");
        }
        public async Task<IActionResult> VerifyListingPayment(string invoiceId)
        {
            if (!IsLoggedin)
            {
                return RedirectToAction("Login", "Account");
            }
            var res = await _paymentServices.VerifyPay(invoiceId);
            if (res)
            {
                _toastNotification.AddSuccessToastMessage("Payment Completed.");
                return Redirect("Listings");
            }
            return View("Listings");
        }
    }
}
