using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Booking.BAL.ViewModel.UserProfile;
using Booking.DAL.Context;
using Booking.Test.Integration.TestFactory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace Booking.Test.Integration
{
    public class UserIntegrationTests : IClassFixture<TestApplicationFactory>
    {
        //private readonly HttpClient _client;
        //private readonly TestApplicationFactory _factory;
        //public UserIntegrationTests(TestApplicationFactory factory)
        //{
        //    _client = factory.CreateClient();
        //    _factory = factory;
        //}

        //[Fact]
        //public async Task AddUser_ReturnsOk()
        //{
        //    var token = await GetJwtTokenAsync("admin@test.com", "Temp@123");
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //    var user = new UserProfileViewModel
        //    {
        //        FirstName = "ravi",
        //        LastName = "Test",
        //        Email = "ravi@test.com",
        //        Password = "Temp@123",
        //        SecretKey = Guid.NewGuid()
        //    };

        //    var response = await _client.PostAsJsonAsync("/api/User/AddUser", user);
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //}

        //[Fact]
        //public async Task Login_ReturnsOk()
        //{

        //    var request = new LoginRequest
        //    {
        //        Email = "admin@test.com",
        //        Password = "Temp@123"
        //    };

        //    var response = await _client.PostAsJsonAsync("/api/User/Login", request);
                        
        //    Assert.True(
        //        response.IsSuccessStatusCode
        //    );
        //}
        //[Fact]
        //public async Task Login_ReturnsBadRequest()
        //{          

        //    var request = new LoginRequest
        //    {
        //        Email = "admin@test.com",
        //        Password = "Temp@12345"
        //    };

        //    var response = await _client.PostAsJsonAsync("/api/User/Login", request);

        //    Assert.True(
        //        response.StatusCode == System.Net.HttpStatusCode.BadRequest
        //    );
        //}

        //private async Task<string> GetJwtTokenAsync(string email, string password)
        //{
        //    var loginRequest = new LoginRequest
        //    {
        //        Email = email,
        //        Password = password
        //    };

        //    var response = await _client.PostAsJsonAsync("/api/User/Login", loginRequest);
        //    response.EnsureSuccessStatusCode();

        //    var result = await response.Content.ReadAsStringAsync(); 
        //    return result ?? throw new Exception("Token not returned.");
        //}
    }
}
