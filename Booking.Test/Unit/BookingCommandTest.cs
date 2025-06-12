using Booking.App.Command.Booking.Interface;
using Booking.App.Command.Booking.Repo;
using Booking.BAL.Helper;
using Booking.BAL.Service.Interface;
using Booking.BAL.Translator.Interface;
using Booking.BAL.ViewModel.Booking;
using Booking.DAL.Entities;
using Booking.DAL.Enum;
using Booking.DAL.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Test.Unit
{
    public class BookingCommandTest
    {
        private readonly ICreateBookingCommand _createBookingCommand;
        private readonly IGetBookingCommand _getBookingCommand;
        private readonly ICancelBookingCommand _cancelBookingCommand;
        private readonly Mock<IBookingService> _mockService;
        private readonly IGetRoomAvailabilityCommand _getRoomAvailabilityCommand;

        public BookingCommandTest()
        {
            _mockService = new Mock<IBookingService>();
            var mockHttpContext = new Mock<HttpContext>();
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Role, "Admin")
        };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);
            mockHttpContext.Setup(c => c.User).Returns(principal);

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext.Object);
            var bookingTranslator = new Mock<IBookingTranslator>();

            var claimsHelper = new ClaimsHelper(mockHttpContextAccessor.Object);
            _getRoomAvailabilityCommand = new GetRoomAvailabilityCommand(_mockService.Object, bookingTranslator.Object);
            _cancelBookingCommand = new CancelBookingCommand(_mockService.Object, claimsHelper);
            _createBookingCommand = new CreateBookingCommand(_mockService.Object, claimsHelper);
            _getBookingCommand = new GetBookingCommand(_mockService.Object);

        }

        //Create Booking Successfully
        [Fact]
        public async Task Execute_WithValidRequest_ReturnsCreatedResult()
        {
            var roomId = Guid.Parse("B51C14B6-9221-4CEA-9A65-144A81BEA68B");


            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = startTime.AddHours(2);
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 1,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"
            };

            var expectedRoom = new Room
            {
                Id = roomId,
                Name = "Conference Room A",
                Location = "1st Floor",
                Capacity = 10,
                Amenities = "Projector, Whiteboard, Wi-Fi",
                AvailableFrom = TimeOnly.FromDateTime(now.AddHours(-1)),
                AvailableTo = TimeOnly.FromDateTime(now.AddMinutes(65)),
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(true);
            _mockService.Setup(s => s.GetRoomById(It.IsAny<Guid>())).ReturnsAsync(expectedRoom);
            var result = await _createBookingCommand.Execute(request);
            var res = Assert.IsType<CreatedResult>(result);
            Assert.Equal(201, res.StatusCode);

        }

        //UserId required
        [Fact]
        public async Task CreateBooking_WhenUserIdRequired()
        {
            var roomId = Guid.Parse("B51C14B6-9221-4CEA-9A65-144A81BEA68B");


            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = startTime.AddHours(2);
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 0,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"
            };

            var expectedRoom = new Room
            {
                Id = roomId,
                Name = "Conference Room A",
                Location = "1st Floor",
                Capacity = 10,
                Amenities = "Projector, Whiteboard, Wi-Fi",
                AvailableFrom = TimeOnly.FromDateTime(now.AddHours(-1)),
                AvailableTo = TimeOnly.FromDateTime(now.AddMinutes(65)),
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(true);
            _mockService.Setup(s => s.GetRoomById(It.IsAny<Guid>())).ReturnsAsync(expectedRoom);

            var result = await _createBookingCommand.Execute(request);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, res.StatusCode);
        }

        //CreateBooking_Invalid User
        [Fact]
        public async Task CreateBooking_InvalidUser()
        {
            var roomId = Guid.Parse("B51C14B6-9221-4CEA-9A65-144A81BEA68B");


            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = startTime.AddHours(2);
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 1,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"
            };

            var expectedRoom = new Room
            {
                Id = roomId,
                Name = "Conference Room A",
                Location = "1st Floor",
                Capacity = 10,
                Amenities = "Projector, Whiteboard, Wi-Fi",
                AvailableFrom = TimeOnly.FromDateTime(now.AddHours(-1)),
                AvailableTo = TimeOnly.FromDateTime(now.AddMinutes(65)),
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(false);
            _mockService.Setup(s => s.GetRoomById(It.IsAny<Guid>())).ReturnsAsync(expectedRoom);
            var result = await _createBookingCommand.Execute(request);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, res.StatusCode);
        }

        //When room Not found 
        [Fact]
        public async Task CreateBooking_WhenRoomNotFound()
        {
            var roomId = Guid.Parse("B51C14B6-9221-4CEA-9A65-144A81BEA68F");


            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = startTime.AddHours(2);
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 1,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"
            };

            var expectedRoom = new Room
            {
                Id = roomId,
                Name = "Conference Room A",
                Location = "1st Floor",
                Capacity = 10,
                Amenities = "Projector, Whiteboard, Wi-Fi",
                AvailableFrom = TimeOnly.FromDateTime(now.AddHours(-1)),
                AvailableTo = TimeOnly.FromDateTime(now.AddMinutes(65)),
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(true);
            _mockService.Setup(s => s.GetRoomById(It.IsAny<Guid>())).ReturnsAsync((Room)null);
            var result = await _createBookingCommand.Execute(request);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, res.StatusCode);

        }

        //End time should be greater than start time
        [Fact]
        public async Task CreateBooking_CheckStartTimeAndEndTime()
        {
            var roomId = Guid.Parse("B51C14B6-9221-4CEA-9A65-144A81BEA68B");


            var now = DateTime.Now;
            var startTime = now.AddHours(-1);
            var endTime = startTime.AddHours(2);
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 1,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"
            };

            var expectedRoom = new Room
            {
                Id = roomId,
                Name = "Conference Room A",
                Location = "1st Floor",
                Capacity = 10,
                Amenities = "Projector, Whiteboard, Wi-Fi",
                AvailableFrom = TimeOnly.FromDateTime(now.AddHours(-2)),
                AvailableTo = TimeOnly.FromDateTime(now.AddHours(2)),
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(true);
            _mockService.Setup(s => s.GetRoomById(It.IsAny<Guid>())).ReturnsAsync(expectedRoom);
            var result = await _createBookingCommand.Execute(request);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, res.StatusCode);

        }

        //End time should be greater than start time
        [Fact]
        public async Task Execute_WithValidRequest_StartTimeGreaterThenEndTime()
        {
            var roomId = Guid.Parse("B51C14B6-9221-4CEA-9A65-144A81BEA68B");


            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = now;
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 1,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"
            };

            var expectedRoom = new Room
            {
                Id = roomId,
                Name = "Conference Room A",
                Location = "1st Floor",
                Capacity = 10,
                Amenities = "Projector, Whiteboard, Wi-Fi",
                AvailableFrom = TimeOnly.FromDateTime(now.AddHours(-1)),
                AvailableTo = TimeOnly.FromDateTime(now.AddMinutes(65)),
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(true);
            _mockService.Setup(s => s.GetRoomById(It.IsAny<Guid>())).ReturnsAsync(expectedRoom);
            var result = await _createBookingCommand.Execute(request);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, res.StatusCode);

        }
        //There is no room available for your time slot bookingTime < room.AvailableTo 
        [Fact]
        public async Task CreateBooking_CheckingBookingTimeLessThenRoomAvailableFrom()
        {
            var roomId = Guid.Parse("B51C14B6-9221-4CEA-9A65-144A81BEA68B");


            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = startTime.AddHours(2);
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 1,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"
            };

            var expectedRoom = new Room
            {
                Id = roomId,
                Name = "Conference Room A",
                Location = "1st Floor",
                Capacity = 10,
                Amenities = "Projector, Whiteboard, Wi-Fi",
                AvailableFrom = TimeOnly.FromDateTime(now.AddHours(2)),
                AvailableTo = TimeOnly.FromDateTime(now.AddHours(2)),
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(true);
            _mockService.Setup(s => s.GetRoomById(It.IsAny<Guid>())).ReturnsAsync(expectedRoom);
            var result = await _createBookingCommand.Execute(request);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, res.StatusCode);
        }

        //There is no room available for your time slot bookingTime > room.AvailableTo 
        [Fact]
        public async Task CreateBooking_CheckingBookingTimeGreaterThenRoomAvailableFrom()
        {
            var roomId = Guid.Parse("B51C14B6-9221-4CEA-9A65-144A81BEA68B");


            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = startTime.AddHours(2);
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 1,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"
            };

            var expectedRoom = new Room
            {
                Id = roomId,
                Name = "Conference Room A",
                Location = "1st Floor",
                Capacity = 10,
                Amenities = "Projector, Whiteboard, Wi-Fi",
                AvailableFrom = TimeOnly.FromDateTime(now),
                AvailableTo = TimeOnly.FromDateTime(now),
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(true);
            _mockService.Setup(s => s.GetRoomById(It.IsAny<Guid>())).ReturnsAsync(expectedRoom);
            var result = await _createBookingCommand.Execute(request);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, res.StatusCode);
        }
        //createbooking_ConflictBooking
        [Fact]
        public async Task createbooking_ConflictBooking()
        {
            var roomId = Guid.Parse("B51C14B6-9221-4CEA-9A65-144A81BEA68B");


            var now = DateTime.Now;
            var startTime = now.AddHours(1);
            var endTime = startTime.AddHours(2);
            var request = new CreateBookingRequest
            {
                RoomId = roomId,
                UserId = 1,
                StartTime = startTime,
                EndTime = endTime,
                Title = "Valid Booking",
                Description = "Should pass all validation checks"
            };

            var expectedRoom = new Room
            {
                Id = roomId,
                Name = "Conference Room A",
                Location = "1st Floor",
                Capacity = 10,
                Amenities = "Projector, Whiteboard, Wi-Fi",
                AvailableFrom = TimeOnly.FromDateTime(now.AddHours(-1)),
                AvailableTo = TimeOnly.FromDateTime(now.AddMinutes(65)),
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(true);
            _mockService.Setup(s => s.GetRoomById(It.IsAny<Guid>())).ReturnsAsync(expectedRoom);
            _mockService.Setup(s => s.checkRoomSlots(request)).ReturnsAsync(true);
            var result = await _createBookingCommand.Execute(request);
            var res = Assert.IsType<ObjectResult>(result);
            Assert.Equal(409, res.StatusCode);
        }

        //GetBooking
        [Fact]
        public async Task GetBooking_CreatedResultSuccess()
        {
            var getBookingRequest = new BookingQueryParameters()
            {
                fromDate = DateTime.Now,
                toDate = DateTime.Now.AddHours(1),
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(true);
            var result = await _getBookingCommand.Execute(1,getBookingRequest);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }

        //GetBooking_InvalidUser
        [Fact]
        public async Task GetBooking_InvalidUser()
        {
            var getBookingRequest = new BookingQueryParameters()
            {
                fromDate = DateTime.Now,
                toDate = DateTime.Now.AddHours(1),
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(false);
            var result = await _getBookingCommand.Execute(1,getBookingRequest);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, res.StatusCode);
        }

        //GetBooking_FromDateGreaterThenToDate
        [Fact]
        public async Task GetBooking_FromDateGreaterThenToDate()
        {
            var getBookingRequest = new BookingQueryParameters()
            {
                fromDate = DateTime.Now.AddHours(1),
                toDate = DateTime.Now,
            };
            _mockService.Setup(s => s.isValidUser(It.IsAny<int>())).ReturnsAsync(true);
            var result = await _getBookingCommand.Execute(1,getBookingRequest);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, res.StatusCode);
        }
        //CancelBooking_ReturnSuccess 
        [Fact]
        public async Task CancelBooking_ReturnNoContent()
        {
            var bookingId = Guid.NewGuid();
            var Expectedbookings = new Bookings()
            {
                Id = Guid.Parse("a2f5a1b7-2c4b-4b7e-a1b0-1234567890ab"),
                UserId = 1,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Title = "Cancel booking",
                Description = "Weekly sync-up with the product and design teams.",
                Status = BookingStatus.Confirmed
            };

            _mockService.Setup(s => s.GetBookingByIdAsync(It.IsAny<Guid>())).ReturnsAsync(Expectedbookings);
            var result = await _cancelBookingCommand.Execute(bookingId);
            var res = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, res.StatusCode);
        }


        //CancelBooking_BookingNull
        [Fact]
        public async Task CancelBooking_BookingNull()
        {
            var bookingId = Guid.NewGuid();
            _mockService.Setup(s => s.GetBookingByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Bookings?)null);
            var result = await _cancelBookingCommand.Execute(bookingId);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, res.StatusCode);

        }

        //CancelBooking_booking.EndTime < DateTime.Now
        [Fact]
        public async Task CancelBooking_bookingEndTimeLessThenDateTimeNow()
        {
            var bookingId = Guid.NewGuid();
            var Expectedbookings = new Bookings()
            {
                Id = Guid.Parse("a2f5a1b7-2c4b-4b7e-a1b0-1234567890ab"),
                UserId = 1,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(-1),
                Title = "Cancel booking",
                Description = "Weekly sync-up with the product and design teams.",
                Status = BookingStatus.Confirmed

            };
            _mockService.Setup(s => s.GetBookingByIdAsync(It.IsAny<Guid>())).ReturnsAsync(Expectedbookings);
            var result = await _cancelBookingCommand.Execute(bookingId);
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, res.StatusCode);

        }

        //GetRoomAvailable_ReturnSuccess
        [Fact]
        public async Task GetRoomAvailable_ReturnSuccess()
        {
            var date = DateTime.Today;

            var rooms = new List<Room>()
            {
             new Room
            {
                Id = Guid.NewGuid(),
                Name = "Conference Room A",
                Location = "2nd Floor, Building B",
                Capacity = 10,
                Amenities = "Projector, Whiteboard, Wi-Fi",
                AvailableFrom = TimeOnly.Parse("09:00:00"),
                AvailableTo = TimeOnly.Parse("18:00:00")
    }
            };


            var availability = new List<RoomAvailability>
                          {
                              new RoomAvailability
                              {
                                  RoomId = Guid.NewGuid(),
                                  Name = "Conference Room",
                                  Location = "Floor 1",
                                  Capacity = 10,
                                  Amenities = "Projector, Whiteboard",
                                  AvailableBlocks = new List<TimeBlock>
                                  {
                                      new TimeBlock { Start = "09:00", End = "10:00" },
                                      new TimeBlock { Start = "13:00", End = "14:00" }
                                  }
                              }
                          };

            _mockService.Setup(s => s.GetRoomAvailabilityAsync(date, rooms))
                .ReturnsAsync(availability);

            _mockService.Setup(s => s.GetAllRoomsAsync()).ReturnsAsync(rooms);
            var result = await _getRoomAvailabilityCommand.Execute(date);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }

        //RoomNotAvailable
        [Fact]
        public async Task GetRoomAvailable_RoomNotAvailable()
        {
            var date = DateTime.Today;

            var rooms = new List<Room>()
            {
             new Room
            {
                Id = Guid.NewGuid(),
                Name = "Conference Room A",
                Location = "2nd Floor, Building B",
                Capacity = 10,
                Amenities = "Projector, Whiteboard, Wi-Fi",
                AvailableFrom = TimeOnly.Parse("09:00:00"),
                AvailableTo = TimeOnly.Parse("18:00:00")
    }
            };


            var availability = new List<RoomAvailability>
                          {
                              new RoomAvailability
                              {
                                  RoomId = Guid.NewGuid(),
                                  Name = "Conference Room",
                                  Location = "Floor 1",
                                  Capacity = 10,
                                  Amenities = "Projector, Whiteboard",
                                  AvailableBlocks = new List<TimeBlock>
                                  {
                                      new TimeBlock { Start = "09:00", End = "10:00" },
                                      new TimeBlock { Start = "13:00", End = "14:00" }
                                  }
                              }
                          };

            _mockService.Setup(s => s.GetRoomAvailabilityAsync(date, rooms))
                .ReturnsAsync(availability);
            var result = await _getRoomAvailabilityCommand.Execute(date);
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, res.StatusCode);
        }
    }
}
