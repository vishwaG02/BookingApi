using Booking.DAL.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Command.Booking.Interface
{
    public interface IGetBookingCommand
    {
        Task<ActionResult> Execute(int userid, BookingQueryParameters parameters);
    }
}
