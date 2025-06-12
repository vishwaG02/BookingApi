using Booking.DAL.Enum;

namespace Booking.DAL.Entities
{
    public class Bookings : BaseEntityClass
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid RoomId { get; set; }

        public int UserId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public BookingStatus Status { get; set; }

        public Room Room { get; set; }

        public UserProfile? User { get; set; }
    }
}
