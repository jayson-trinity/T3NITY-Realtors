using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using T3NITY_Realtors.Entities;
using T3NITY_Realtors.Repository.IRepository;

namespace T3NITY_Realtors.Repository
{
    public class DbOperations : IDbOperations
    {
        protected ApplicationDbContext _context;
        public DbOperations(ApplicationDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<Users> UsersRepository() 
        {
            IGenericRepository<Users> repo = new GenericRepository<Users>(_context);
            return repo;
        }

        public IGenericRepository<Customer> CustomersRepository()
        {
            IGenericRepository<Customer> repo = new GenericRepository<Customer>(_context);
            return repo;
        }

        public IGenericRepository<Landlord> LandlordsRepository()
        {
            IGenericRepository<Landlord> repo = new GenericRepository<Landlord>(_context);
            return repo;
        }

        public IGenericRepository<Admin> AdminRepository()
        {
            IGenericRepository<Admin> repo = new GenericRepository<Admin>(_context);
            return repo;
        }

        public IGenericRepository<Listings> ListingsRepository()
        {
            IGenericRepository<Listings> repo = new GenericRepository<Listings>(_context);
            return repo;
        }
        
        public IGenericRepository<ListingImages> ListingImagedRepository()
        {
            IGenericRepository<ListingImages> repo = new GenericRepository<ListingImages>(_context);
            return repo;
        }

        public DatabaseFacade GetDbContext()
        {
            return _context.Database;
        }
    }
}
