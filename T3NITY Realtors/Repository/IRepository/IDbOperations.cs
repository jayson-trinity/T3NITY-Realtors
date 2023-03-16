using T3NITY_Realtors.Entities;

namespace T3NITY_Realtors.Repository.IRepository
{
    public interface IDbOperations
    {
        IGenericRepository<Admin> AdminRepository();
        IGenericRepository<Customer> CustomersRepository();
        IGenericRepository<Landlord> LandlordsRepository();
        IGenericRepository<Users> UsersRepository();
    }
}
