using Business.Abstract;
using Dto.Crypt;
using Entities;
using Filters;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PM.UI.ApiControllers
{
	[AuthorizeWithSession]
	[TryException]
	[Route("api/[controller]")]
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoriesService;

		public CategoryController(ICategoryService categoriesService)
		{
			_categoriesService = categoriesService;
		}

		public ActionResult Index()
		{
			return Redirect("/");
		}

		[Route("add"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> Add(ECategories Input)
		{
			if (ModelState.IsValid)
			{
				Input.UserId = HttpContext.GetCurrentUserId();

				var result = await _categoriesService.AddAsync(Input);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}

		[Route("edit"), HttpPost, ValidateAntiForgeryToken, TryException]
		public async Task<JsonResult> Edit(ECategories Input)
		{
			if (ModelState.IsValid)
			{
				Input.UserId = HttpContext.GetCurrentUserId();

				var result = await _categoriesService.UpdateAsync(Input);

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

				var result = await _categoriesService.DeleteAsync(x => arr.Any(y => y == x.Id) && x.UserId == userId);

				return Json(result);
			}

			return Json(await ModelStateHelper.GetErrors(ModelState));
		}

		[Route("list"), HttpGet, TryException]
		public async Task<IActionResult> List()
		{
			var userId = HttpContext.GetCurrentUserId();

			var list = await _categoriesService.FindAllAsync(x => x.Published && x.UserId == userId, orderBy: x => x.OrderBy(x => x.Name));

			return Json(list);
		}
	}
}
