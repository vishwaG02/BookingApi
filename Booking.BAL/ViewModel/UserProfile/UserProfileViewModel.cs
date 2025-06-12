using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BAL.ViewModel.UserProfile
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid SecretKey { get; set; }

        public int? RoleId { get; set; } = 2;
    }
}
