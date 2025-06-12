using Booking.App.Command.User.Interface;
using Booking.App.Common;
using Booking.BAL.Constant;
using Booking.BAL.Helper;
using Booking.BAL.ViewModel.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //[Authorize(Policy = PermissionsConstants.CreateUser)]        
        //[HttpPost("AddUser")]
        //[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult> AddUser(
        //    [FromServices] ICreateUserCommand command,
        //    [FromBody] UserProfileViewModel model)
        //{
        //    return await command.Execute(model);
        //}

        ////[AllowAnonymous]
        //[HttpPost("Login")]
        //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult> Login(
        //    [FromServices] IUserLoginCommand command,
        //    [FromBody] LoginRequest request)
        //{
        //    return await command.Execute(request);
        //}
    }
}
