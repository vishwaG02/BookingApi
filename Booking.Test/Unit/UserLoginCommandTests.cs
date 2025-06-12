using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.App.Command.User.Repo;
using Booking.BAL.Service.Interface;
using Booking.BAL.ViewModel.UserProfile;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Booking.Test.Unit
{
    public class UserLoginCommandTests
    {
        //private readonly Mock<IUserProfileService> _mockService;
        //private readonly UserLoginCommand _command;
        //public UserLoginCommandTests()
        //{
        //    _mockService = new Mock<IUserProfileService>();
        //    _command = new UserLoginCommand(_mockService.Object);
        //}

        //[Fact]
        //public async Task Execute_NullRequest_ReturnsBadRequest()
        //{
        //    var result = await _command.Execute(null);

        //    var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        //    Assert.Equal("Login Credentials required..!", badRequest.Value);
        //}

        //[Fact]
        //public async Task Execute_InvalidCredentials_ReturnsBadRequest()
        //{
        //    _mockService.Setup(s => s.Login(It.IsAny<LoginRequest>())).ReturnsAsync((string)null);

        //    var request = new LoginRequest { Email = "test@example.com", Password = "testpassword" };
        //    var result = await _command.Execute(request);

        //    var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        //    Assert.Equal("Invalid Credentials..!", badRequest.Value);
        //}

        //[Fact]
        //public async Task Execute_ValidCredentials_ReturnsOk()
        //{
        //    _mockService.Setup(s => s.Login(It.IsAny<LoginRequest>())).ReturnsAsync("valid-jwt");

        //    var request = new LoginRequest { Email = "test@example.com", Password = "Temp@123" };
        //    var result = await _command.Execute(request);

        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal("valid-jwt", okResult.Value);
        //}
    }
}
