using Booking.App.Common;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BAL.ViewModel.Booking
{
    public class CancelBookingResponseExample : IExamplesProvider<ApiResponse<string>>
    {
        public ApiResponse<string> GetExamples()
        {
            return ApiResponse<string>.Success("Booking cancelled successfully");
        }
    }
}
