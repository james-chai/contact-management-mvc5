using ContactManagement.Core;
using System.Data.Entity;

namespace ContactManagement.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext() : base("name=ContactManagementDb")
        {
            // EF6 Configuration
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(c => c.CreatedAt)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .Property(c => c.UpdatedAt)
                .IsRequired();
        }
    }
}
