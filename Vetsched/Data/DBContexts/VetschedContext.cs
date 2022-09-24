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
        public DbSet<Service> Service { get; set; }
        public DbSet<Pet> Pet { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public static void RegisterType()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Gender>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ProfileType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ServiceCategory>();
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
            modelBuilder.Entity<Pet>().HasQueryFilter(m => m.Deleted == false);
            modelBuilder.Entity<Service>().HasQueryFilter(m => m.Deleted == false);
            modelBuilder.Entity<UserProfile>().HasQueryFilter(m => m.Deleted == false);

            modelBuilder.HasPostgresEnum<Gender>();
            modelBuilder.HasPostgresEnum<ProfileType>();
            modelBuilder.HasPostgresEnum<ServiceCategory>();

            modelBuilder.Entity<Service>()
             .HasMany<UserProfile>(s => s.Providers)
             .WithMany(c => c.Services);


            modelBuilder.Entity<Pet>()
             .HasOne<UserProfile>(s => s.PetLover)
             .WithMany(p => p.Pets);

            modelBuilder.Entity<UserProfile>()
                .HasOne<ApplicationUser>(up => up.User)
                .WithMany(au => au.Profiles)
                .HasForeignKey(up => up.UserId);
            //.
            //.Map(cs =>
            //{
            //    cs.MapLeftKey("ServicesRefId");
            //    cs.MapRightKey("ProviderRefId");
            //    cs.ToTable("provider_services");
            //});
        }
    }
}
