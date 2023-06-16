using Business.Abstract;
using Dto;
using Dto.Crypt;
using Entities;
using Filters;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PM.UI.ApiControllers
{
	[TryException]
	[AuthorizeWithSession]
	[Route("api/[controller]")]
	public class PasswordController : Controller
	{
		private readonly IPasswordsService _passwordsService;

		public PasswordController(IPasswordsService passwordsService)
		{
			_passwordsService = passwordsService;
		}

		public ActionResult Index()
		{
			return Redirect("/");
		}

		[Route("add"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> Add(PasswordDto Input)
		{
			if (ModelState.IsValid)
			{
				Input.SecretKey = HttpContext.GetSecretKey();
				Input.UserId = HttpContext.GetCurrentUserId();

				var result = await _passwordsService.AddAsync(Input);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}

		[Route("edit"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> Edit(PasswordDto Input)
		{
			if (ModelState.IsValid)
			{
				Input.SecretKey = HttpContext.GetSecretKey();
				Input.UserId = HttpContext.GetCurrentUserId();

				var result = await _passwordsService.UpdateAsync(Input);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}

		[Route("delete"), HttpPost, TryException]
		public async Task<JsonResult> Delete(string id)
		{
			if (ModelState.IsValid && !string.IsNullOrWhiteSpace(id))
			{
				var arr = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

				var userId = HttpContext.GetCurrentUserId();

				var result = await _passwordsService.DeleteAsync(x=> arr.Any(y=> y == x.Id) && x.UserId == userId);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}
	}
}
