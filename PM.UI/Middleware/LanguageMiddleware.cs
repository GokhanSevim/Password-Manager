using Filters;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace PM.UI.Middleware
{
	[TryException]
	public class LanguageMiddleware
	{
		private readonly RequestDelegate _next;

		public LanguageMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			httpContext.Request.RouteValues.TryGetValue("culture", out var cultureValue);

			string culture = cultureValue as string;

			culture ??= httpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

			if (string.IsNullOrWhiteSpace(culture))
			{
				culture = "tr";
			}

			var cultureInfo = CultureInfo.GetCultureInfo(culture);

			if (cultureInfo is not null)
			{
				Thread.CurrentThread.CurrentCulture = cultureInfo;
				Thread.CurrentThread.CurrentUICulture = cultureInfo;
			}

			httpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, Thread.CurrentThread.CurrentUICulture.Name, options: new CookieOptions
			{
				Expires = DateTimeOffset.UtcNow.AddYears(1),
				Path = "/",
				HttpOnly = true,
				SameSite = SameSiteMode.Strict,
				Secure = true
			});

			await _next.Invoke(httpContext);
		}
	}
}
