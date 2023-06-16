using Entities;
using Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Helpers
{
	[TryException]
	public static class IdentityHelper
	{
		public static string GetName(this ClaimsPrincipal user)
		{
			return user.FindFirstValue(ClaimTypes.Name);
		}

		public static byte[] GetSecretKey(this HttpContext httpContext)
		{
			return GetCurrentUser(httpContext).SecretKey;
		}

		public static string GetCurrentUserId(this HttpContext httpContext)
		{
			return GetCurrentUser(httpContext).Id;
		}

		public static ApplicationUser GetCurrentUser(this HttpContext httpContext)
		{
			if (httpContext is null)
				return null;

			var userBytes = httpContext.Session.Get("CurrentUser");

			if (userBytes is not null)
			{
				var user = JsonConvert.DeserializeObject<ApplicationUser>(Encoding.UTF8.GetString(userBytes));

				return user;
			}

			return new ApplicationUser();
		}

		public static void SetCurrentUser(this HttpContext httpContext, ApplicationUser user)
		{
			if (httpContext is null || user is null)
				return;

			var userBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(user));

			httpContext.Session.Set("CurrentUser", userBytes);
		}

		public static void ClearSessionOfCurrentUser(this HttpContext httpContext)
		{
			if (httpContext is null)
				return;

			httpContext.Session.Clear();
		}
	}
}
