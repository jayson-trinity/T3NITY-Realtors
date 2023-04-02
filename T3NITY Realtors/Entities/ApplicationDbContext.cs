using Microsoft.EntityFrameworkCore;

namespace T3NITY_Realtors.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Landlord> Landlords { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<ListingImages> ListingImages { get; set; }
        public virtual DbSet<Listings> Listings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
            });
            SeedUsers(builder);
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        private void SeedUsers(ModelBuilder builder)
        {
            builder.Entity<Users>().HasData(new Users()
            {
                Id = 1,
                Role = UtilData.Admin,
                Username = "admin@gmail.com",
                Password = UtilData.GetHash("Admin*123")
            });
            builder.Entity<Admin>().HasData(new Admin()
            {
                Email = "admin@gmail.com",
                FirstName = "AdminF",
                LastName = "AdminL",
                PhoneNumber = "00000000000",
                UsersId = 1,
                Id = 1,
            });
        }
    }

}
