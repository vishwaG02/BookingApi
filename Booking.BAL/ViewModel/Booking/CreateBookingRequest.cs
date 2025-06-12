using System.ComponentModel.DataAnnotations;

namespace Booking.BAL.ViewModel.Booking
{
    public class CreateBookingRequest
    {
        [Required]
        public Guid RoomId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        // Optional field
        public string? Description { get; set; }
    }
}
