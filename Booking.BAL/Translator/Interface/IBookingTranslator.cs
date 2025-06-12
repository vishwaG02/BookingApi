using Booking.BAL.ViewModel.Booking;
using Booking.DAL.Entities;

namespace Booking.BAL.Translator.Interface
{
    public interface IBookingTranslator
    {
        Bookings Translate(CreateBookingRequest request);

        BookingResponse ToBookingViewModel(Bookings model);

        IEnumerable<BookingResponse> TranslateBookingList(IEnumerable<Bookings> bookings);

        RoomAvailability TranslateRoomList(Room rooms, List<TimeBlock> blocks);
    }
}
