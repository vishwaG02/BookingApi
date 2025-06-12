using Booking.App.Common;
using Booking.BAL.ViewModel.Booking;
using Booking.DAL.Entities;
using Booking.DAL.Pagination;
namespace Booking.BAL.Service.Interface
{
    public interface IBookingService
    {
        Task CreateBooking(CreateBookingRequest request);

        Task<PagedBookingResponse> GetBooking(int userid, BookingQueryParameters parameters);

        Task<IEnumerable<Room>> GetAllRoomsAsync();

        Task<IEnumerable<RoomAvailability>> GetRoomAvailabilityAsync(DateTime date,IEnumerable<Room> rooms);

        Task CancelBookingAsync(Bookings booking);

        Task<Room> GetRoomById(Guid id);

        Task<Bookings?> GetBookingByIdAsync(Guid id);

        Task<bool> isValidUser(int id);

        Task<bool> checkRoomSlots(CreateBookingRequest model);
    }
}
