using System.Diagnostics;

namespace VideoGames.Middlewares
{
    public class ConsoleLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ConsoleLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;
            var url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}";

            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();
            Console.WriteLine($"[CONSOLE] {method} {url} → Статус: {context.Response.StatusCode} | Время: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
