using T3NITY_Realtors.Entities;
using T3NITY_Realtors.Models;
using T3NITY_Realtors.Repository.IRepository;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Services
{
    public class AdminServices : IAdminServices
    {
        protected IDbOperations _DbOperations;
        public AdminServices(IDbOperations dbOperations)
        {
            _DbOperations = dbOperations;
        }

        public bool RegisterAdmin(UserModel userModel)
        {

            try
            {
                if (userModel != null)
                {
                    Users users = new Users()
                    {
                        Password = userModel.Password,
                        Role = userModel.Role,
                        Username = userModel.Email
                    };

                    var dbUser = _DbOperations.UsersRepository().Add(users);

                    Admin admin = new Admin()
                    {
                        Email = userModel.Email,
                        PhoneNumber = userModel.PhoneNumber,
                        FirstName = userModel.FirstName,
                        LastName = userModel.LastName,
                        UsersId = dbUser.Id
                    };
                    var dbAdmin = _DbOperations.AdminRepository().Add(admin);
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }



            return false;
        }
    }
}
