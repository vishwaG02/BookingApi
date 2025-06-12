using Booking.App.Command.User.Interface;
using Booking.BAL.Constant;
using Booking.BAL.Helper;
using Booking.BAL.Service.Interface;
using Booking.BAL.ViewModel.UserProfile;
using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Command.User.Repo
{
    public class UserLoginCommand : IUserLoginCommand
    {
        private readonly IUserProfileService _userProfileService;
        public UserLoginCommand(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        public async Task<ActionResult> Execute(LoginRequest request)
        {
            var errors = new List<ErrorDetailResponse>();
            if (request == null)
            {
                errors.Add(new ErrorDetailResponse { Field = "Email Or Password is required", Issue = UserConstants.LoginCredentialsRequired });
            }

            var result = await _userProfileService.Login(request);
            if (result == null)
            {
                errors.Add(new ErrorDetailResponse { Field = "Email Or Password is invalid", Issue = UserConstants.InvalidCredentials });
            }

            if (errors.Any())
                return ErrorResponseBuilder.BadRequest(errors);

            return new OkObjectResult(result);
        }
    }
}
