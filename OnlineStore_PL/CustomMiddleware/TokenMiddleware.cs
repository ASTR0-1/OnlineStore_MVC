using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OnlineStore_PL.CustomMiddleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies[".AspNetCore.Application.Id"];

            if (!string.IsNullOrEmpty(token))
                context.Request.Headers.Add("Authorization", "Bearer " + token);

            await _next.Invoke(context);
        }
    }
}
