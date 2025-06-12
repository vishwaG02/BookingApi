using Booking.BAL.ViewModel.Booking;
using Booking.DAL.Pagination;
using Booking.Test.Integration.TestFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Booking.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Booking.Test.Integration.TestData;

namespace Booking.Test.Integration
{
    public class BookingControllerIntegrationTests : IClassFixture<TestApplicationFactory>
    {

        private readonly HttpClient _client;
        private readonly TestApplicationFactory _factory;
        public BookingControllerIntegrationTests(TestApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task AddBookings_ReturnsOk()
        {
            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = startTime.AddHours(2);
            var booking = new CreateBookingRequest
            {
                RoomId = Guid.Parse("b2d6b2c8-3d5c-4c8f-b2c1-abcdefabcdef"),
                UserId = 4,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/bookings")
            {
                Content = JsonContent.Create(booking)
            };
            request.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        // when room not found
        [Fact]
        public async Task CreateBooking_RoomNotFoundservice_ReturnsBadRequest()
        {
            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = startTime.AddHours(2);
            var roomId = Guid.Parse("B51C14B6-9221-4CEA-9A65-144A81BEA68B");
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 4,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Test Booking",
                Description = "Unit test booking"
            };

            var requestApi = new HttpRequestMessage(HttpMethod.Post, "/api/bookings")
            {
                Content = JsonContent.Create(request)
            };
            requestApi.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(requestApi);
            Assert.True(
               response.StatusCode == System.Net.HttpStatusCode.BadRequest
           );

        }
     
        //Invalid User
        [Fact]
        public async Task Execute_WithValidRequest_InvalidUser()
        {
            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = startTime.AddHours(2);
            var roomId = Guid.Parse("b2d6b2c8-3d5c-4c8f-b2c1-abcdefabcdef");
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 3,
                StartTime = startTime,      
                EndTime = endTime,
                Title = "Test Booking",
                Description = "Unit test booking"
            };

            var requestApi = new HttpRequestMessage(HttpMethod.Post, "/api/bookings")
            {
                Content = JsonContent.Create(request)
            };
            requestApi.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(requestApi);
            Assert.False(response.IsSuccessStatusCode);
        }

        //when startTime is less current datetime
        [Fact]
        public async Task Execute_WithValidRequest_StartTimeLessThenCurrentDateTime()
        {
            var today = DateTime.Today;
            var startTime = DateTime.Now.AddMinutes(-10);
            var endTime = today.AddHours(2);
            var booking = new CreateBookingRequest
            {

                RoomId = Guid.Parse("b2d6b2c8-3d5c-4c8f-b2c1-abcdefabcdef"),
                UserId = 4,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"

            };
            var requestApi = new HttpRequestMessage(HttpMethod.Post, "/api/bookings")
            {
                Content = JsonContent.Create(booking)
            };
            requestApi.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(requestApi);
            Assert.True(
               response.StatusCode == System.Net.HttpStatusCode.BadRequest
           );
        }

        //End time should be greater than start time
        [Fact]
        public async Task CreateBooking_CheckStartTimeAndEndTime()
        {

            var request = new CreateBookingRequest
            {
                RoomId = Guid.Parse("b2d6b2c8-3d5c-4c8f-b2c1-abcdefabcdef"),
                UserId = 4,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(-1),
                Title = "Test Booking",
                Description = "Unit test booking"
            };

            var requestApi = new HttpRequestMessage(HttpMethod.Post, "/api/bookings")
            {
                Content = JsonContent.Create(request)
            };
            requestApi.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(requestApi);
            Assert.True(
               response.StatusCode == System.Net.HttpStatusCode.BadRequest


           );
        }

        //UserIsRequired
        [Fact]
        public async Task CreateBooking_UserIsRequired()
        {
            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = startTime.AddHours(2);
            var booking = new CreateBookingRequest
            {
                RoomId = Guid.Parse("b2d6b2c8-3d5c-4c8f-b2c1-abcdefabcdef"),
                UserId = 0,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/bookings")
            {
                Content = JsonContent.Create(booking)
            };
            request.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(request);

            Assert.False(response.IsSuccessStatusCode);
        }

        //There is no room available for your time slot bookingTime < room.AvailableTo 
        [Fact]
        public async Task CreateBooking_CheckingBookingTimeLessThenRoomAvailableTo()
        {
            var roomId = Guid.Parse("c3f7e3a4-9c5d-4d9b-9c31-fedcba987654");
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 4,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddMinutes(95),

                Title = "Test Booking",
                Description = "Unit test booking"
            };

            var requestApi = new HttpRequestMessage(HttpMethod.Post, "/api/bookings")
            {
                Content = JsonContent.Create(request)
            };
            requestApi.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(requestApi);
            Assert.True(
               response.StatusCode == System.Net.HttpStatusCode.BadRequest


           );
        }

        // There is no room available for your time slot bookingTime > room.AvailableFrom
        [Fact]
        public async Task CreateBooking_CheckingBookingTimeGreaterThenRoomAvailableFrom()
        {
            var roomId = Guid.Parse("c3f7e3a4-9c5d-4d9b-9c31-fedcba987654");
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 4,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddMinutes(95),
                Title = "Test Booking",
                Description = "Unit test booking"
            };



            var requestApi = new HttpRequestMessage(HttpMethod.Post, "/api/bookings")
            {
                Content = JsonContent.Create(request)
            };
            requestApi.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(requestApi);
            Assert.True(
               response.StatusCode == System.Net.HttpStatusCode.BadRequest


           );
        }

