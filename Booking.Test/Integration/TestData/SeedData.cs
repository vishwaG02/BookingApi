using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.DAL.Context;
using Booking.DAL.Entities;
using Booking.DAL.Enum;
using Booking.Test.Integration.Constant;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Test.Integration.TestData
{
    public static class SeedData
    {
        private static readonly IPasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        public static void SeedDatabase(ApplicationDbContext context)
        {    
            if (context.UserRoles.Any() || context.Permissions.Any())
                return; // Skip if already seeded

            var roles = new List<UserRole>
            {
                new UserRole { RoleId = 1, RoleName = "Admin", IsActive = true, CreatedDate = DateTime.UtcNow },
                new UserRole { RoleId = 2, RoleName = "Customer", IsActive = true, CreatedDate = DateTime.UtcNow }
            };

            var permissions = new List<Permission>
            {
                new Permission { Id = 1, Name = "CreateUser", IsActive = true, CreatedDate = DateTime.UtcNow },
                new Permission { Id = 2, Name = "EditUser", IsActive = true, CreatedDate = DateTime.UtcNow },
                new Permission { Id = 3, Name = "GetUser", IsActive = true, CreatedDate = DateTime.UtcNow },
                new Permission { Id = 4, Name = "CreateBooking", IsActive = true, CreatedDate = DateTime.UtcNow },
                new Permission { Id = 5, Name = "EditBooking", IsActive = true, CreatedDate = DateTime.UtcNow },
                new Permission { Id = 6, Name = "GetBooking", IsActive = true, CreatedDate = DateTime.UtcNow },
                new Permission { Id = 7, Name = "CancelBooking", IsActive = true, CreatedDate = DateTime.UtcNow },
                new Permission { Id = 8, Name = "DeleteUser", IsActive = true, CreatedDate = DateTime.UtcNow },
                new Permission { Id = 9, Name = "Dashboard", IsActive = true, CreatedDate = DateTime.UtcNow }
            };

            context.UserRoles.AddRange(roles);
            context.Permissions.AddRange(permissions);
            context.SaveChanges();

            var adminRole = roles.First(r => r.RoleName == "Admin");
            var customerRole = roles.First(r => r.RoleName == "Customer");

            var adminPermissions = permissions.Select(p => new RolePermission
            {
                RoleId = adminRole.RoleId,
                Role = adminRole,
                PermissionId = p.Id,
                Permission = p,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            }).ToList();

            var customerPermissionNames = new[] { "CreateBooking", "EditBooking", "GetBooking", "Dashboard" };
            var customerPermissions = permissions
                .Where(p => customerPermissionNames.Contains(p.Name))
                .Select(p => new RolePermission
                {
                    RoleId = customerRole.RoleId,
                    Role = customerRole,
                    PermissionId = p.Id,
                    Permission = p,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                }).ToList();
            
            adminRole.RolePermissions = adminPermissions;
            customerRole.RolePermissions = customerPermissions;

            var adminUser = new UserProfile
            {
                UserId = 4,
                FirstName = "Super",
                LastName = "Admin",
                Email = "admin@test.com",
                Password = _passwordHasher.HashPassword(TestingConstants.UserSecretKey, "Temp@123"),
                SecretKey = Guid.NewGuid(),
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            var adminMapping = new UserRoleMapping
            {
                RoleId = adminRole.RoleId,
                UserRole = adminRole,
                UserId = adminUser.UserId,
                UserProfile = adminUser,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            var date = DateTime.Today;
            var bookings = new List<Bookings>
            {


                new Bookings {
                    Id = Guid.Parse("a1f5a1b7-2c4b-4b7e-a1b0-1234567890ab"),
                    RoomId =  Guid.Parse("b2d6b2c8-3d5c-4c8f-b2c1-abcdefabcdef"),
                    UserId = 4,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),   // 12:00 PM today
                    Title = "Client Presentation",
                    Description = "Presenting Q2 results to client",
                    Status = BookingStatus.Cancelled,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                },
                  new Bookings {
                    Id = Guid.Parse("a1f5a1b7-2c4b-4b7e-a1b0-1234567890ad"),
                    RoomId =  Guid.Parse("d8a5f740-0e9f-4a87-9b1d-50b7dbfc98ad"),
                    UserId = 4,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),   // 12:00 PM today
                    Title = "Client Presentation",
                    Description = "Presenting Q2 results to client",
                    Status = BookingStatus.Confirmed,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                },

                new Bookings
                {
                     Id = Guid.Parse("a1f5a1b7-2c4b-4b7e-a1b0-1234567890ae"),
                    RoomId =  Guid.Parse("f4b9a4c6-2dcb-4567-8a9f-6e02b6e88b22"),
                    UserId = 4,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),   // 12:00 PM today
                    Title = "Client Presentation",
                    Description = "Presenting Q2 results to client",
                    Status = BookingStatus.Confirmed,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                },
                new Bookings
                {
                    Id = Guid.NewGuid(),
                    RoomId = Guid.Parse("b2d6b2c8-3d5c-4c8f-b2c1-abcdefabcdef"),
                    UserId = 4,
                    StartTime = date.AddHours(10), // 10:00
                    EndTime = date.AddHours(12),   // 12:00
                    Title = "Partial Meeting",
                    Description = "Leaves availability before and after",
                    Status = BookingStatus.Cancelled,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                },
                 new Bookings
                {
                    Id = Guid.Parse("d3e5c6a1-9a8b-4c2a-8c0d-9876543210fe"), // new booking ID
                    RoomId = Guid.Parse("c1f3d5e8-2a7b-4d91-a2c1-ffeeddccbbaa"), // different room ID
                    UserId = 4,
                    StartTime = DateTime.Today.AddHours(14),  // 2:00 PM today
                    EndTime = DateTime.Today.AddHours(15),    // 3:00 PM today
                    Title = "Cancelled Meeting",
                    Description = "Meeting that was cancelled",
                    Status = BookingStatus.Cancelled, // <-- Important
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                }



            };

            var now = DateTime.Now;
            var availableFrom = TimeOnly.FromDateTime(now.AddHours(-1));
            var availableTo = TimeOnly.FromDateTime(now.AddMinutes(65));
            var rooms = new List<Room>

            {
    new Room
    {
        Id = Guid.Parse("b2d6b2c8-3d5c-4c8f-b2c1-abcdefabcdef"),
        Name = "Conference Room A",
        Location = "1st Floor",
        Capacity = 10,
        Amenities = "Projector, Whiteboard, AC",
        AvailableFrom = availableFrom,
        AvailableTo = availableTo,
        CreatedDate = DateTime.UtcNow,
        IsActive = true
    },
    new Room
    {
    Id = Guid.Parse("c3f7e3a4-9c5d-4d9b-9c31-fedcba987654"),
    Name = "Executive Meeting Room",
    Location = "2nd Floor",
    Capacity = 6,
    Amenities = "TV Screen, Coffee Machine, AC",
    AvailableFrom =  TimeOnly.FromDateTime(now.AddHours(1)), // 9:00 AM
    AvailableTo =  TimeOnly.FromDateTime(now.AddHours(1)), // 6:00 PM

    CreatedDate = DateTime.UtcNow,
    IsActive = true
    },
    new Room
    {
        Id = Guid.Parse("d8a5f740-0e9f-4a87-9b1d-50b7dbfc98ad"),
        Name = "Conference Room A",
        Location = "1st Floor",
        Capacity = 10,
        Amenities = "Projector, Whiteboard, AC",
        AvailableFrom = availableFrom,
        AvailableTo = availableTo,
        CreatedDate = DateTime.UtcNow,
        IsActive = true
    },
    new Room
    {
        Id = Guid.Parse("f4b9a4c6-2dcb-4567-8a9f-6e02b6e88b22"),
        Name = "Meeting Room B",
        Location = "2nd Floor",
        Capacity = 6,
        Amenities = "TV, AC",
        AvailableFrom = TimeOnly.FromDateTime(now.AddHours(1)),
        AvailableTo = TimeOnly.FromDateTime(now.AddHours(1)),

        CreatedDate = DateTime.UtcNow,
        IsActive = true
    }

};

            adminUser.UserRoleMappings = new List<UserRoleMapping> { adminMapping };
            adminRole.UserRoleMappings = new List<UserRoleMapping> { adminMapping };

            context.UserProfiles.Add(adminUser);
            context.UserRoleMappings.Add(adminMapping);
            context.RolePermissions.AddRange(adminPermissions);
            context.RolePermissions.AddRange(customerPermissions);
            context.Bookings.AddRange(bookings);
            context.Rooms.AddRange(rooms);

            context.SaveChanges();
        }
    }
}
