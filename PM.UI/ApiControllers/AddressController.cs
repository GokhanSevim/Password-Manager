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
	public class AddressController : Controller
	{
		private readonly IAddressService _addressService;

		public AddressController(IAddressService addressService)
		{
			_addressService = addressService;
		}

		public ActionResult Index()
		{
			return Redirect("/");
		}

		[Route("add"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> Add(AddressDto Input)
		{
			if (ModelState.IsValid)
			{
				Input.UserId = HttpContext.GetCurrentUserId();

				var result = await _addressService.AddAsync(Input);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}

		[Route("edit"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> Edit(AddressDto Input)
		{
			if (ModelState.IsValid)
			{
				Input.UserId = HttpContext.GetCurrentUserId();

				var result = await _addressService.UpdateAsync(Input);

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

				var result = await _addressService.DeleteAsync(x => arr.Any(y => y == x.Id) && x.UserId == userId);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}
	}
}
