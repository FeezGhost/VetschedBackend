using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Vetsched.Data.Entities;
using Vetsched.Data.Enums;

namespace Vetsched.Data.DBContexts
{
    public class VetschedContext : IdentityDbContext<
     ApplicationUser, ApplicationRole, Guid,
     IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>,
     IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public static void RegisterType()
        {

            NpgsqlConnection.GlobalTypeMapper.MapEnum<Gender>();
        }
        public VetschedContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(m => m.Deleted == false);
            modelBuilder.Entity<ApplicationUserRole>().HasQueryFilter(m => m.Deleted == false);
            modelBuilder.Entity<ApplicationRole>().HasQueryFilter(m => m.Deleted == false);

            modelBuilder.HasPostgresEnum<Gender>();
        }
    }
}
