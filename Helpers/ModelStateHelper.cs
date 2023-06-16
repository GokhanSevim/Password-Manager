using Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Results.Abstract.Results;
using Results.Concrete.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
	[TryException]
	public static class ModelStateHelper
	{
		public static async Task<IResult> GetErrors(ModelStateDictionary modelState)
		{
			var result = new Result(true);

			if (modelState is null) 
				return result;

			var message = new StringBuilder();

			foreach (var item in modelState.Values)
			{
				foreach (var item2 in item.Errors)
				{
					message.AppendLine($"{item2.ErrorMessage}");
				}
			}

			result.Success = false;
			result.Message = message.ToString();

			return await Task.FromResult(result);
		}
	}
}
