using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;

namespace Filters
{
	[TryException]
	public class AuthorizeWithSessionAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (context is null)
				return;

			var userBytes = context.HttpContext.Session.Get("CurrentUser");

			if (!context.HttpContext.User.Identity.IsAuthenticated || userBytes is null)
			{
				context.Result = new RedirectToActionResult("Logout", "Account", null);
			}
		}
	}
}
