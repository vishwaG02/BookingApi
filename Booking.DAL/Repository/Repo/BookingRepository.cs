using Booking.DAL.Context;
using Booking.DAL.Entities;
using Booking.DAL.Enum;
using Booking.DAL.Pagination;
using Booking.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace Booking.DAL.Repository.Repo
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateBooking(Bookings model)
        {
            await _context.Bookings.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> GetRoomById(Guid id)
        {
            return await _context.Rooms.SingleOrDefaultAsync(r => r.Id == id);
        }

        public IQueryable<Bookings> GetAllBookings()
        {
            return _context.Bookings.IgnoreQueryFilters().AsQueryable();
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<IEnumerable<Bookings>> GetBookingsByDateAsync(DateTime date)
        {
            return await _context.Bookings
                .Where(b => b.StartTime.Date == date.Date && b.Status == BookingStatus.Confirmed)
                .ToListAsync();
        }

        public async Task<Bookings?> GetBookingByIdAsync(Guid id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateBookingAsync(Bookings booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> isValidUser(int id)
        {
            var isValidUser = await _context.UserProfiles.SingleOrDefaultAsync(u => u.UserId == id);
            if (isValidUser == null)
                return false;

            return true;
        }

        public async Task<bool> checkRoomSlots(Bookings model)
        {
            var isBooked = await _context.Bookings.SingleOrDefaultAsync(a =>
               a.RoomId == model.RoomId &&
               a.Status == BookingStatus.Confirmed &&
               a.StartTime < model.EndTime &&
               a.EndTime > model.StartTime);

            if (isBooked != null)
                return true;

            return false;
        }
    }
}
