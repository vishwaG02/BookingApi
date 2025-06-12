using Azure.Core;
using Booking.App.Command.Booking.Interface;
using Booking.App.Common;
using Booking.BAL.Constant;
using Booking.BAL.Helper;
using Booking.BAL.Service.Interface;
using Booking.DAL.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Booking.App.Command.Booking.Repo
{
    public class CancelBookingCommand : ICancelBookingCommand
    {
        private readonly IBookingService _bookingService;
        private readonly ClaimsHelper _claimsHelper;

        public CancelBookingCommand(IBookingService bookingService, ClaimsHelper claimsHelper)
        {
            _bookingService = bookingService;
            _claimsHelper = claimsHelper;
        }

        public async Task<ActionResult> Execute(Guid bookingId)
        {
            var errors = new List<ErrorDetailResponse>();
            //var currentUserId = 0;
            //var isAdmin = _claimsHelper.IsAdmin();

            //currentUserId = isAdmin ? currentUserId: _claimsHelper.GetUserId();

            var booking = await _bookingService.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                errors.Add(new ErrorDetailResponse
                {
                    Field = nameof(bookingId),
                    Issue = BookingConstants.BookingNotFound
                });
            }
            else if (booking.EndTime < DateTime.Now)
            {
                errors.Add(new ErrorDetailResponse
                {
                    Field = nameof(bookingId),
                    Issue = BookingConstants.PastBookingCancel
                });

            }

            if (errors.Any())
                return ErrorResponseBuilder.BadRequest(errors);

            await _bookingService.CancelBookingAsync(booking);
            return new NoContentResult();
        }
    }
}
