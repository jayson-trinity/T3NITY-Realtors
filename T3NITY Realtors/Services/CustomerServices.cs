using T3NITY_Realtors.Entities;
using T3NITY_Realtors.Models;
using T3NITY_Realtors.Repository.IRepository;
using T3NITY_Realtors.Services.IServices;

namespace T3NITY_Realtors.Services
{
    public class CustomerServices : ICustomerServices
    {
        protected IDbOperations _DbOperations;
        public CustomerServices(IDbOperations dbOperations)
        {
            _DbOperations = dbOperations;
        }

        public bool RegisterCustomer(UserModel userModel)
        {
            var tranz = _DbOperations.GetDbContext();

            try
            {
                tranz.BeginTransaction();
                if (userModel != null)
                {
                    Users users = new()
                    {
                        Password = userModel.Password,
                        Role = userModel.Role,
                        Username = userModel.Email
                    };

                    var dbUser = _DbOperations.UsersRepository().Add(users);

                    Customer customers = new()
                    {
                        Email = userModel.Email,
                        PhoneNumber = userModel.PhoneNumber,
                        FirstName = userModel.FirstName,
                        LastName = userModel.LastName,
                        UsersId = dbUser.Id
                    };
                    var dbCustomer = _DbOperations.CustomersRepository().Add(customers);
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

                    var dbCustomer = _DbOperations.CustomersRepository().Find(l => l.UsersId == userModel.Id);
                    dbCustomer.Email = userModel.Email;
                    dbCustomer.PhoneNumber = userModel.PhoneNumber;
                    dbCustomer.FirstName = userModel.FirstName;
                    dbCustomer.LastName = userModel.LastName;
                    _DbOperations.CustomersRepository().Update(dbCustomer, userModel.Id);
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return false;
        }

        public Customer GetCustomerByID(int userId)
        {

            try
            {
                return _DbOperations.CustomersRepository().Find(c => c.UsersId == userId);
            }
            catch (Exception)
            {

                throw;
            }

            return null;
        }
    }
}
