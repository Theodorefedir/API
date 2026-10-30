namespace VideoGames.Middlewares
{
    public class IpBlockMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly List<string> BlockedIps = new()
        {
            "192.168.1.100",
            "10.0.0.5",
            //"::1"
        };

        public IpBlockMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            if (ip != null && BlockedIps.Contains(ip))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync($"Access denied. Your IP ({ip}) is blocked.");
                return;
            }

            await _next(context);
        }
    }
}
