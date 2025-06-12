using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.DAL.Entities;
using Booking.DAL.Entities.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Booking.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserProfileConfiguration).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingConfiguration).Assembly);

            // ... other customizations if needed


            base.OnModelCreating(modelBuilder);
        }
    }
}
