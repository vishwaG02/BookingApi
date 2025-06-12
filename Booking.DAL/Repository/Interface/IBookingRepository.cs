using Booking.DAL.Entities;
using Booking.DAL.Pagination;

namespace Booking.DAL.Repository.Interface
{
    public interface IBookingRepository
    {
        Task CreateBooking(Bookings model);

        Task<Room> GetRoomById(Guid id);

        IQueryable<Bookings> GetAllBookings();

        Task<IEnumerable<Room>> GetAllRoomsAsync();

        Task<IEnumerable<Bookings>> GetBookingsByDateAsync(DateTime date);

        Task<Bookings?> GetBookingByIdAsync(Guid id);

        Task UpdateBookingAsync(Bookings booking);

        Task<bool> isValidUser(int id);

        Task<bool> checkRoomSlots(Bookings model);
    }
}
