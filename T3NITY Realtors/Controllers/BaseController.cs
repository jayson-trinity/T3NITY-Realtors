using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T3NITY_Realtors.Models;

namespace T3NITY_Realtors.Controllers
{
    public class BaseController : Controller
    {

        public bool IsLoggedin;
        public UserModel? CurrentUser { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            CurrentUser = GetUser();
            ViewBag.Name = CurrentUser.FirstName;

            if (CurrentUser.Id > 0)
            {
                IsLoggedin = true;
            }
            else
            {
                IsLoggedin = false;
            }

            await base.OnActionExecutionAsync(context, next);
        }
        /// <summary>
        /// Sets current user session
        /// </summary>
        /// <param name="userModel"></param>
        public void SetUser(UserModel userModel)
        {
            HttpContext.Session.SetString(UtilData.UserName, userModel.Email);
            HttpContext.Session.SetString(UtilData.FirstName, userModel.FirstName);
            HttpContext.Session.SetString(UtilData.Role, userModel.Role);
            HttpContext.Session.SetInt32(UtilData.UserId, userModel.Id);
        }

        /// <summary>
        ///  Gets current user session
        /// </summary>
        /// <returns>UserModel</returns>
        public UserModel GetUser()
        {
            UserModel user = new();
            if (HttpContext.Session.IsAvailable && HttpContext.Session.Keys.Contains(UtilData.UserId))
            {
                user = new UserModel()
                {
                    FirstName = HttpContext.Session.GetString(UtilData.FirstName)!,
                    Email = HttpContext.Session.GetString(UtilData.UserName)!,
                    Id = (int)HttpContext.Session.GetInt32(UtilData.UserId)!,
                    Role = HttpContext.Session.GetString(UtilData.Role)!,

                };
            }

            return user!;
        }

        public bool IsNullOrEmpty(UserModel myObject)
        {
            return myObject.GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => (string)pi.GetValue(myObject))
                .Any(value => string.IsNullOrEmpty(value));
        }


        public string GetImgString(byte[] img)
        {
            return img == null ? string.Empty : "data:image/png;base64," + Convert.ToBase64String(img);
        }
    }
}
