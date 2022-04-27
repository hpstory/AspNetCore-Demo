using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace MiddlewareDemo
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestLimitingMiddleware
    {
        private const int Limit = 10;

        private readonly RequestDelegate next;

        private readonly IMemoryCache cache;

        public RequestLimitingMiddleware(
            RequestDelegate next,
            IMemoryCache cache)
        {
            this.next = next;
            this.cache = cache;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var requestKey = $"{httpContext.Request.Method}-{httpContext.Request.Path}";
            int hitCount = 0;
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
            };

            if (this.cache.TryGetValue(requestKey, out hitCount))
            {
                if (hitCount < Limit)
                {
                    await this.ProcessRequest(httpContext, requestKey, hitCount, cacheOptions);
                }
                else
                {
                    httpContext.Response.Headers["X-RateLimit-RetryAfter"]
                        = cacheOptions.AbsoluteExpiration?.ToString();
                    httpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                }
            }
            else
            {
                await this.ProcessRequest(httpContext, requestKey, hitCount, cacheOptions);
            }
        }

        private async Task ProcessRequest(
            HttpContext httpContext, string requestKey, int hitCount, MemoryCacheEntryOptions cacheOptions)
        {
            hitCount++;
            this.cache.Set(requestKey, hitCount, cacheOptions);
            httpContext.Response.Headers["X-RateLimit-Limit"] = Limit.ToString();
            httpContext.Response.Headers["X-RateLimit-Remaining"] = (Limit - hitCount).ToString();
            await next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestLimitingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLimitingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLimitingMiddleware>();
        }
    }
}
