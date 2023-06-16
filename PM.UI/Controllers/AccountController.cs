using Dto;
using Dto.Account;
using Entities;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PM.UI.Models;

namespace PM.UI.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ILogger<AccountController> _logger;

		public AccountController(SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
		{
			_signInManager = signInManager;
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{
			if (User.Identity is not null && User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index");
			}

			return View();
		}

		public IActionResult SingUp()
		{
			if (User.Identity is not null && User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index");
			}

			var model = new RegisterDto();

			return View(model);
		}

		public IActionResult ForgotPassword()
		{
			if (User.Identity is not null && User.Identity.IsAuthenticated)
			{
				return Redirect("/");
			}

			return View();
		}

		public IActionResult ResetPassword(string code)
		{
			if (User.Identity is not null && User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index");
			}

			var model = new ResetPasswordDto()
			{
				Code = code
			};

			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			HttpContext.ClearSessionOfCurrentUser();

			_logger.LogInformation("Kullanıcı oturumu kapattı.");

			return RedirectToAction("Login");
		}
	}
}
