using Microsoft.AspNet.Identity;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Users>(entity => {
                entity.HasIndex(e => e.Username).IsUnique();
            });
        }

    }

}
