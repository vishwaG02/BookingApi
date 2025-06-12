using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Entities
{
    public class UserProfile : BaseEntityClass
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid SecretKey { get; set; }

        public ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();

        public ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();
    }
}
