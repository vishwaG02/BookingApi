using Booking.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Pagination
{
    public class BookingQueryParameters
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public string? sortBy { get; set; } = "StartTime";
        public bool descending { get; set; } = false;
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public BookingStatus status { get; set; } = BookingStatus.Confirmed;
    }
}
