using T3NITY_Realtors.Models;

namespace T3NITY_Realtors.Services.IServices
{
    public interface ICustomerServices
    {
        
        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Returns True if Successfull</returns>
        bool RegisterCustomer(UserModel userModel);
    }
}
