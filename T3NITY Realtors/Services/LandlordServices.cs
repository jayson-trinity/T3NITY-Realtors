using T3NITY_Realtors.Entities;
using T3NITY_Realtors.Models;
using T3NITY_Realtors.Repository.IRepository;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Services
{
    public class LandlordServices : ILandlordServices
    {
        protected IDbOperations _DbOperations;
        public LandlordServices(IDbOperations dbOperations)
        {
            _DbOperations = dbOperations;
        }

        public bool RegisterLandlord(UserModel userModel)
        {

            try
            {
                if (userModel != null)
                {
                    Users users = new()
                    {
                        Password = userModel.Password,
                        Role = userModel.Role,
                        Username = userModel.Email
                    };

                    var dbUser = _DbOperations.UsersRepository().Add(users);

                    Landlord landlord = new()
                    {
                        Email = userModel.Email,
                        PhoneNumber = userModel.PhoneNumber,
                        FirstName = userModel.FirstName,
                        LastName = userModel.LastName,
                        UsersId = dbUser.Id
                    };
                    var dbLandlord = _DbOperations.LandlordsRepository().Add(landlord);
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return false;
        }

        public bool UpdateProfile(UserModel userModel)
        {

            try
            {
                if (userModel != null)
                {

                    var dbLandlord = _DbOperations.LandlordsRepository().Find(l => l.UsersId == userModel.Id);
                    dbLandlord.Email = userModel.Email;
                    dbLandlord.PhoneNumber = userModel.PhoneNumber;
                    dbLandlord.FirstName = userModel.FirstName;
                    dbLandlord.LastName = userModel.LastName;
                    _DbOperations.LandlordsRepository().Update(dbLandlord, userModel.Id);
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
