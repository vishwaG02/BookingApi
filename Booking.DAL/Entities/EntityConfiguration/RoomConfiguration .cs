using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Entities.EntityConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(r => r.Location)
                .HasMaxLength(255);

            builder.Property(r => r.Capacity)
                .IsRequired();

            builder.Property(r => r.Amenities)
                .HasMaxLength(1000); // Optional: enforce a max length

            builder.Property(r => r.AvailableFrom)
                .HasConversion(
                    v => v.ToTimeSpan(), // convert TimeOnly to TimeSpan
                    v => TimeOnly.FromTimeSpan(v)
                );

            builder.Property(r => r.AvailableTo)
                .HasConversion(
                    v => v.ToTimeSpan(),
                    v => TimeOnly.FromTimeSpan(v)
                );

            builder.HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(e => e.IsActive);

            builder.ToTable(nameof(Room));
        }
    }
}
