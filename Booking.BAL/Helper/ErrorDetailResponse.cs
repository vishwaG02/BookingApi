using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BAL.Helper
{
    public class ErrorDetailResponse
    {
        public string Field { get; set; }
        public string Issue { get; set; }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public List<ErrorDetailResponse> Details { get; set; } = new();
    }
}
