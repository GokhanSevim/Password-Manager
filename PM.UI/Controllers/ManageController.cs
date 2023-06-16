using Dto;
using Entities;
using Filters;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PM.UI.Controllers
{
	[AuthorizeWithSession]
	public class ManageController : Controller
	{
		[BindProperty]
		public ApplicationUser CurrentUser { get; set; }

        public IActionResult Index()
		{
			return View(this);
		}

		public IActionResult Profile()
		{
			CurrentUser = HttpContext.GetCurrentUser();

			return View(this);
		}

		[Route("[controller]/account-settings")]
		public IActionResult AccountSettings()
		{
			return View(this);
		}
	}
}
