using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Entities
{
    public class UserRole : BaseEntityClass
    {
        public int RoleId { get; set; } 

        public string RoleName { get; set; } = string.Empty;

        public ICollection<UserRoleMapping?> UserRoleMappings { get; set; }

        public ICollection<RolePermission?> RolePermissions { get; set; }

    }
}
