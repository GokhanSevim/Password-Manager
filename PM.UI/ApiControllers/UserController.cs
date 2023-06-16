using Filters;
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Helpers;
using Dto.Account;

namespace PM.UI.ApiControllers
{
    [Route("api/[controller]")]
	[TryException]
	public class UserController : Controller
	{
		private readonly IAccountService _accountService;

		public UserController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		public IActionResult Index()
		{
			return Redirect("/");
		}

		[Route("add"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> Add(RegisterDto Input)
		{
			if (ModelState.IsValid)
			{
				var result = await _accountService.AddAsync(Input);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}

		[Route("login"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> Login(LoginDto Input)
		{
			if (ModelState.IsValid)
			{
				var result = await _accountService.LoginAsync(Input);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}

		[Route("forgot-password"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> ForgotPassword(ForgotPasswordDto Input)
		{
			if (ModelState.IsValid)
			{
				var result = await _accountService.ForgotPasswordAsync(Input);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}

		[Route("reset-password"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> ResetPassword(ResetPasswordDto Input)
		{
			if (ModelState.IsValid)
			{
				var result = await _accountService.ResetPasswordAsync(Input);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}
	}
}
