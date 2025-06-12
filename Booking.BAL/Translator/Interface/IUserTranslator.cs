using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Booking.BAL.ViewModel.UserProfile;
using Booking.DAL.Entities;

namespace Booking.BAL.Translator.Interface
{
    public interface IUserTranslator
    {
        UserProfile Translate(UserProfileViewModel model);

        List<Claim> TranslateClaims(UserProfile user);
    }
}
