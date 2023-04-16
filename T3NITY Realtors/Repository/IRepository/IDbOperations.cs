using Microsoft.EntityFrameworkCore.Infrastructure;
using T3NITY_Realtors.Entities;

namespace T3NITY_Realtors.Repository.IRepository
{
    public interface IDbOperations
    {
        IGenericRepository<Admin> AdminRepository();
        IGenericRepository<Customer> CustomersRepository();
        DatabaseFacade GetDbContext();
        IGenericRepository<Landlord> LandlordsRepository();
        IGenericRepository<ListingImages> ListingImagedRepository();
        IGenericRepository<Listings> ListingsRepository();
        IGenericRepository<Payments> PaymentsRepository();
        IGenericRepository<Users> UsersRepository();
    }
}
