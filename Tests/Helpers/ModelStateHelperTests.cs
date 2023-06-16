using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using Results.Abstract.Results;

namespace Tests.Helpers
{
	[TestFixture]
	public class ModelStateHelperTests
	{
		[Test]
		public async Task Is_Null()
		{
			ModelStateDictionary modelState = null;

			IResult result = await ModelStateHelper.GetErrors(modelState);

			Assert.Multiple(() =>
			{
				Assert.That(result.Success, Is.True);
				Assert.That(result.Message, Is.EqualTo("İşleminiz gerçekleştirilmiştir."));
			});
		}

		[Test]
		public async Task Is_Not_Null()
		{
			var modelState = new ModelStateDictionary();
			modelState.AddModelError("FirstName", "Adınızı girin.");
			modelState.AddModelError("LastName", "Soyadınızı girin.");

			IResult result = await ModelStateHelper.GetErrors(modelState);

			Assert.Multiple(() =>
			{
				Assert.That(result.Success, Is.False);
				Assert.That(result.Message, Is.EqualTo("Soyadınızı girin.\r\nAdınızı girin.\r\n"));
			});
		}
	}
}
