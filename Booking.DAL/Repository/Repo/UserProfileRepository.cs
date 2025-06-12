using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Booking.DAL.Context;
using Booking.DAL.Entities;
using Booking.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Booking.DAL.Repository.Repo
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserProfile user)
        {
            await _context.UserProfiles.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserProfile?> GetUserByEmail(string email)
        {
            return await _context.UserProfiles
           .Where(u => u.Email == email)
           .Include(u => u.UserRoleMappings)
               .ThenInclude(urm => urm.UserRole)
                   .ThenInclude(ur => ur.RolePermissions)
                       .ThenInclude(rp => rp.Permission)
           .FirstOrDefaultAsync();
        }
    }
}
