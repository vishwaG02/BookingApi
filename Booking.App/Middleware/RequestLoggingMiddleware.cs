using System.Diagnostics;
using System.Text.Json;
using Booking.BAL.Helper;

namespace Booking.App.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Enable request body buffering so we can read it without affecting the actual pipeline
            context.Request.EnableBuffering();

            // Read request body
            string requestBody = "";
            if (context.Request.ContentLength > 0 &&
                context.Request.ContentType?.Contains("application/json") == true)
            {
                using var reader = new StreamReader(
                    context.Request.Body,
                    leaveOpen: true);

                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            // Log the request
            _logger.LogInformation("HTTP Request: {method} {url} | Headers: {headers} | Body: {body}",
                context.Request.Method,
                context.Request.Path,
                context.Request.Headers,
                string.IsNullOrWhiteSpace(requestBody) ? "<empty>" : requestBody);

            try
            {
                await _next(context);

                _logger.LogInformation("HTTP Response: {statusCode}", context.Response.StatusCode);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access.");
                var errorResponse = new ErrorResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = "Unauthorized",
                    Message = "Missing or invalid API key",
                    Details = new List<ErrorDetailResponse>
                    {
                        new ErrorDetailResponse
                        {
                            Field = "X-API-Key",
                            Issue = ex.Message
                        }
                    }
                };

                context.Response.StatusCode = errorResponse.StatusCode;
                context.Response.ContentType = "application/json";
                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");

                var errorResponse = new ErrorResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Error = "Internal Server Error",
                    Message = ex.Message                   
                };

                context.Response.StatusCode = errorResponse.StatusCode;
                context.Response.ContentType = "application/json";
                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("Request processed in {duration} ms", stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