        //CreateBooking Conflict
        [Fact]
        public async Task CreateBooking_ConflictResult()
        {
            var roomId = Guid.Parse("d8a5f740-0e9f-4a87-9b1d-50b7dbfc98ad");
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 4,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2),
                Title = "Test Booking",
                Description = "Unit test booking"
            };
            var requestApi = new HttpRequestMessage(HttpMethod.Post, "/api/bookings")
            {
                Content = JsonContent.Create(request)
            };
            requestApi.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(requestApi);
            Assert.True(
               response.StatusCode == System.Net.HttpStatusCode.Conflict);
        }

        //Get booking for valid user
        [Fact]
        public async Task GetBooking_ValidUser()
        {
            var userId = 4;
            var fromDate = DateTime.Today.AddDays(-7);
            var toDate = DateTime.Today;

            var query = $"?PageNumber=1" +
                 $"&PageSize=10" +
                 $"&SortBy=StartTime" +
                 $"&Descending=false" +
                 $"&FromDate={fromDate}" +
                 $"&ToDate={toDate}" +
                 $"&IsActive=true";

            var requestApi = new HttpRequestMessage(HttpMethod.Get, $"/api/users/{userId}/bookings{query}")
            {
                Content = JsonContent.Create(query)
            };

            requestApi.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(requestApi);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //GetBookings for inValid user
        [Fact]
        public async Task GetBooking_InValidUser()
        {
            var userId = 1;
            var fromDate = DateTime.Today.AddDays(-7);
            var toDate = DateTime.Today;
            var query = $"?PageNumber=1" +
                $"&PageSize=10" +
                $"&SortBy=StartTime" +
                $"&Descending=false" +
                $"&FromDate={fromDate}" +
                $"&ToDate={toDate}" +
                $"&IsActive=true";

            var requestApi = new HttpRequestMessage(HttpMethod.Get, $"/api/users/{userId}/bookings{query}")
            {
                Content = JsonContent.Create(query)
            };

            requestApi.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(requestApi);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //CancelBooking_ReturnOk
        [Fact]
        public async Task CancelBooking_ReturnNoContent()
        {
            var bookingId = Guid.Parse("a1f5a1b7-2c4b-4b7e-a1b0-1234567890ae");

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/bookings/{bookingId}");

            request.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        //CancelBooking_ReturnNoBooking
        [Fact]
        public async Task CancelBooking_ReturnNoBooking()
        {
            var bookingId = Guid.Parse("a1f5a1b7-2c4b-4b7e-a1b0-1234567890ac");

            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/bookings/{bookingId}");

            request.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");

            var response = await _client.SendAsync(request);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        //Get rooms when no availability
        [Fact]
        public async Task GetRooms_ReturnsBadRequest_WhenNoAvailability()
        {
            var date = DateTime.Today;

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/availability?date={date:yyyy-MM-dd}");


            request.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");
            var response = await _client.SendAsync(request);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        //Room Available in this date 
        [Fact]
        public async Task Execute_WhenRoomAvailabilityExists_ReturnsOk()
        {

            var date = DateTime.Today;

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/availability?date={date:yyyy-MM-dd}");
            request.Headers.Add("X-Api-Key", "2RfmSxS68esNhTO8gmPvmKOFOYXfPq14Zq5VMVEdIiUqX2g2FFGARb6z32Z4IAAD");

            var response = await _client.SendAsync(request);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}
