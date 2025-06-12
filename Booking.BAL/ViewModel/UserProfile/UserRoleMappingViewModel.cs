using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BAL.ViewModel.UserProfile
{
    public class UserRoleMappingViewModel
    {
        public int UserRoleId { get; set; }

        public int? RoleId { get; set; }

        public int? UserId { get; set; }

        [NotMapped]
        public UserRoleViewModel? UserRole { get; set; }

        public UserProfileViewModel? UserProfile { get; set; }
    }
}
