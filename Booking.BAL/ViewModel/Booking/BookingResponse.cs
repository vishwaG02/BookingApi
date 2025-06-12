namespace Booking.BAL.ViewModel.Booking
{
    public class BookingResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid RoomId { get; set; }

        public int UserId { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }
    }
}
