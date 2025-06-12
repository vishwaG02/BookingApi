using Booking.BAL.ViewModel.Booking;
using Swashbuckle.AspNetCore.Filters;

public class CreateBookingRequestExample : IExamplesProvider<CreateBookingRequest>
{
    public CreateBookingRequest GetExamples()
    {
        return new CreateBookingRequest
        {
            RoomId = Guid.Parse("21a57f52-0cd4-4b4c-9a96-234f50ae489f"),
            UserId = 102,
            StartTime = DateTime.UtcNow.AddHours(1),
            EndTime = DateTime.UtcNow.AddHours(2),
            Title = "Team Sync",
            Description = "Weekly standup meeting"
        };
    }
}