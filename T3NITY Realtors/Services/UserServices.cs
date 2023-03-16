using T3NITY_Realtors.Entities;
using T3NITY_Realtors.Models;
using T3NITY_Realtors.Repository;
using T3NITY_Realtors.Repository.IRepository;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Services
{
    public class UserServices : IUserServices
    {
        protected IDbOperations _DbOperations;
        public UserServices(IDbOperations dbOperations)
        {
            _DbOperations = dbOperations;
        }
        public UserModel LoginUser(UserModel uM)
        {

            try
            {
                var user = _DbOperations.UsersRepository().Find(u => u.Username == uM.Email) ?? throw new Exception("Incorrect Username");

                if (user.Password == uM.Password)
                {
                    //test am
                    //
                    var details = GetUserDetails(user, _DbOperations);
                    return new UserModel { LastName = details.LastName, Id = user.Id, Email = user.Username, Role = user.Role };
                }
                else
                {
                    throw new Exception("Incorrect password");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static dynamic GetUserDetails(Users users, IDbOperations dbOperations)
        {
            if (users.Role == UtilData.Customer) 
            {
                var cust = dbOperations.CustomersRepository().Find(c => c.UsersId == users.Id);
                return cust;
            }

            if (users.Role == UtilData.Landlord)
            {
                var land = dbOperations.LandlordsRepository().Find(c => c.UsersId == users.Id);
                return land;
            }
            return null;
        }

    }
}
