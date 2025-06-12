using Azure;
using Booking.App.Command.Booking.Interface;
using Booking.App.Common;
using Booking.BAL.Constant;
using Booking.BAL.Helper;
using Booking.BAL.ViewModel.Booking;
using Booking.DAL.Entities;
using Booking.DAL.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Booking.App.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        /// <summary>
        /// Creates a new booking with the specified details.
        /// </summary>
        /// <param name="command">The service responsible for booking creation logic.</param>
        /// <param name="request">The booking request containing room, user, time, and other details.</param>
        /// <returns>A success message if the booking is created, or an error response if creation fails.</returns>
        [HttpPost]
        [Route("bookings")]
        //[Authorize(Policy = PermissionsConstants.CreateBooking)]
        [Produces("application/json")]
        [SwaggerRequestExample(typeof(CreateBookingRequest), typeof(CreateBookingRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreateBookingResponseExample))]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> AddBooking([FromServices] ICreateBookingCommand command, [FromBody]CreateBookingRequest request)
        {
            return await command.Execute(request);
            }

        /// <summary>
        /// Retrieves a paginated list of bookings for a specific user.
        /// </summary>
        /// <param name="command">The booking query command service.</param>
        /// <param name="userId">The ID of the user whose bookings are being fetched.</param>
        /// <param name="parameters">The filtering and pagination parameters for the booking query.</param>
        /// <returns>A paginated list of bookings or an error response.</returns>
        [HttpGet]
        [Route("users/{userId}/bookings")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedBookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetBooking([FromServices] IGetBookingCommand command, int userId,  [FromQuery] BookingQueryParameters parameters)
        {
            return await command.Execute(userId, parameters);
        }

        /// <summary>
        /// Retrieves the availability of all rooms for the specified date.
        /// </summary>
        /// <param name="command">The service that handles room availability queries.</param>
        /// <param name="date">The date for which room availability is requested (required).</param>
        /// <returns>A list of available rooms and their time blocks for the specified date.</returns>
        [HttpGet]
        [Route("availability")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApiResponse<List<RoomAvailability>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetRooms([FromServices] IGetRoomAvailabilityCommand command, [FromQuery][Required] DateTime date)
        {
            return await command.Execute(date);
        }

        /// <summary>
        /// Cancels an existing booking based on the provided booking ID.
        /// </summary>
        /// <param name="command">The service that handles booking cancellation.</param>
        /// <param name="bookingId">The unique identifier of the booking to cancel.</param>
        /// <returns>A success message or an error response.</returns>
        [HttpDelete]
        [Route("bookings/{bookingId}")]
        //[Authorize(Policy = PermissionsConstants.CancelBooking)]
        [Produces("application/json")]
        //[SwaggerResponseExample(StatusCodes.Status200OK, typeof(CancelBookingResponseExample))]
        //[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CancelBooking([FromServices] ICancelBookingCommand command, Guid bookingId)
        {
            return await command.Execute(bookingId);
        }
    }
}
