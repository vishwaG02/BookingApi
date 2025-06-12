using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BAL.Helper
{
    public class ClaimsHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : 0;
        }

        public bool IsAdmin()
        {
            return _httpContextAccessor.HttpContext?.User.IsInRole("Admin") ?? false;
        }

        public bool IsCustomer()
        {
            return _httpContextAccessor.HttpContext?.User.IsInRole("Customer") ?? false;
        }
    }
}
