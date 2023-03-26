using T3NITY_Realtors.Models;

namespace T3NITY_Realtors.Services.IServices
{
    public interface IAdminServices
    {
        bool RegisterAdmin(UserModel userModel);
        bool UpdateProfile(UserModel userModel);
    }
}
