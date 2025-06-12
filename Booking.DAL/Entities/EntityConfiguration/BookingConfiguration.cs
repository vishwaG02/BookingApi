using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Entities.EntityConfiguration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Bookings>
    {
        public void Configure(EntityTypeBuilder<Bookings> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Property(b => b.RoomId)
                .IsRequired();

            builder.Property(b => b.UserId)
                .IsRequired();

            builder.Property(b => b.StartTime)
                .IsRequired();

            builder.Property(b => b.EndTime)
                .IsRequired();

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(b => b.Description)
                .HasMaxLength(1000);

            builder.Property(b => b.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(e => e.IsActive);

            builder.Property(b => b.CreatedDate)
                      .HasDefaultValueSql("GETDATE()");

            builder.ToTable(nameof(Bookings));
        }
    }
}
