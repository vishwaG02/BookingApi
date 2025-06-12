using Booking.BAL.Translator.Interface;
using Booking.BAL.ViewModel.Booking;
using Booking.DAL.Entities;
using Booking.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Booking.BAL.Translator.Repo
{
    public class BookingTranslator : IBookingTranslator
    {
        public Bookings Translate(CreateBookingRequest request)
        {
            return new Bookings
            {
                RoomId = request.RoomId,
                UserId = request.UserId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Title = request.Title,
                Description = request.Description,
                IsActive = true
            };
        }

        public IEnumerable<BookingResponse> TranslateBookingList(IEnumerable<Bookings> bookings)
        {
            return bookings.Select(Translate).ToList();
        }

        public BookingResponse Translate(Bookings booking)
        {
            return new BookingResponse
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                UserId = booking.UserId,
                StartTime = booking.StartTime.ToString(),
                EndTime = booking.EndTime.ToString(),
                Title = booking.Title,
                Description = booking.Description,
                Status = booking.Status.ToString(),
            };
        }

        public BookingResponse ToBookingViewModel(Bookings model)
        {
            return new BookingResponse
            {
                Id = model.Id,
                RoomId = model.RoomId,
                StartTime = model.StartTime.ToString(),
                EndTime = model.EndTime.ToString(),
                Status = model.Status.ToString(),
                UserId = model.UserId,
                Title = model.Title,
                Description = model.Description,
            };
        }

        public RoomAvailability TranslateRoomList(Room room, List<TimeBlock> blocks)
        {
            return new RoomAvailability
            {
                RoomId = room.Id,
                Name = room.Name,
                Location = room.Location,
                Capacity = room.Capacity,
                Amenities = room.Amenities,
                AvailableBlocks = blocks
            };
        }
    }
}
