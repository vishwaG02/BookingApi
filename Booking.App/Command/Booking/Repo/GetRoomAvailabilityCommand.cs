using Booking.App.Command.Booking.Interface;
using Booking.App.Common;
using Booking.BAL.Constant;
using Booking.BAL.Service.Interface;
using Booking.BAL.Translator.Interface;
using Booking.BAL.ViewModel.Booking;
using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Command.Booking.Repo
{
    public class GetRoomAvailabilityCommand : IGetRoomAvailabilityCommand
    {
        private readonly IBookingService _bookingService;
        private readonly IBookingTranslator _bookingTranslator;

        public GetRoomAvailabilityCommand(IBookingService bookingService, IBookingTranslator bookingTranslator)
        {
            _bookingService = bookingService;
            _bookingTranslator = bookingTranslator;
        }

        public async Task<ActionResult> Execute(DateTime date)
        {
            var rooms = await _bookingService.GetAllRoomsAsync();
            if (rooms.Count() == 0)
                return new OkObjectResult(ApiResponse<IEnumerable<RoomAvailability>>.Success(BookingConstants.RoomNotAvailable));

            var result = await _bookingService.GetRoomAvailabilityAsync(date, rooms);
            
            return new OkObjectResult(ApiResponse<IEnumerable<RoomAvailability>>.Success(BookingConstants.BookingFetchSuccess, result));
        }
    }
}
