using T3NITY_Realtors.Entities;
using T3NITY_Realtors.Models;

namespace T3NITY_Realtors.Services.IServices
{
    public interface ICustomerServices
    {
        Customer GetCustomerByID(int id);

        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Returns True if Successfull</returns>
        bool RegisterCustomer(UserModel userModel);
        bool UpdateProfile(UserModel userModel);
    }
}
