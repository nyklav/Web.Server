using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Server
{
    public class UserAccountDbContext : IdentityDbContext<UserAccount>
    {
        public UserAccountDbContext(DbContextOptions<UserAccountDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasIndex(p => p.FirstName);
            modelBuilder.Entity<UserAccount>().Property(p => p.FirstName).HasMaxLength(75);
            modelBuilder.Entity<UserAccount>().Property(p => p.LastName).HasMaxLength(75);
            base.OnModelCreating(modelBuilder);
        }

    }
}
