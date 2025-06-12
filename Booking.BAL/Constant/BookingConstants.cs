using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BAL.Constant
{
    public class BookingConstants
    {
        public const string RoomNotFound = "There is no such room found.";
        public const string RoomNotAvailable = "No available room found";
        public const string BookingInPast = "Bookings only available for future dates";
        public const string EndTimeBeforeStartTime = "End date should be greater than start date";
        public const string TimeSlotUnavailable = "There is no room available for your time slot.";
        public const string InvalidUser = "Invalid User";
        public const string RoomAlreadyBooked = "Room is already booked for this interval.";
        public const string BookingCreated = "Booking Created Successfully";
        public const string BookingNotFound = "Booking not found";
        public const string NoPermissionToCancel = "You do not have permission to cancel this Booking.";
        public const string BookingAlreadyCancelled = "Booking already cancelled";
        public const string BookingCancelledSuccess = "Booking cancelled successfully";
        public const string BookingFetchSuccess = "Success";
        public const string NoBookingsFound = "No bookings";
        public const string UserRequired = "UserId required";
        public const string PastBookingCancel = "Cannot able to cancel the past bookings";
    }
}
