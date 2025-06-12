using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Entities.EntityConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(e => e.RoleId);

            builder.Property(e => e.RoleName)
                .HasMaxLength(128);

            builder.HasQueryFilter(e => e.IsActive);

            builder.ToTable(nameof(UserRole));
        }
    }
}
