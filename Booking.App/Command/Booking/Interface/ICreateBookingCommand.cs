using Booking.BAL.ViewModel.Booking;
using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Command.Booking.Interface
{
    public interface ICreateBookingCommand
    {
        Task<ActionResult> Execute(CreateBookingRequest request);
    }
}
