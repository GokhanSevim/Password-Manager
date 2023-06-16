using Business.Abstract;
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
	public class BankAccountController : Controller
	{
		private readonly IBankAccountsService _bankAccountsService;

		public BankAccountController(IBankAccountsService bankAccountsService)
		{
			_bankAccountsService = bankAccountsService;
		}

		public ActionResult Index()
		{
			return Redirect("/");
		}

		[Route("add"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> Add(BankAccountDto Input)
		{
			if (ModelState.IsValid)
			{
				Input.UserId = HttpContext.GetCurrentUserId();
				Input.SecretKey = HttpContext.GetSecretKey();

				var result = await _bankAccountsService.AddAsync(Input);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}

		[Route("edit"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> Edit(BankAccountDto Input)
		{
			if (ModelState.IsValid)
			{
				Input.UserId = HttpContext.GetCurrentUserId();
				Input.SecretKey = HttpContext.GetSecretKey();

				var result = await _bankAccountsService.UpdateAsync(Input);

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

				var result = await _bankAccountsService.DeleteAsync(x => arr.Any(y => y == x.Id) && x.UserId == userId);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}
	}
}
