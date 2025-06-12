using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Command.Booking.Interface
{
    public interface ICancelBookingCommand
    {
        Task<ActionResult> Execute(Guid id);
    }
}
