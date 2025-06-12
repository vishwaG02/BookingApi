using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.BAL.ViewModel.UserProfile;

namespace Booking.BAL.Service.Interface
{
    public interface IUserProfileService
    {
        Task AddUser(UserProfileViewModel model);

        Task<string> Login(LoginRequest request);
    }
}
