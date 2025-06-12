using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Booking.DAL.Entities.EntityConfiguration
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.HasIndex(e => e.UserId);

            builder.HasIndex(e => e.Email)
                .IsUnique();

            builder.Property(e => e.Email)
                .HasMaxLength(256);

            builder.Property(e => e.Password)
                .HasMaxLength(500);

            builder.Property(e => e.FirstName)
                .HasMaxLength(256);

            builder.Property(e => e.LastName)
                .HasMaxLength(256);

            builder.HasQueryFilter(e => e.IsActive);

            builder.ToTable(nameof(UserProfile));
        }
    }
}
