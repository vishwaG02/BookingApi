using Booking.App.Command.Booking.Interface;
using Booking.App.Common;
using Booking.BAL.Constant;
using Booking.BAL.Helper;
using Booking.BAL.Service.Interface;
using Booking.BAL.ViewModel.Booking;
using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Command.Booking.Repo
{
    public class CreateBookingCommand : ICreateBookingCommand
    {
        private readonly IBookingService _bookingService;
        private readonly ClaimsHelper _claimsHelper;

        public CreateBookingCommand(IBookingService bookingService, ClaimsHelper claimsHelper)
        {
            _bookingService = bookingService;
            _claimsHelper = claimsHelper;
        }

        public async Task<ActionResult> Execute(CreateBookingRequest request)
        {
            //var isAdmin = _claimsHelper.IsAdmin();
            var errors = new List<ErrorDetailResponse>();

            //request.UserId = isAdmin ? request.UserId : _claimsHelper.GetUserId();

            if (request.UserId == 0)
            {
                errors.Add(new ErrorDetailResponse { Field = nameof(request.UserId), Issue = BookingConstants.UserRequired });
            }
            else if (!await _bookingService.isValidUser(request.UserId))
            {
                errors.Add(new ErrorDetailResponse { Field = nameof(request.UserId), Issue = BookingConstants.InvalidUser });
            }

            var room = await _bookingService.GetRoomById(request.RoomId);
            if (room == null)
                errors.Add(new ErrorDetailResponse { Field = nameof(request.RoomId), Issue = BookingConstants.RoomNotFound });

            if (request.StartTime < DateTime.Now)
                errors.Add(new ErrorDetailResponse { Field = nameof(request.StartTime), Issue = BookingConstants.BookingInPast });

            if (request.StartTime > request.EndTime)
                errors.Add(new ErrorDetailResponse { Field = nameof(request.EndTime), Issue = BookingConstants.EndTimeBeforeStartTime });

            if (room is not null)
            {
                var bookingTime = TimeOnly.FromDateTime(request.StartTime);
                if (bookingTime < room.AvailableFrom || bookingTime > room.AvailableTo)
                    errors.Add(new ErrorDetailResponse { Field = nameof(request.StartTime), Issue = BookingConstants.TimeSlotUnavailable });
            }

            if (await _bookingService.checkRoomSlots(request))
            {
                var conflict = new List<ErrorDetailResponse>
        {
            new ErrorDetailResponse { Field = nameof(request.StartTime), Issue = BookingConstants.RoomAlreadyBooked }
        };
                return ErrorResponseBuilder.Conflict(conflict);
            }

            if (errors.Any())
                return ErrorResponseBuilder.BadRequest(errors);

            await _bookingService.CreateBooking(request);

            return new CreatedResult(string.Empty, ApiResponse<BookingResponse>.Created(BookingConstants.BookingCreated));
        }

    }
}
