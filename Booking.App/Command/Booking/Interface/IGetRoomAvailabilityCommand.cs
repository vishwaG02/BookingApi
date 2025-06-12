using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Command.Booking.Interface
{
    public interface IGetRoomAvailabilityCommand
    {
        Task<ActionResult> Execute(DateTime Date);
    }
}
