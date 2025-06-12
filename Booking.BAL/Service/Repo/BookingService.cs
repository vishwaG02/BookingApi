using Booking.App.Common;
using Booking.BAL.Constant;
using Booking.BAL.Service.Interface;
using Booking.BAL.Translator.Interface;
using Booking.BAL.ViewModel.Booking;
using Booking.DAL.Entities;
using Booking.DAL.Enum;
using Booking.DAL.Pagination;
using Booking.DAL.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Booking.BAL.Service.Repo
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingTranslator _bookingTranslator;

        public BookingService(IBookingRepository repository, IBookingTranslator bookingTranslator)
        {
            _bookingRepository = repository;
            _bookingTranslator = bookingTranslator;
        }

        public async Task CreateBooking(CreateBookingRequest request)
        {
            var booking = _bookingTranslator.Translate(request);
            await _bookingRepository.CreateBooking(booking);
        }

        public async Task<PagedBookingResponse> GetBooking(int userid, BookingQueryParameters parameters)
        {
            var query = _bookingRepository.GetAllBookings();

            query = query.Where(b => b.UserId == userid);

            if (parameters.fromDate.HasValue && parameters.toDate.HasValue)
            {
                var from = parameters.fromDate.Value.Date;
                var to = parameters.toDate.Value.Date.AddDays(1);
                query = query.Where(b => b.StartTime >= from && b.StartTime < to);
            }

            query = query.Where(b => b.Status == parameters.status);

            var entityType = typeof(Bookings);
            if (!string.IsNullOrEmpty(parameters.sortBy) &&
                entityType.GetProperty(parameters.sortBy, BindingFlags.Public | BindingFlags.Instance) != null)
            {
                query = parameters.descending
                    ? query.OrderByDescending(e => EF.Property<object>(e, parameters.sortBy))
                    : query.OrderBy(e => EF.Property<object>(e, parameters.sortBy));
            }

            var totalRecords = await query.CountAsync();
            var skip = (parameters.pageNumber - 1) * parameters.pageSize;
            var bookings = await query.Skip(skip).Take(parameters.pageSize).ToListAsync();

            if (bookings.Any())
            {
                var bookingVMs = _bookingTranslator.TranslateBookingList(bookings);
                return new PagedBookingResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = BookingConstants.BookingFetchSuccess,
                    Data = bookingVMs,
                    TotalRecords = totalRecords,
                    PageNumber = parameters.pageNumber,
                    PageSize = parameters.pageSize
                };
            }

            return new PagedBookingResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = BookingConstants.NoBookingsFound
            };
        }


        public async Task<IEnumerable<RoomAvailability>> GetRoomAvailabilityAsync(DateTime date,IEnumerable<Room> rooms)
        {

            var bookings = await _bookingRepository.GetBookingsByDateAsync(date);

            var result = rooms.Select(room =>
            {
                var dayStart = date.Date + room.AvailableFrom.ToTimeSpan();
                var dayEnd = date.Date + room.AvailableTo.ToTimeSpan();

                var roomBookings = bookings
                    .Where(b => b.RoomId == room.Id)
                    .OrderBy(b => b.StartTime)
                    .ToList();

                var blocks = new List<TimeBlock>();
                var current = dayStart;

                foreach (var b in roomBookings)
                {
                    if (current < b.StartTime)
                    {
                        blocks.Add(new TimeBlock
                        {
                            Start = current.ToString("HH:mm"),
                            End = b.StartTime.ToString("HH:mm")
                        });
                    }

                    current = b.EndTime > current ? b.EndTime : current;
                }

                if (current < dayEnd)
                {
                    blocks.Add(new TimeBlock
                    {
                        Start = current.ToString("HH:mm"),
                        End = dayEnd.ToString("HH:mm")
                    });
                }

                var validBlocks = blocks
                    .Where(b => b.Start != b.End)
                    .ToList();

                var finalBlocks = validBlocks.Any() ? validBlocks : null;

                return _bookingTranslator.TranslateRoomList(room, finalBlocks);
            }).ToList();

            return result;
        }

        public async Task CancelBookingAsync(Bookings booking)
        {
            booking.Status = BookingStatus.Cancelled;
            booking.IsActive = false;
            booking.ModifiedDate = DateTime.UtcNow;
            await _bookingRepository.UpdateBookingAsync(booking);
        }

        public Task<Room> GetRoomById(Guid id)
        {
            return _bookingRepository.GetRoomById(id);
        }

        public async Task<Bookings?> GetBookingByIdAsync(Guid id)
        {
            return await _bookingRepository.GetBookingByIdAsync(id);
        }

        public Task<bool> isValidUser(int id)
        {
            return _bookingRepository.isValidUser(id);
        }

        public Task<bool> checkRoomSlots(CreateBookingRequest model)
        {
            return _bookingRepository.checkRoomSlots(_bookingTranslator.Translate(model));
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _bookingRepository.GetAllRoomsAsync();
        }
    }
}
