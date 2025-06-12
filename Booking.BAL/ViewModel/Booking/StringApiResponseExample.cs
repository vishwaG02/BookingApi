using Booking.App.Common;
using Booking.BAL.Constant;
using Swashbuckle.AspNetCore.Filters;

public class StringApiResponseExample : IExamplesProvider<ApiResponse<string>>
{
    public ApiResponse<string> GetExamples()
    {
        return ApiResponse<string>.Created(BookingConstants.BookingCreated);
    }
}
