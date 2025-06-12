using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BAL.ViewModel.Booking
{
    public class PagedBookingResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IEnumerable<BookingResponse> Data { get; set; } = null;
        public int TotalRecords { get; set; } = 0;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1;
    }
}
