using Filters;

namespace PM.UI.Middleware
{
	public class SecurityHeadersMiddleware
	{
		private readonly RequestDelegate next;

		public SecurityHeadersMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		[TryException]
		public async Task Invoke(HttpContext httpContext)
		{
			httpContext.Response.Headers.Remove("x-powered-by");
			httpContext.Response.Headers.Remove("server");
			httpContext.Response.Headers.Remove("X-Frame-Options");
			httpContext.Response.Headers.Remove("X-Xss-Protection");
			httpContext.Response.Headers.Remove("X-Content-Type-Options");
			httpContext.Response.Headers.Remove("Content-Security-Policy");
			httpContext.Response.Headers.Remove("Referrer-Policy");
			httpContext.Response.Headers.Remove("Permissions-Policy");


			httpContext.Response.Headers.Add("X-Frame-Options", "DENY");
			httpContext.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
			httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
			httpContext.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; object-src 'none'; img-src 'self' data: *.cloudinary.com; font-src 'self' data: *.googleapis.com *.gstatic.com *.cloudflare.com; style-src 'self' 'unsafe-inline' *.unpkg.com unpkg.com *.cloudflare.com *.google.com *.googleapis.com *.jsdelivr.net; script-src 'self' 'unsafe-inline' 'unsafe-eval' *.unpkg.com unpkg.com *.cloudflare.com *.google.com *.gstatic.com *.googletagmanager.com *.googleapis.com *.jsdelivr.net; frame-src 'self' *.youtube.com *.google.com; frame-ancestors 'none'; connect-src 'self' localhost:* *.fatigroup.com.tr httpbin.org;");
			httpContext.Response.Headers.Add("Referrer-Policy", "same-origin");
			httpContext.Response.Headers.Add("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");

			await next(httpContext);
		}
	}
}
