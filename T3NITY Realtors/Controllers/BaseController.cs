using Microsoft.AspNetCore.Mvc;
using T3NITY_Realtors.Models;

namespace T3NITY_Realtors.Controllers
{
    public class BaseController : Controller
    {
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
            UserModel user = null;
            if (HttpContext.Session.IsAvailable && HttpContext.Session.Keys.Contains(UtilData.UserId))
            {
                user = new UserModel()
                {
                    FirstName = HttpContext.Session.GetString(UtilData.FirstName)!,
                    Email = HttpContext.Session.GetString(UtilData.UserName)!,
                    Id = (int)HttpContext.Session.GetInt32(UtilData.UserId)!,
                    Role = HttpContext.Session.GetString(UtilData.Role)!,

                };
                if (IsNullOrEmpty(user))
                {
                    return null;
                }
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
    }
}
