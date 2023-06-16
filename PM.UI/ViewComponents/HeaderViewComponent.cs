using Helpers;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Filters;

namespace PM.UI.ViewComponents
{
	[AuthorizeWithSession]
	public class HeaderViewComponent : ViewComponent
	{

		[BindProperty]
        public ApplicationUser CurrentUser { get; set; }

        public async Task<IViewComponentResult> InvokeAsync()
		{
			CurrentUser = HttpContext.GetCurrentUser();

			await Task.CompletedTask;

			return View(this);
		}
	}
}
