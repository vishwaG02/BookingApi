using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.DAL.Entities;

namespace Booking.DAL.Repository.Interface
{
    public interface IUserProfileRepository
    {
        Task AddAsync(UserProfile user);

        Task<UserProfile?> GetUserByEmail(string email);

    }
}
