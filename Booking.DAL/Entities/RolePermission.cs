using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Entities
{
    public class RolePermission : BaseEntityClass
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public UserRole Role { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
