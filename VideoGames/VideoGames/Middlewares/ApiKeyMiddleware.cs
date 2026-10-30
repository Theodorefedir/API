namespace VideoGames.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        private const string ApiKeyHeaderName = "lol";
        private const string ValidApiKey = "LOL";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key was not provided.");
                return;
            }
            if (extractedApiKey != ValidApiKey)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized client.");
                return;
            }
            await _next(context);
        }
    }
}
