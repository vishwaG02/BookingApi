using Booking.DAL.Pagination;
using Swashbuckle.AspNetCore.Filters;

public class BookingQueryParametersExample : IExamplesProvider<BookingQueryParameters>
{
    public BookingQueryParameters GetExamples()
    {
        return new BookingQueryParameters
        {
            pageNumber = 1,
            pageSize = 10,
            sortBy = "startTime",
            descending = false,
            fromDate = DateTime.UtcNow.AddDays(-7),
            toDate = DateTime.UtcNow,
            status = Booking.DAL.Enum.BookingStatus.Confirmed
        };
    }
}
