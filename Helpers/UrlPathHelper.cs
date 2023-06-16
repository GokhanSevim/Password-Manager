using Core.Entities;
using Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helpers
{
	[TryException]
	public static class UrlPathHelper
	{
		public static async Task<string> CombineUrl(params dynamic[] paths)
		{
			string path = "/";

			if (paths is not null)
			{
				string regexHttp = @"^(?:(https?):)?\/\/(?:[\w\d]+\.)+[\w\d]{2,}(?:[\w\d./?=%&_-]*)?$";
				string regexLocal = @"^\/\/localhost:(\d{1,5})\/[\w\d./?=%&_-]*$";

				path = string.Join('/', paths);

				if (!Regex.IsMatch(path, regexHttp) && !Regex.IsMatch(path, regexLocal))
				{
					path = $"/{path}/";
					path = Regex.Replace(path, @"\/+", "/");
				}
			}

			return await Task.FromResult(path);
		}
	}
}
