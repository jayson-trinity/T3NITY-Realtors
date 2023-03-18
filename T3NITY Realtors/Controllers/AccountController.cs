using Microsoft.AspNetCore.Mvc;
using T3NITY_Realtors.Models;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Controllers
{
    public class AccountController : Controller
    {
        protected ICustomerServices _customerServices;
        protected ILandlordServices _landlordServices;
        protected IAdminServices _adminServices;
        protected IUserServices _userServices;


        public AccountController(ICustomerServices customerServices, ILandlordServices landlordServices, IAdminServices adminServices, IUserServices userServices)
        {
            _customerServices = customerServices;
            _landlordServices = landlordServices;
            _adminServices = adminServices;
            _userServices = userServices;   
        }


        public IActionResult Create()
        {
            //UserModel userModel = new UserModel() { Email = "kayceee", Password = "1234", PhoneNumber = 234 };
            ViewBag.Success = "Registration Successfull";
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserModel userModel)
        {
            // res is set to false to determine when process is successful.
            var res = false;
            #region Checks Type of user to register
            if (userModel.Role == "Customer")
            {
                res = _customerServices.RegisterCustomer(userModel);
            }
            else if (userModel.Role == "Landlord")
            {
                res = _landlordServices.RegisterLandlord(userModel);
            }
            #endregion

            #region Checks if res is True
            if (res)
            {
                ViewBag.Message = "Registration Successful";
                RedirectToAction("Login");
                
            }
            else
            {
                ViewBag.Message = "Registration Failed";
            }
            #endregion

            return View(userModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            try
            {
                var loginCheck = _userServices.LoginUser(user);

            if (loginCheck != null)
            {
                    HttpContext.Session.SetString("","")
                    HttpContext.Session.SetInt32("","")
                    HttpContext.Session.SetString("","")

                return View();
            }
           
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
              
            }
            
            return View(user);
        }
    }
}
