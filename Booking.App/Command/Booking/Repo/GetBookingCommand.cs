using Booking.App.Command.Booking.Interface;
using Booking.BAL.Constant;
using Booking.BAL.Helper;
using Booking.BAL.Service.Interface;
using Booking.DAL.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Command.Booking.Repo
{
    public class GetBookingCommand : IGetBookingCommand
    {
        private readonly IBookingService _bookingService;
        public GetBookingCommand(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<ActionResult> Execute(int userid, BookingQueryParameters parameters)
        {
            var errors = new List<ErrorDetailResponse>();

            var isValidUser = await _bookingService.isValidUser(userid);
            if (!isValidUser)
            {
                errors.Add(new ErrorDetailResponse
                {
                    Field = nameof(userid),
                    Issue = BookingConstants.InvalidUser
                });
            }

            if (parameters.fromDate > parameters.toDate)
            {
                errors.Add(new ErrorDetailResponse
                {
                    Field = nameof(parameters.toDate),
                    Issue = BookingConstants.EndTimeBeforeStartTime
                });
            }

            if (errors.Any())
            {
                return ErrorResponseBuilder.BadRequest(errors);
            }

            var result = await _bookingService.GetBooking(userid, parameters);
            return new OkObjectResult(result);
        }
    }
}
