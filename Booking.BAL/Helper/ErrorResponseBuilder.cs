using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BAL.Helper
{
    public static class ErrorResponseBuilder
    {
        public static BadRequestObjectResult BadRequest( List<ErrorDetailResponse> details)
        {
            return new BadRequestObjectResult(new ErrorResponse
            {
                StatusCode = 400,
                Error = "Bad Request",
                Message = "Validation failed",
                Details = details
            });
        }


        public static ObjectResult Conflict(List<ErrorDetailResponse> details)
        {
            return new ObjectResult(new ErrorResponse
            {
                StatusCode = 409,
                Error = "Conflict",
                Message = "Validation failed",
                Details = details
            })
            {
                StatusCode = 409
            };
        }


    }
}
