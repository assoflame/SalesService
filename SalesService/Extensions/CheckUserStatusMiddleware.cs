using SalesService.Entities.Models;

namespace SalesService.Extensions
{
    public class CheckUserStatusMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckUserStatusMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated ||
                context.User.HasClaim("Status", UserStatus.Normal.ToString()))
            {
                await _next.Invoke(context);
            } else
            {
                await context.Response.WriteAsync("You were blocked");
            }
        }
    }
}
