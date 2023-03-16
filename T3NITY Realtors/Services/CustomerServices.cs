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

                    Customer customers = new Customer()
                    {
                        Email = userModel.Email,
                        PhoneNumber = userModel.PhoneNumber,
                        FirstName = userModel.FirstName,
                        LastName = userModel.LastName,
                        UsersId = dbUser.Id
                    };
                    var dbCustomer = _DbOperations.CustomersRepository().Add(customers);
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
