using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Booking.DAL.Entities.EntityConfiguration
{
    public class UserRoleMappingConfiguration : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.HasKey(e => e.UserRoleId);

            builder.HasOne(e => e.UserProfile)
                .WithMany(e => e.UserRoleMappings)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);

            builder.HasOne(e => e.UserRole)
                .WithMany(e => e.UserRoleMappings)
                .HasForeignKey(e => e.RoleId)
                .IsRequired(false);

            builder.HasQueryFilter(e => e.IsActive);

            builder.ToTable(nameof(UserRoleMapping));
        }
    }
}
