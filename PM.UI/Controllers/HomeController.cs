using Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.UI.Models;
using System.Diagnostics;

namespace PM.UI.Controllers
{
	[AuthorizeWithSession]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[Route("/help/")]
		public IActionResult Help()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[Route("/404/")]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult _404()
		{
			return View();
		}

		[Route("/500/")]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult _500()
		{
			return View();
		}
	}
}