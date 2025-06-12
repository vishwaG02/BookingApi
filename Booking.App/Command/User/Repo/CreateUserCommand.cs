using Booking.App.Command.User.Interface;
using Booking.App.Common;
using Booking.BAL.Constant;
using Booking.BAL.Helper;
using Booking.BAL.Service.Interface;
using Booking.BAL.ViewModel.UserProfile;
using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Command.User.Repo
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IUserProfileService _userProfileService;
        public CreateUserCommand(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        public async Task<ActionResult> Execute(UserProfileViewModel model)
        {
            var errors = new List<ErrorDetailResponse>();
            if (model == null)
            {
                errors.Add(new ErrorDetailResponse { Field = "User Details is required", Issue = UserConstants.UserDetailsRequired });
            }

            if (errors.Any())
                return ErrorResponseBuilder.BadRequest(errors);

            await _userProfileService.AddUser(model);

            return new CreatedResult(UserConstants.UserCreated,ApiResponse<string>.Created(UserConstants.UserCreated));
        }
    }
}
