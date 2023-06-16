using Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
	[TryException]
	public static class LanguageHelper
	{
		public static string GetCurrentTwoLetterISOLanguageName()
		{
			var currentLanguage = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

			if (string.IsNullOrWhiteSpace(currentLanguage))
			{
				return "tr";
			}

			return currentLanguage;
		}
	}
}
