using Microsoft.AspNetCore.Mvc;

namespace PM.UI.ViewComponents
{
	public class HeaderUserViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			await Task.CompletedTask;

			return View();
		}
	}
}
