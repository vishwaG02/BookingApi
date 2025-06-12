namespace Booking.App.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey))
                throw new UnauthorizedAccessException("API Key is missing.");

            var configuredApiKey = _configuration["ApiKey"];
            if (extractedApiKey != configuredApiKey)
                throw new UnauthorizedAccessException("Invalid API Key.");

            await _next(context);
        }
    }
}
