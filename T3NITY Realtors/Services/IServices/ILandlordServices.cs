using T3NITY_Realtors.Models;

namespace T3NITY_Realtors.Services.IServices
{
    public interface ILandlordServices
    {
       
        bool RegisterLandlord(UserModel userModel);
        bool UpdateProfile(UserModel userModel);
    }
}
