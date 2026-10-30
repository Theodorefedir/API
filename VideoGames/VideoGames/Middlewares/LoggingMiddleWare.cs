using System.Diagnostics;

namespace VideoGames.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Request
            var request = context.Request;
            string message = $"IsHttps: {request.IsHttps}\n" +
                $"Method: {request.Method}\n" +
                $"Url: {request.Scheme}://{request.Host}{request.Path}";

            _logger.LogInformation(message);

            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            // Response
            var response = context.Response;
            message = $"StatusCode: {response.StatusCode} {stopwatch.ElapsedMilliseconds}ms";
            _logger.LogInformation(message);
        }
    }
}
