using Booking.BAL.ViewModel.UserProfile;
using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Command.User.Interface
{
    public interface ICreateUserCommand
    {
        Task<ActionResult> Execute(UserProfileViewModel model);
    }
}
