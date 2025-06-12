using Booking.BAL.ViewModel.UserProfile;
using Microsoft.AspNetCore.Mvc;

namespace Booking.App.Command.User.Interface
{
    public interface IUserLoginCommand
    {
        Task<ActionResult> Execute(LoginRequest request);
    }
}
