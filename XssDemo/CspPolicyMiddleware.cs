using Microsoft.Extensions.Options;

namespace XssDemo
{
    public class CspPolicyOptions
    {
        public string CspHeaderValue { get; set; }
    }
    public class CspPolicyMiddleware
    {
        private readonly CspPolicyOptions options;
        private readonly RequestDelegate next;
        public CspPolicyMiddleware(RequestDelegate next, IOptions<CspPolicyOptions> options)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Content-Security-Policy", options.CspHeaderValue);
            await next(context);
        }
    }

    public static class CspPolicyMiddlewareExtensions
    {
        public static IApplicationBuilder UseCspPolicy(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CspPolicyMiddleware>();
        }
    }
}
