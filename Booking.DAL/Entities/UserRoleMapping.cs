using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Entities
{
    public class UserRoleMapping : BaseEntityClass
    {
        public int UserRoleId { get; set; }

        public int? RoleId { get; set; }

        public int? UserId { get; set; }

        public UserRole? UserRole { get; set; }

        public UserProfile? UserProfile { get; set; }
    }
}
