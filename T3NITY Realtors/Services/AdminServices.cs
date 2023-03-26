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
            var tranz = _DbOperations.GetDbContext();
            try
            {
                tranz.BeginTransaction();
                if (userModel != null)
                {
                    Users users = new ()
                    {
                        Password = userModel.Password,
                        Role = userModel.Role,
                        Username = userModel.Email
                    };

                    var dbUser = _DbOperations.UsersRepository().Add(users);

                    Admin admin = new()
                    {
                        Email = userModel.Email,
                        PhoneNumber = userModel.PhoneNumber,
                        FirstName = userModel.FirstName,
                        LastName = userModel.LastName,
                        UsersId = dbUser.Id
                    };
                    var dbAdmin = _DbOperations.AdminRepository().Add(admin);
                    tranz.CommitTransaction();
                    return true;
                }
            }
            catch (Exception)
            {
                tranz.RollbackTransaction();

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

                    var dbAdmin = _DbOperations.AdminRepository().Find(l => l.UsersId == userModel.Id);
                    dbAdmin.Email = userModel.Email;
                    dbAdmin.PhoneNumber = userModel.PhoneNumber;
                    dbAdmin.FirstName = userModel.FirstName;
                    dbAdmin.LastName = userModel.LastName;
                    _DbOperations.AdminRepository().Update(dbAdmin, userModel.Id);
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
